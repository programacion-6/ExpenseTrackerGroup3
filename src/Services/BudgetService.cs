using Domain.DTOs;
using Domain.Entities;

using ExpenseTrackerGroup3.Repositories.Interfaces;
using ExpenseTrackerGroup3.Services.Interfaces;

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
        if (user == null)
        {
            throw new ArgumentException("User not found");
        }

        var alredyExists = await _budgetRepository.GetMonthlyBudgetByUserId(userId, budget.Month);

        if (alredyExists != null)
        {
            throw new ArgumentException("Budget already exists for the user");
        }

        var newBudget = new Budget
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Month = budget.Month,
            BudgetAmount = budget.BudgetAmount,
            AlertThreshold = budget.AlertThreshold
        };

        var sucess = await _budgetRepository.CreateAsync(newBudget);

        if (!sucess)
        {
            throw new Exception("Failed to create budget");
        }

        return newBudget;
    }

    public async Task<bool> CheckBudgetStatusAsync(Guid userId, DateTime month)
    {
        var currentMonth = DateTime.UtcNow;
        var budget = await _budgetRepository.GetMonthlyBudgetByUserId(userId, month);

        if (budget == null)
        {
            throw new ArgumentException("Budget not found for the specified user and month");
        }

        var expenses = await _expenseRepository.GetMonthlyExpensesAsync(userId, currentMonth);
        var totalExpenses = expenses?.Sum(e => e?.Amount) ?? 0;

        return totalExpenses >= (budget.BudgetAmount * (budget.AlertThreshold / 100));
    }

    public async Task<bool> DeleteBudgetAsync(Guid budgetId, Guid userId)
    {
        var existingUser = await _userRepository.GetByIdAsync(userId);

        if (existingUser == null)
        {
            throw new ArgumentException("User not exists");
        }

        var budgetToDelete = await _budgetRepository.GetByIdAsync(budgetId);

        if (budgetToDelete == null || budgetToDelete.UserId != userId)
        {
            throw new ArgumentException("This user does not have the specified budget");
        }

        return await _budgetRepository.DeleteAsync(budgetId);
    }

    public async Task<Budget?> GetBudgetUserByMonthAsync(Guid userId, DateTime month)
    {
        var userExists = await _userRepository.GetByIdAsync(userId);

        if (userExists == null)
        {
            throw new ArgumentException("User not found");
        }

        return await _budgetRepository.GetMonthlyBudgetByUserId(userId, month);
    }

    public async Task<decimal> GetRemainingBudgetAsync(Guid userId)
    {
        var currentMonth = DateTime.UtcNow;
        var totalBudget = await _budgetRepository.GetBudgetByUserAsync(userId);

        if (totalBudget == 0)
        {
            throw new ArgumentException("No budget found for the specified user");
        }

        var expenses = await _expenseRepository.GetMonthlyExpensesAsync(userId, currentMonth);
        var totalExpenses = expenses?.Sum(e => e?.Amount) ?? 0;

        return totalBudget - totalExpenses;
    }

    public async Task<Budget> UpdateBudgetAsync(Guid userId, Guid budgetId, CreateBudget budget)
    {
        var existingBudget = await _userRepository.GetByIdAsync(userId);

        if (existingBudget == null)
        {
            throw new ArgumentException("User not exists");
        }

        var budgetToUpdate = await _budgetRepository.GetByIdAsync(budgetId);

        if (budgetToUpdate == null || budgetToUpdate.UserId != userId)
        {
            throw new ArgumentException("This user does not have the specified budget");
        }

        budgetToUpdate.Month = budget.Month;
        budgetToUpdate.BudgetAmount = budget.BudgetAmount;
        budgetToUpdate.AlertThreshold = budget.AlertThreshold;

        await _budgetRepository.UpdateAsync(budgetToUpdate);

        return budgetToUpdate;
    }
}
