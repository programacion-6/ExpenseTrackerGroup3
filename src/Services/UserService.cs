
using Domain.Entities;

using ExpenseTrackerGroup3.Domain.DTOs;
using ExpenseTrackerGroup3.Exceptions;
using ExpenseTrackerGroup3.Repositories.Interfaces;
using ExpenseTrackerGroup3.Services.Interfaces;
using ExpenseTrackerGroup3.Utils.Exception;

namespace ExpenseTrackerGroup3.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IExpenseRepository _expenseRepository;
    private readonly IIncomeRepository _incomeRepository;
    private readonly IBudgetRepository _budgetRepository;

    public UserService(IUserRepository userRepository, IExpenseRepository expenseRepository, IIncomeRepository incomeRepository, IBudgetRepository budgetRepository)
    {
        _userRepository = userRepository;
        _expenseRepository = expenseRepository;
        _incomeRepository = incomeRepository;
        _budgetRepository = budgetRepository;
    }
    
    public async Task<User?> GetUserProfileAsync(Guid userId)
    {
        var userExists = await _userRepository.GetByIdAsync(userId);
        userExists.ThrowIfNull("User not found");
        return userExists;
    }

    public async Task<User> UpdateUserProfileAsync(Guid userId, UpdateUserDTO user)
    {
        var userExists = await _userRepository.GetByIdAsync(userId);
        userExists.ThrowIfNull("User not found");

        var updatedUser = user.ToDomain(userExists!);
        var success = await  _userRepository.UpdateAsync(updatedUser);
        success.ThrowIfOperationFailed("Failed to update user");
        
        return updatedUser;
    }

    public async Task<IEnumerable<MonthlySummaryDTO>> GetMonthlySummaryAsync(Guid userId, DateTime startDate, DateTime endDate)
    {
        if (userId == Guid.Empty)
        {
            throw new ArgumentException("User ID cannot be empty", nameof(userId));
        }

        if (startDate.Year >= endDate.Year)
        {
            throw new ArgumentException("The initial year must be less than the final year.", nameof(startDate));
        }

        if (startDate.Month >= endDate.Month)
        {
            throw new ArgumentException("The initial month must be less than the final month.", nameof(startDate));
        }

        try
        {
            var monthlySummaries = new List<MonthlySummaryDTO>();

            for (var year = startDate.Year; year <= endDate.Year; year++)
            {
                var startMonth = year == startDate.Year ? startDate.Month : 1;
                var endMonth = year == endDate.Year ? endDate.Month : 12;

                for (var month = startMonth; month <= endMonth; month++)
                {
                    IEnumerable<Income> incomes = await _incomeRepository.GetMonthlyIncomeByUserId(userId, new DateTime(year, month, 1));
                    IEnumerable<Expense> expenses = await _expenseRepository.GetMonthlyExpenseByUserId(userId, new DateTime(year, month, 1));
                    Budget? budget = await _budgetRepository.GetMonthlyBudgetByUserId(userId, new DateTime(year, month, 1));

                    decimal totalIncome = incomes.Sum(income => income.Amount);
                    decimal totalExpense = expenses.Sum(expense => expense.Amount);
                    decimal totalBudget = budget?.BudgetAmount ?? 0;

                    MonthlySummaryDTO summary = new(new DateTime(year, month, 1), totalIncome, totalExpense, totalBudget);

                    monthlySummaries.Add(summary);
                }
            }

            return monthlySummaries;
        }
        catch (Exception)
        {
            throw new BadRequestException("Error when obtaining the monthly summary");
        }
    }
}
