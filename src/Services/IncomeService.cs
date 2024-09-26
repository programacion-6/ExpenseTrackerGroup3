using Domain.DTOs;
using Domain.Entities;

using ExpenseTrackerGroup3.Repositories.Interfaces;
using ExpenseTrackerGroup3.Services.Interfaces;

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

        if (user == null)
        {
            throw new ArgumentException("User not found");
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

    public async Task<IEnumerable<Income>> GetIncomesByUserIdAsync(Guid userId)
    {

        User? user = await _userRepository.GetByIdAsync(userId);

        if (user == null) 
        {
            throw new ArgumentException("User not found");
        }

        IEnumerable<Income> income = await _incomeRepository.GetAllAsync();

        return income.Where(i => i.UserId == userId);
    }

    public async Task<IEnumerable<Income>> GetMonthlyIncomeByUserId(Guid userId, DateTime month)
    {
        User? user = await _userRepository.GetByIdAsync(userId);

        if (user == null)
        {
            throw new ArgumentException("User not found");
        }

       IEnumerable<Income> incomes = await _incomeRepository.GetAllAsync();

       return incomes.Where(i => i.UserId == userId && i.CreatedAt.Month == month.Month);
    }

    public async Task<Income> UpdateIncomeAsync(Guid incomeId, UpdateIncome income)
    {
        var existingIncome = await _incomeRepository.GetByIdAsync(incomeId);

        if (existingIncome == null)
            {
                throw new ArgumentException("Income not found");
            }
        
        existingIncome = income.ToDomain(existingIncome);
        var success = await _incomeRepository.UpdateAsync(existingIncome);

        if (!success)
            {
                throw new Exception("Failed to update income");
            }

        return existingIncome;
    }   
}
