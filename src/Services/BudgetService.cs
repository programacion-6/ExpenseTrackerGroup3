
using Domain.DTOs;
using Domain.Entities;

using ExpenseTrackerGroup3.Services.Interfaces;

namespace ExpenseTrackerGroup3.Services;

public class BudgetService : IBudgetService
{
    private readonly IBudgetService _budgetService;

    public BudgetService(IBudgetService budgetService)
    {
        _budgetService = budgetService;
    }

    public Task<Budget> AddBudgetAsync(Guid userId, CreateBudget budget)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CheckBudgetStatusAsync(Guid userId, DateTime month)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteBudgetAsync(Guid budgetId)
    {
        throw new NotImplementedException();
    }

    public Task<Budget> GetBudgetUserByMonthAsync(Guid userId, DateTime month)
    {
        throw new NotImplementedException();
    }

    public Task<decimal> GetRemainingBudgetAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateBudgetAsync(Guid budgetId, CreateBudget budget)
    {
        throw new NotImplementedException();
    }
}
