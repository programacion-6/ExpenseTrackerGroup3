
using Domain.DTOs;
using Domain.Entities;

using ExpenseTrackerGroup3.Exceptions;
using ExpenseTrackerGroup3.Repositories.Interfaces;
using ExpenseTrackerGroup3.Services.Interfaces;
using ExpenseTrackerGroup3.Utils.EmailSender;
using ExpenseTrackerGroup3.Utils.Exception;
using ExpenseTrackerGroup3.Utils.NotifyMilestone.Interfaces;
using ExpenseTrackerGroup3.Validators.GoalValidator;

namespace ExpenseTrackerGroup3.Services;

public class GoalService : IGoalService
{
    private readonly IGoalRepository _goalRepository;
    private readonly IUserRepository _userRepository;
    private readonly IGoalNotifyService _goalNotifyService;

    public GoalService(
        IGoalRepository goalRepository, 
        IUserRepository userRepository, 
        IGoalNotifyService goalNotifyService)
    {
        _goalRepository = goalRepository;
        _userRepository = userRepository;
        _goalNotifyService = goalNotifyService;
    }

    public async Task<Goal> CreateGoalAsync(Guid userId, CreateGoal goal)
    {
        var goalValidator = new GoalValidator();
        var validationResult = await goalValidator.ValidateAsync(goal);
        validationResult.ThrowIfValidationFailed();

        var userExists = await _userRepository.GetByIdAsync(userId);
        userExists.ThrowIfNull("User not found");

        var newGoal = new Goal
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            GoalAmount = goal.GoalAmount,
            DeadLine = goal.DeadLine,
            CurrentAmount = goal.CurrentAmount,
            CreatedAt = DateTime.Now
        };

        var success = await _goalRepository.CreateAsync(newGoal);
        success.ThrowIfOperationFailed("Failed to create goal");

        return newGoal;
    }

    public async Task<bool> DeleteGoalAsync(Guid goalId, Guid userId)
    {
        var userExists = await _userRepository.GetByIdAsync(userId);
        userExists.ThrowIfNull("User not found");

        var goalExists = await _goalRepository.GetByIdAsync(goalId);
        if (goalExists == null || goalExists.UserId != userId)
        {
            throw new BadRequestException("This goal does not exist or Goal belongs to another user");
        }

        return await _goalRepository.DeleteAsync(goalId);
    }

    public async Task<IEnumerable<Goal>> GetActiveGoalsAsync(Guid userId)
    {
        var userExists = await _userRepository.GetByIdAsync(userId);
        userExists.ThrowIfNull("User not found");

        return await _goalRepository.GetActiveGoalsByUserIdAsync(userId);
    }

    public async Task<Goal> GetGoalByIdAsync(Guid goalId)
    {
        var goalExists = await _goalRepository.GetByIdAsync(goalId);
        goalExists.ThrowIfNull("Goal not found");

        return goalExists!;
    }

    public async Task<IEnumerable<Goal>> GetGoalsAsync(Guid userId)
    {
        var userExists = await _userRepository.GetByIdAsync(userId);
        userExists.ThrowIfNull("User not found");

        var goals = await _goalRepository.GetGoalsByUserIdAsync(userId);
        return goals;
    }

    public async Task<decimal> TrackGoalProgressAsync(Guid userId, Guid goalId)
    {
        var userExists = await _userRepository.GetByIdAsync(userId);
        userExists.ThrowIfNull("User not found");

        var goal = await _goalRepository.GetByIdAsync(goalId);
        if (goal == null || goal.UserId != userId)
        {
            throw new BadRequestException("This goal does not exist or belongs to another user");
        }

        var goalProgress = await _goalRepository.GetGoalProgressAsync(goalId);
        return goalProgress < 100 ? goalProgress : 100;
    }

    public async Task<Goal> UpdateGoalProgressAsync(Guid userId, Guid goalId, decimal amount)
    {
        var userExists = await _userRepository.GetByIdAsync(userId);
        userExists.ThrowIfNull("User not found");

        var goal = await _goalRepository.GetByIdAsync(goalId);
        if (goal == null || goal.UserId != userId)
        {
            throw new BadRequestException("This goal does not exist or belongs to another user");
        }

        goal.CurrentAmount += amount;
        await _goalRepository.UpdateAsync(goal);

        var goalProgress = goal.CurrentAmount / goal.GoalAmount * 100;
        await NotifyUserOnMilestoneAsync(userExists, goalProgress, goal);

        return goal;
    }

    public async Task<Goal> UpdateGoalAsync(Guid userId, Guid goalId, CreateGoal goal)
    {
        var goalValidator = new GoalValidator();
        var validationResult = await goalValidator.ValidateAsync(goal);
        validationResult.ThrowIfValidationFailed();
           
        var userExists = await _userRepository.GetByIdAsync(userId);
        userExists.ThrowIfNull("User not found");

        var existingGoal = await _goalRepository.GetByIdAsync(goalId);
        if (existingGoal == null || existingGoal.UserId != userId)
        {
            throw new BadRequestException("This goal does not exist or belongs to another user");
        }

        existingGoal.GoalAmount = goal.GoalAmount;
        existingGoal.DeadLine = goal.DeadLine;
        existingGoal.CurrentAmount = goal.CurrentAmount;

        var success = await _goalRepository.UpdateAsync(existingGoal);
        success.ThrowIfOperationFailed("Failed to update goal");

        return existingGoal;
    }

    private async Task NotifyUserOnMilestoneAsync(User user, decimal goalProgress, Goal goal)
    {
        await _goalNotifyService.NotifyUserOnMilestoneAsync(user, goalProgress, goal);
    }
}
