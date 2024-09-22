using Domain.DTOs;
using Domain.Entities;

using ExpenseTrackerGroup3.Repositories.Interfaces;
using ExpenseTrackerGroup3.Services.Interfaces;

namespace ExpenseTrackerGroup3.Services;

public class BudgetService : IBudgetService
{
    private readonly IBudgetRepository _budgetRepository;
    private readonly IUserRepository _userRepository;

    public BudgetService(IBudgetRepository budgetRepository, IUserRepository userRepository)
    {
        _budgetRepository = budgetRepository;
        _userRepository = userRepository;
    }

    public async Task<Budget> AddBudgetAsync(Guid userId, CreateBudget budget)
    {
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

    public Task<bool> CheckBudgetStatusAsync(Guid userId, DateTime month)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteBudgetAsync(Guid budgetId)
    {
        var budgetExist = _budgetRepository.GetByIdAsync(budgetId);

        if (budgetExist == null)
        {
            throw new ArgumentException("Budget not found");
        }

        return await _budgetRepository.DeleteAsync(budgetId);
    }

    public async Task<Budget?> GetBudgetUserByMonthAsync(Guid userId, DateTime month)
    {
        var userExists = _userRepository.GetByIdAsync(userId);

        if (userExists == null)
        {
            throw new ArgumentException("User not found");
        }

        return await _budgetRepository.GetMonthlyBudgetByUserId(userId, month);
    }

    public async Task<decimal> GetRemainingBudgetAsync(Guid userId)
    {
        var currentMonth = DateTime.UtcNow;
        var budget = await _budgetRepository.GetMonthlyBudgetByUserId(userId, currentMonth);

        if (budget == null)
        {
            throw new ArgumentException("Budget user not found");
        }

        
    }

    public async Task<bool> UpdateBudgetAsync(Guid budgetId, CreateBudget budget)
    {
        var existingBudget = await _budgetRepository.GetByIdAsync(budgetId);

        if (existingBudget == null)
        {
            return false;
        }

        existingBudget.Month = budget.Month;
        existingBudget.BudgetAmount = budget.BudgetAmount;
        existingBudget.AlertThreshold = budget.AlertThreshold;

        return await _budgetRepository.UpdateAsync(existingBudget);
    }
}
