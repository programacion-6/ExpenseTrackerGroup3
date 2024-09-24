using Domain.DTOs;
using Domain.Entities;

namespace ExpenseTrackerGroup3.Services.Interfaces;

public interface IExpenseService
{
    Task<Expense> AddExpenseAsync(Guid userId, CreateExpense expense);
    Task<IEnumerable<Expense>> GetUserExpensesByCategoryAsync(Guid userId, DateTime month, string category);
    Task<IEnumerable<Expense>> GetExpenseByUserIdAsync(Guid userId);
    Task<Expense> UpdateExpenseAsync(Guid userId, Guid expenseId, CreateExpense expense);
    Task<bool> DeleteExpense(Guid userId, Guid expenseId);
    Task<string> GetHighestExpeseUserCategoryAsync(Guid userId);
    Task<string> GetUserMostExpensiveMonth(Guid userId);
    Task<IEnumerable<Expense>> GetUserRecurringExpense(Guid userId);
}