using Domain.DTOs;
using Domain.Entities;

using ExpenseTrackerGroup3.Domain.DTOs;

using ExpenseTrackerGroup3.Repositories.Interfaces;
using ExpenseTrackerGroup3.Services.Interfaces;
using ExpenseTrackerGroup3.Utils.Exception;

namespace ExpenseTrackerGroup3.Services;

public class BudgetService : IBudgetService
{
    private readonly IBudgetRepository _budgetRepository;
    private readonly IUserRepository _userRepository;
    private readonly IExpenseRepository _expenseRepository;

    public BudgetService(IBudgetRepository budgetRepository, IUserRepository userRepository, IExpenseRepository expenseRepository)
    {
        _budgetRepository = budgetRepository;
        _userRepository = userRepository;
        _expenseRepository = expenseRepository;
    }

    public async Task<Budget> AddBudgetAsync(Guid userId, CreateBudget budget)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        user.ThrowIfNull("User not found");

        var monthlyBudget = await _budgetRepository.GetMonthlyBudgetByUserId(userId, DateTime.Now);
        monthlyBudget.ThrowIfExists("Monthly budget for the user already exists");

        var newBudget = new Budget
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Month = DateTime.Now,
            BudgetAmount = budget.BudgetAmount,
            AlertThreshold = budget.AlertThreshold
        };

        var sucess = await _budgetRepository.CreateAsync(newBudget);
        sucess.ThrowIfOperationFailed("Failed to create budget");

        return newBudget;
    }

    public async Task<BudgetStatus> CheckBudgetStatusAsync(Guid userId)
    {
        var currentMonth = DateTime.Now;
        var budget = await _budgetRepository.GetMonthlyBudgetByUserId(userId, currentMonth);
        budget.ThrowIfNull("Budget not found for the specified user and month");

        var expenses = await _expenseRepository.GetMonthlyExpensesAsync(userId, currentMonth);
        var totalExpenses = expenses?.Sum(e => e?.Amount) ?? 0;

        var isOverThreshold = totalExpenses >= (budget.BudgetAmount * (budget.AlertThreshold / 100));

        var message = isOverThreshold
            ? "You have exceeded your budget threshold for this month."
            : "You are within your budget threshold for this month.";

        return new BudgetStatus(
            IsOverThreshold: isOverThreshold,
            TotalBudget: budget.BudgetAmount,
            TotalExpenses: totalExpenses,
            AlertThreshold: budget.AlertThreshold,
            Message: message
        );
    }


    public async Task<bool> DeleteBudgetAsync(Guid userId, Guid budgetId)
    {
        var existingUser = await _userRepository.GetByIdAsync(userId);
        existingUser.ThrowIfNull("User not found");

        var budgetToDelete = await _budgetRepository.GetSpecificBudgetByUserIdAsync(userId, budgetId);
        budgetToDelete.ThrowIfNull("Budget not found for the specified user");

        return await _budgetRepository.DeleteAsync(budgetToDelete!.Id);
    }

    public async Task<Budget?> GetBudgetUserByMonthAsync(Guid userId, DateTime month)
    {
        var userExists = await _userRepository.GetByIdAsync(userId);
        userExists.ThrowIfNull("User not found");

        var budget = await _budgetRepository.GetMonthlyBudgetByUserId(userId, month);
        budget.ThrowIfNull("Budget not found for the specified user or month");

        return budget;
    }

    public async Task<decimal> GetRemainingBudgetAsync(Guid userId)
    {
        var currentMonth = DateTime.Now;
        var budget = await _budgetRepository.GetCurrentBudgetAsync(userId);
        budget.ThrowIfNull("Budget not found for the specified user");

        var expenses = await _expenseRepository.GetMonthlyExpensesAsync(userId, currentMonth);
        var totalExpenses = expenses?.Sum(e => e?.Amount) ?? 0;

        return budget!.BudgetAmount - totalExpenses;
    }

    public async Task<Budget> UpdateBudgetAsync(Guid userId, CreateBudget budget)
    {
        var existingBudget = await _userRepository.GetByIdAsync(userId);
        existingBudget.ThrowIfNull("User not found");

        var budgetToUpdate = await _budgetRepository.GetCurrentBudgetAsync(userId);
        budgetToUpdate.ThrowIfNull("Budget not found");

        budgetToUpdate.BudgetAmount = budget.BudgetAmount;
        budgetToUpdate.AlertThreshold = budget.AlertThreshold;

        await _budgetRepository.UpdateAsync(budgetToUpdate);

        return budgetToUpdate;
    }
}
