using Domain.DTOs;
using Domain.Entities;

using ExpenseTrackerGroup3.Exceptions;
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

        var alredyExists = await _budgetRepository.GetBudgetsByUserAsync(userId);
        alredyExists.ThrowIfExists("Budget already exists for the user");

        var newBudget = new Budget
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Month = budget.Month,
            BudgetAmount = budget.BudgetAmount,
            AlertThreshold = budget.AlertThreshold
        };

        var sucess = await _budgetRepository.CreateAsync(newBudget);
        sucess.ThrowIfOperationFailed("Failed to create budget");        

        return newBudget;
    }

    public async Task<bool> CheckBudgetStatusAsync(Guid userId, DateTime month)
    {
        var currentMonth = DateTime.UtcNow;
        var budget = await _budgetRepository.GetMonthlyBudgetByUserId(userId, month);
        budget.ThrowIfNull("Budget not found for the specified user and month");

        var expenses = await _expenseRepository.GetMonthlyExpensesAsync(userId, currentMonth);
        var totalExpenses = expenses?.Sum(e => e?.Amount) ?? 0;

        return totalExpenses >= (budget!.BudgetAmount * (budget.AlertThreshold / 100));
    }

    public async Task<bool> DeleteBudgetAsync(Guid userId)
    {
        var existingUser = await _userRepository.GetByIdAsync(userId);
        existingUser.ThrowIfNull("User not found");

        var budgetToDelete = await _budgetRepository.GetBudgetByUserAsync(userId);
        budgetToDelete.ThrowIfNull("Budget not found for the specified user");

        return await _budgetRepository.DeleteAsync(budgetToDelete!.Id);
    }

    public async Task<Budget?> GetBudgetUserByMonthAsync(Guid userId, DateTime month)
    {
        var userExists = await _userRepository.GetByIdAsync(userId);
        userExists.ThrowIfNull("User not found");

        var budget = await _budgetRepository.GetMonthlyBudgetByUserId(userId, month);
        budget.ThrowIfNull("Budget not found for the specified user and month");

        return budget;
    }

    public async Task<decimal> GetRemainingBudgetAsync(Guid userId)
    {
        var currentMonth = DateTime.UtcNow;
        var budget = await _budgetRepository.GetBudgetByUserAsync(userId);
        budget.ThrowIfNull("Budget not found for the specified user");

        var expenses = await _expenseRepository.GetMonthlyExpensesAsync(userId, currentMonth);
        var totalExpenses = expenses?.Sum(e => e?.Amount) ?? 0;
        
        return budget!.BudgetAmount - totalExpenses;
    }

    public async Task<Budget> UpdateBudgetAsync(Guid userId, CreateBudget budget)
    {
        var existingBudget = await _userRepository.GetByIdAsync(userId);
        existingBudget.ThrowIfNull("User not found");

        var budgetToUpdate = await _budgetRepository.GetBudgetByUserAsync(userId);
        existingBudget.ThrowIfNull("Budget not found");

        budgetToUpdate!.Month = budget.Month;
        budgetToUpdate.BudgetAmount = budget.BudgetAmount;
        budgetToUpdate.AlertThreshold = budget.AlertThreshold;

        await _budgetRepository.UpdateAsync(budgetToUpdate);

        return budgetToUpdate;
    }
}
