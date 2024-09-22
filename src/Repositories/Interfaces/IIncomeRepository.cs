using Domain.Entities;

namespace ExpenseTrackerGroup3.Repositories.Interfaces;

public interface IIncomeRepository : IRepository<Income>
{
    Task<Income?> GetMonthlyIncomeByUserId(Guid userId, DateTime month);
}
