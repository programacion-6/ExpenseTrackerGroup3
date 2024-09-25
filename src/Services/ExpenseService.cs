using Domain.DTOs;
using Domain.Entities;

using ExpenseTrackerGroup3.Services.Interfaces;
using ExpenseTrackerGroup3.Repositories.Interfaces;

namespace ExpenseTrackerGroup3.Services;

public class ExpenseService : IExpenseService
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly IUserRepository _userRepository;

    public ExpenseService(IExpenseRepository expenseRepository, IUserRepository userRepository)
    {
        _expenseRepository = expenseRepository;
        _userRepository = userRepository;
    }

    public async Task<Expense> AddExpenseAsync(Guid userId, CreateExpense expense)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            throw new ArgumentException("User not found");
        }

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

        if (result == false)
        {
            throw new ArgumentException("Failed to create an Expense");
        }

        return newExpense;
    }

    public async Task<IEnumerable<Expense>> GetExpenseByUserIdAsync(Guid userId)
    {
        IEnumerable<Expense> userExpenses = await _expenseRepository.GetAllByUserId(userId);

        if (!userExpenses.Any())
        {
            throw new ArgumentException("No expenses found for the user");
        }

        return userExpenses;
    }

    public async Task<string> GetHighestExpeseUserCategoryAsync(Guid userId)
    {
        string? highestCategory = await _expenseRepository.GetHighestSpendingCategoryByUserId(userId);

        if (highestCategory == null)
        {
            return "No category found";
        }

        return highestCategory;
    }

    public async Task<IEnumerable<Expense>> GetUserExpensesByCategoryAsync(Guid userId, DateTime month, string category)
    {
        IEnumerable<Expense> userExpenses = await _expenseRepository.GetAllByUserId(userId);

        var categoryExpenses = userExpenses
            .Where(expense => expense.Category
            .Equals(category, StringComparison.OrdinalIgnoreCase) 
            && expense.Date.Month.Equals(month.Month));

        if (!categoryExpenses.Any())
        {
            throw new ArgumentException("Invalid category");
        }

        return categoryExpenses;
    }

    public async Task<Expense> UpdateExpenseAsync(Guid userId, Guid expenseId, CreateExpense expense)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user == null)
        {
            throw new ArgumentException("User not found");
        }

        var existingExpense = await _expenseRepository.GetByIdAsync(expenseId);

        if (existingExpense == null || existingExpense.UserId != userId)
        {
            throw new ArgumentException("This user does not have this expense asociated");
        }

        existingExpense.Amount = expense.Amount;
        existingExpense.Description = expense.Description;
        existingExpense.Category = expense.Category;
        existingExpense.Date = expense.Date;
        existingExpense.CreatedAt = expense.Date;
        existingExpense.RecurringExpense = expense.RecurringExpense;

        if (!await _expenseRepository.UpdateAsync(existingExpense))
        {
            throw new ArgumentException("Error updating the expense");
        };

        return existingExpense;
    }

    public async Task<bool> DeleteExpense(Guid userId, Guid expenseId)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user == null)
        {
            throw new ArgumentException("User not found");
        }

        var expense = await _expenseRepository.GetByIdAsync(expenseId);

        if (expense == null || expense.UserId != userId)
        {
            throw new ArgumentException("This user does not have this expense asociated");
        }

        return await _expenseRepository.DeleteAsync(expense.Id);
    }

    public async Task<string> GetUserMostExpensiveMonth(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user == null)
        {
            throw new ArgumentException("User not found");
        }

        DateTime? mostExpensiveMonth = await _expenseRepository.GetMostExpensiveMonthByUserId(userId);

        if (mostExpensiveMonth == null)
        {
            return "No expense found for the user";
        }

        return mostExpensiveMonth.Value.ToString("MMMM yyyy");
    }

    public async Task<IEnumerable<Expense>> GetUserRecurringExpense(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user == null)
        {
            throw new ArgumentException("User not found");
        }

        IEnumerable<Expense> userExpenses = await _expenseRepository.GetAllByUserId(userId);

        var userRecurringExpense = userExpenses
            .Where(expense => expense.RecurringExpense == true);

        return userRecurringExpense;
    }
}
