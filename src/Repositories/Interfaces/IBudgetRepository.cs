using Domain.Entities;

namespace ExpenseTrackerGroup3.Repositories.Interfaces;

public interface IBudgetRepository : IRepository<Budget>
{
    Task<Budget?> GetMonthlyBudgetByUserId(Guid userId, DateTime month);
    Task<Budget?> GetBudgetByUserAsync(Guid userId);
    Task<IEnumerable<Budget>> GetBudgetsByUserAsync(Guid userId);
}
