using Domain.Entities;

namespace ExpenseTrackerGroup3.Repositories.Interfaces;

public interface IExpenseRepository : IRepository<Expense>
{
    Task<string?> GetHighestSpendingCategoryByUserId(Guid userId);
    Task<DateTime> GetMostExpensiveMonthByUserId(Guid userId);
}
