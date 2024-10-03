using Domain.DTOs;
using Domain.Entities;

using ExpenseTrackerGroup3.Services.Interfaces;
using ExpenseTrackerGroup3.Repositories.Interfaces;
using ExpenseTrackerGroup3.Exceptions;
using ExpenseTrackerGroup3.Utils.Exception;
using ExpenseTrackerGroup3.Utils.EmailSender;

namespace ExpenseTrackerGroup3.Services;

public class ExpenseService : IExpenseService
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly IUserRepository _userRepository;
    private readonly IBudgetService _budgetService;
    private readonly IEmailSender _emailSender;

    public ExpenseService(IExpenseRepository expenseRepository, IUserRepository userRepository, IBudgetService budgetService, IEmailSender emailSender)
    {
        _expenseRepository = expenseRepository;
        _userRepository = userRepository;
        _budgetService = budgetService;
        _emailSender = emailSender;
    }

    public async Task<Expense> AddExpenseAsync(Guid userId, CreateExpense expense)
    {
        var user = await ValidateUserAsync(userId);
        var remainingBudget = await ValidateBudgetAsync(userId, expense.Amount);
        await NotifyIfThresholdExceededAsync(user, remainingBudget, expense.Amount);

        var newExpense = new Expense
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Amount = expense.Amount,
            Description = expense.Description,
            Category = expense.Category,
            Date = expense.Date,
            CreatedAt = DateTime.Now,
            RecurringExpense = expense.RecurringExpense
        };

        var result = await _expenseRepository.CreateAsync(newExpense);
        result.ThrowIfOperationFailed("Failed to create an Expense");

        return newExpense;
    }

    public async Task<IEnumerable<Expense>> GetExpenseByUserIdAsync(Guid userId)
    {
        IEnumerable<Expense> userExpenses = await _expenseRepository.GetMonthlyExpensesAsync(userId, DateTime.Now);
        userExpenses.ThrowIfEmpty("No expenses found for the user");

        return userExpenses;
    }

    public async Task<string> GetHighestExpenseUserCategoryAsync(Guid userId)
    {
        string? highestCategory = await _expenseRepository.GetHighestSpendingCategoryByUserId(userId);
        return highestCategory ?? "No category found";
    }

    public async Task<IEnumerable<Expense>> GetUserExpensesByCategoryAsync(Guid userId, DateTime month, string category)
    {
        IEnumerable<Expense> userExpenses = await _expenseRepository.GetAllByUserId(userId);

        var categoryExpenses = userExpenses
            .Where(expense => expense.Category
            .Equals(category, StringComparison.OrdinalIgnoreCase)
            && expense.Date.Month.Equals(month.Month));

        categoryExpenses.ThrowIfEmpty("Invalid category");

        return categoryExpenses;
    }

    public async Task<Expense> UpdateExpenseAsync(Guid userId, Guid expenseId, CreateExpense expense)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        user.ThrowIfNull("User not found");

        var existingExpense = await _expenseRepository.GetByIdAsync(expenseId);

        if (existingExpense == null || existingExpense.UserId != userId)
        {
            throw new NotFoundException("This user does not have this expense asociated");
        }

        existingExpense.Amount = expense.Amount;
        existingExpense.Description = expense.Description;
        existingExpense.Category = expense.Category;
        existingExpense.Date = expense.Date;
        existingExpense.CreatedAt = expense.Date;
        existingExpense.RecurringExpense = expense.RecurringExpense;

        var success = await _expenseRepository.UpdateAsync(existingExpense);
        success.ThrowIfOperationFailed("Failed to update expense");

        return existingExpense;
    }

    public async Task<bool> DeleteExpense(Guid userId, Guid expenseId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        user.ThrowIfNull("User not found");

        var expense = await _expenseRepository.GetByIdAsync(expenseId);

        if (expense == null || expense.UserId != userId)
        {
            throw new NotFoundException("This user does not have this expense asociated");
        }

        return await _expenseRepository.DeleteAsync(expense.Id);
    }

    public async Task<string> GetUserMostExpensiveMonth(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        user.ThrowIfNull("User not found");

        DateTime? mostExpensiveMonth = await _expenseRepository.GetMostExpensiveMonthByUserId(userId);

        return mostExpensiveMonth.Value.ToString("MMMM yyyy") ?? "No expense found for the user";
    }

    public async Task<IEnumerable<Expense>> GetUserRecurringExpense(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        user.ThrowIfNull("User not found");

        IEnumerable<Expense> userExpenses = await _expenseRepository.GetAllByUserId(userId);

        var userRecurringExpense = userExpenses
            .Where(expense => expense.RecurringExpense == true);

        return userRecurringExpense;
    }

    private async Task<User> ValidateUserAsync(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        user.ThrowIfNull("User not found");
        return user;
    }

    private async Task<decimal> ValidateBudgetAsync(Guid userId, decimal expenseAmount)
    {
        var remainingBudget = await _budgetService.GetRemainingBudgetAsync(userId);

        if (expenseAmount > remainingBudget)
        {
            throw new BadRequestException("Expense surpasses the current budget amount");
        }

        return remainingBudget - expenseAmount;
    }

    private async Task NotifyIfThresholdExceededAsync(User user, decimal remainingBudget, decimal expenseAmount)
    {
        var currentBudget = await _budgetService.GetBudgetUserByMonthAsync(user.Id, DateTime.Now);
        currentBudget.ThrowIfNull("Monthly budget not created");

        var totalExpenses = currentBudget.BudgetAmount - remainingBudget;
        var totalExpensesIncludingCurrent = totalExpenses + expenseAmount;

        var percentageSpent = totalExpensesIncludingCurrent * 100 / currentBudget.BudgetAmount;

        if (percentageSpent > currentBudget.AlertThreshold)
        {
            string subject = "Budget threshold surpassed - Expense Tracker";
            string body = $"Dear {user.Name}, you have surpassed your budget threshold, please check your balance.";
            await _emailSender.SendEmail(user.Email, subject, body);
        }
    }

}
