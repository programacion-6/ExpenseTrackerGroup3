using Domain.Entities;

namespace ExpenseTrackerGroup3.Repositories.Interfaces;

public interface IBudgetRepository : IRepository<Budget>
{
    Task<Budget?> GetMonthlyBudgetByUserId(Guid userId, DateTime month);
    Task<Budget?> GetSpecificBudgetByUserIdAsync(Guid userId, Guid budgetId);
    Task<Budget?> GetCurrentBudgetAsync(Guid userId);
    Task<IEnumerable<Budget>> GetAllUsersBudgetsAsync(Guid userId);
}
