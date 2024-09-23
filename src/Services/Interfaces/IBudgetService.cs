using Domain.DTOs;
using Domain.Entities;

namespace ExpenseTrackerGroup3.Services.Interfaces;

public interface IBudgetService
{
    Task<Budget> AddBudgetAsync(Guid userId, CreateBudget budget);
    Task<Budget?> GetBudgetUserByMonthAsync(Guid userId, DateTime month);
    Task<bool> UpdateBudgetAsync(Guid userId, Guid budgetId, CreateBudget budget);
    Task<bool> DeleteBudgetAsync(Guid budgetId, Guid userId);
    Task<decimal> GetRemainingBudgetAsync(Guid userId);
    Task<bool> CheckBudgetStatusAsync(Guid userId, DateTime month);
}
