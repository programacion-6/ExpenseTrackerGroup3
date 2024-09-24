using Domain.DTOs;
using Domain.Entities;

namespace ExpenseTrackerGroup3.Services.Interfaces;

public interface IIncomeService
{
    Task<Income> AddIncomeAsync(Guid userid, CreateIncome income);
    Task<Income> GetIncomesByUserIdAsync(Guid userId);
    Task<Income> GetMonthlyIncomeByUserId(Guid userId, DateTime month);
    Task<Income> UpdateIncomeAsync(Guid userId, Guid incomeId, CreateIncome income);
    Task<bool> DeleteIncomeAsync(Guid incomeId ,Guid userId);
}
