using Domain.DTOs;
using Domain.Entities;

using ExpenseTrackerGroup3.Domain.DTOs;

namespace ExpenseTrackerGroup3.Services.Interfaces;

public interface IBudgetService
{
    Task<Budget> AddBudgetAsync(Guid userId, CreateBudget budget);
    Task<Budget?> GetBudgetUserByMonthAsync(Guid userId, DateTime month);
    Task<Budget> UpdateBudgetAsync(Guid userId, CreateBudget budget);
    Task<bool> DeleteBudgetAsync(Guid userId, Guid budgetId);
    Task<decimal> GetRemainingBudgetAsync(Guid userId);
    Task<BudgetStatus> CheckBudgetStatusAsync(Guid userId);
}
