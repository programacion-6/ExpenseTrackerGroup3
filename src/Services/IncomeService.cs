using Domain.DTOs;
using Domain.Entities;

using ExpenseTrackerGroup3.Repositories.Interfaces;
using ExpenseTrackerGroup3.Services.Interfaces;

namespace ExpenseTrackerGroup3.Services;

public class IncomeService : IIncomeService
{
    private readonly IIncomeRepository _incomeRepository;
    private readonly IUserRepository _userRepository;
    private readonly IExpenseRepository _expenseRepository;

    public IncomeService(IIncomeRepository incomeRepository, IUserRepository userRepository, IExpenseRepository expenseRepository)
    {
        _incomeRepository = incomeRepository;
        _userRepository = userRepository;
        _expenseRepository = expenseRepository;
    }

    public async Task<Income> AddIncomeAsync(Guid userId, CreateIncome income)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            throw new ArgumentException("User not found");
        }

        var alredyExists = await _incomeRepository.GetMonthlyIncomeByUserId(userId, DateTime.Now);

        if (alredyExists != null)
        {
            throw new ArgumentException("Income already exists for the user");
        }

        var newIncome = new Income
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Amount = income.Amount,
            Source = income.Source,
            CreatedAt = DateTime.Now
        };

        var success = await _incomeRepository.CreateAsync(newIncome);

        if (!success)
        {
            throw new Exception("Failed to create income");
        }

        return newIncome;
    }

    public async Task<bool> DeleteIncomeAsync(Guid incomeId, Guid userId)
    {
        var existingUser = await _userRepository.GetByIdAsync(userId);

        if (existingUser == null)
        {
            throw new ArgumentException("User not found");
        }

        var incomeToDelete = await _incomeRepository.GetByIdAsync(incomeId);

        if (incomeToDelete == null )
        {
            throw new ArgumentException("This income does not exist or does not belong to the user");
        }

        return await _incomeRepository.DeleteAsync(incomeId);
    }

    public async Task<Income> GetIncomesByUserIdAsync(Guid userId)
    {

        var income = await _incomeRepository.GetByIdAsync(userId);

        if (income == null)
        {
            throw new ArgumentException("Income not found");
        }

        return income;
    }

    public async Task<Income> GetMonthlyIncomeByUserId(Guid userId, DateTime month)
    {
        var income = await _incomeRepository.GetMonthlyIncomeByUserId(userId, month);

        if (income == null)
        {
            throw new ArgumentException("Income not found");
        }
        
        return income;

    }

    public async Task<Income> UpdateIncomeAsync(Guid userId, Guid incomeId, CreateIncome income)
    {
        var existingUser = await _userRepository.GetByIdAsync(userId);

        if (existingUser == null)
        {
            throw new ArgumentException("User not found");
        }

        var existingIncome = await _incomeRepository.GetByIdAsync(incomeId);

        if (existingIncome == null)
        {
            throw new ArgumentException("Income not found");
        }

        existingIncome.Amount = income.Amount;
        existingIncome.Source = income.Source;

        var success = await _incomeRepository.UpdateAsync(existingIncome);

        if (!success)
        {
            throw new Exception("Failed to update income");
        }

        return existingIncome;
    }
  
}
