using Domain.Entities;

namespace ExpenseTrackerGroup3.Repositories.Interfaces;

public interface IIncomeRepository : IRepository<Income>
{
    Task<IEnumerable<Income>> GetMonthlyIncomeByUserId(Guid userId, int year, int month);
    Task<IEnumerable<Income>> GetMonthlyIncomeByUserId(Guid userId, DateTime date);
}
