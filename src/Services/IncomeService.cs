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

    public async Task<Income> UpdateIncomeAsync(Guid userId, UpdateIncome income)
    {
        var existingUser = await _userRepository.GetByIdAsync(userId);

        if (existingUser == null)
        {
            throw new ArgumentException("User not found");
        }

        IEnumerable<Income> incomes = await _incomeRepository.GetAllAsync();

        IEnumerable<Income> userIncomes = incomes.Where(i => i.UserId == userId);

        Income? incomeToUpdate = userIncomes.FirstOrDefault(i => i.Id == income.Id);

        if (incomeToUpdate == null)
        {
            throw new ArgumentException("Income not found");
        }
        
        incomeToUpdate = income.ToDomain(incomeToUpdate);

        var success = await _incomeRepository.UpdateAsync(incomeToUpdate);

        if (!success)
        {
            throw new Exception("Failed to update income");
        }

        return incomeToUpdate;
    }
  
}
