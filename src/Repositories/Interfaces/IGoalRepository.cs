using Domain.Entities;

namespace ExpenseTrackerGroup3.Repositories.Interfaces;

public interface IGoalRepository : IRepository<Goal>
{
    Task<IEnumerable<Goal>> GetActiveGoalsByUserId(Guid id);
    Task<decimal> GetGoalProgress(Guid id);
}
