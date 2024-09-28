using Domain.DTOs;
using Domain.Entities;

using ExpenseTrackerGroup3.Exceptions;
using ExpenseTrackerGroup3.Repositories.Interfaces;
using ExpenseTrackerGroup3.Services.Interfaces;
using ExpenseTrackerGroup3.Utils.Exception;

namespace ExpenseTrackerGroup3.Services;

public class IncomeService : IIncomeService
{
    private readonly IIncomeRepository _incomeRepository;
    private readonly IUserRepository _userRepository;

    public IncomeService(IIncomeRepository incomeRepository, IUserRepository userRepository)
    {
        _incomeRepository = incomeRepository;
        _userRepository = userRepository;
    }

    public async Task<Income> AddIncomeAsync(Guid userId, CreateIncome income)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        user.ThrowIfNull("User not found");

        var newIncome = new Income
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Amount = income.Amount,
            Source = income.Source,
            CreatedAt = DateTime.Now
        };

        var success = await _incomeRepository.CreateAsync(newIncome);
        success.ThrowIfOperationFailed("Failed to create income");

        return newIncome;
    }

    public async Task<IEnumerable<Income>> GetIncomesByUserIdAsync(Guid userId)
    {

        User? user = await _userRepository.GetByIdAsync(userId);
        user.ThrowIfNull("User not found");

        IEnumerable<Income> income = await _incomeRepository.GetAllAsync();

        return income.Where(i => i.UserId == userId);
    }

    public async Task<IEnumerable<Income>> GetMonthlyIncomeByUserId(Guid userId, DateTime date)
    {
        User? user = await _userRepository.GetByIdAsync(userId);
        user.ThrowIfNull("User not found");

       return await _incomeRepository.GetMonthlyIncomeByUserId(userId, date.Year, date.Month);
    }

    public async Task<Income> UpdateIncomeAsync(Guid incomeId, UpdateIncome income)
    {
        var existingIncome = await _incomeRepository.GetByIdAsync(incomeId);
        existingIncome.ThrowIfNull("Income not found");
        
        existingIncome = income.ToDomain(existingIncome!);
        var success = await _incomeRepository.UpdateAsync(existingIncome);
        success.ThrowIfOperationFailed("Failed to update income");

        return existingIncome;
    }   
}
