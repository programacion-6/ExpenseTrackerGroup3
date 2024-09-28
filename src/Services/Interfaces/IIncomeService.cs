using Domain.DTOs;
using Domain.Entities;

namespace ExpenseTrackerGroup3.Services.Interfaces;

public interface IIncomeService
{
    Task<Income> AddIncomeAsync(Guid userid, CreateIncome income);
    Task<IEnumerable<Income>> GetIncomesByUserIdAsync(Guid userId);
    Task<IEnumerable<Income>> GetMonthlyIncomeByUserId(Guid userId, DateTime date);
    Task<Income> UpdateIncomeAsync(Guid incomeId, UpdateIncome income);
}
