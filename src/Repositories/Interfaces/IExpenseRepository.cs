using Domain.Entities;

namespace ExpenseTrackerGroup3.Repositories.Interfaces;

public interface IExpenseRepository : IRepository<Expense>
{
    Task<string?> GetHighestSpendingCategoryByUserId(Guid userId);
    Task<DateTime> GetMostExpensiveMonthByUserId(Guid userId);
    Task<IEnumerable<Expense?>> GetMonthlyExpensesAsync(Guid userId, DateTime month);
    Task<IEnumerable<Expense>> GetAllByUserId(Guid userId);
}
