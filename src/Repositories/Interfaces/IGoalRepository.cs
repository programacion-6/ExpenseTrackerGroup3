using Domain.Entities;

namespace ExpenseTrackerGroup3.Repositories.Interfaces;

public interface IGoalRepository : IRepository<Goal>
{
    Task<IEnumerable<Goal>> GetGoalsByUserIdAsync(Guid userId);
    Task<IEnumerable<Goal>> GetActiveGoalsByUserIdAsync(Guid userId);
    Task<decimal> GetGoalProgressAsync(Guid id);
}
