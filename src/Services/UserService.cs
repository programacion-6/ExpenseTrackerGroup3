
using Domain.Entities;

using ExpenseTrackerGroup3.Domain.DTOs;
using ExpenseTrackerGroup3.Exceptions;
using ExpenseTrackerGroup3.Repositories.Interfaces;
using ExpenseTrackerGroup3.Services.Interfaces;
using ExpenseTrackerGroup3.Utils.Exception;
using ExpenseTrackerGroup3.Validators.UserValidator;

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
        var userValidator = new UserValidator();
        var validateResult = await userValidator.ValidateAsync(user);
        validateResult.ThrowIfValidationFailed();

        var userExists = await _userRepository.GetByIdAsync(userId);
        userExists.ThrowIfNull("User not found");

        var emailExists = await _userRepository.GetByEmailAsync(user.Email);
        emailExists.ThrowIfExists("Email already exists");

        var updatedUser = user.ToDomain(userExists!);
        var success = await _userRepository.UpdateAsync(updatedUser);
        success.ThrowIfOperationFailed("Failed to update user");

        return updatedUser;
    }

    public async Task<IEnumerable<MonthlySummaryDTO>> GetMonthlySummaryAsync(Guid userId, DateTime startDate, DateTime endDate)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        user.ThrowIfNull("User not found");

        ValidateDateRange(startDate, endDate);

        var monthlySummaries = new List<MonthlySummaryDTO>();

        for (var year = startDate.Year; year <= endDate.Year; year++)
        {
            var startMonth = year == startDate.Year ? startDate.Month : 1;
            var endMonth = year == endDate.Year ? endDate.Month : 12;

            for (var month = startMonth; month <= endMonth; month++)
            {
                var summary = await GetMonthlySummary(userId, year, month);
                if (summary != null)
                {
                    monthlySummaries.Add(summary);
                }
            }
        }

        monthlySummaries.ThrowIfEmpty("No monthly summaries found for the specified date range");

        return monthlySummaries;
    }

    private void ValidateDateRange(DateTime startDate, DateTime endDate)
    {
        if (startDate.Year > endDate.Year)
        {
            throw new BadRequestException("The initial year must be less than the final year.");
        }

        if (startDate.Year == endDate.Year && startDate.Month >= endDate.Month)
        {
            throw new BadRequestException("When years are the same, the initial month must be less than the final month.");
        }
    }

    private async Task<MonthlySummaryDTO?> GetMonthlySummary(Guid userId, int year, int month)
    {
        var date = new DateTime(year, month, 1);
        var incomes = await _incomeRepository.GetMonthlyIncomeByUserId(userId, date.Year, date.Month);
        var expenses = await _expenseRepository.GetMonthlyExpenseByUserId(userId, date);
        var budget = await _budgetRepository.GetMonthlyBudgetByUserId(userId, date);

        decimal totalIncome = incomes.Sum(income => income.Amount);
        decimal totalExpense = expenses.Sum(expense => expense.Amount);
        decimal totalBudget = budget?.BudgetAmount ?? 0;
        decimal remainingBudget = totalBudget - totalExpense;

        if (totalIncome > 0 || totalExpense > 0 || totalBudget > 0)
        {
            return new MonthlySummaryDTO(date, totalIncome, totalExpense, totalBudget, remainingBudget);
        }

        return null;
    }
}
