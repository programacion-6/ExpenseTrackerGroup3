using Domain.DTOs;
using Domain.Entities;

namespace ExpenseTrackerGroup3.Services.Interfaces;

public interface IGoalService
{
    Task<Goal> CreateGoalAsync(Guid userId, CreateGoal goal);
    Task<IEnumerable<Goal>> GetGoalsAsync(Guid userId);
    Task<IEnumerable<Goal>> GetActiveGoalsAsync(Guid userId);
    Task<Goal> GetGoalByIdAsync(Guid goalId);
    Task<decimal> TrackGoalProgressAsync(Guid userId, Guid goalId);
    Task<Goal> UpdateGoalProgressAsync(Guid userId, Guid goalId, decimal amount);
    Task<Goal> UpdateGoalAsync(Guid userId, Guid goalId, CreateGoal goal);
    Task<bool> DeleteGoalAsync(Guid goalId, Guid userId);
}
