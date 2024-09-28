using Domain.DTOs;
using ExpenseTrackerGroup3.Services.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerGroup3.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/users/{userId}/goals")]
public class GoalController : ControllerBase
{
    private readonly IGoalService _goalService;

    public GoalController(IGoalService goalService)
    {
        _goalService = goalService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResponseGoal), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddGoal(Guid userId, [FromBody] CreateGoal goal)
    {
        var newGoal = await _goalService.CreateGoalAsync(userId, goal);
        var response = ResponseGoal.FromDomain(newGoal);
        return CreatedAtAction(nameof(GetActiveGoals), new { userId }, response);
    }

    [HttpPut("{goalId}")]
    [ProducesResponseType(typeof(ResponseGoal), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateGoal(Guid userId, Guid goalId, [FromBody] CreateGoal goal)
    {
        var updatedGoal = await _goalService.UpdateGoalAsync(userId, goalId, goal);
        var response = ResponseGoal.FromDomain(updatedGoal);
        return Ok(response);
    }

    [HttpGet("active")]
    [ProducesResponseType(typeof(IEnumerable<ResponseGoal>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetActiveGoals(Guid userId)
    {
        var activeGoals = await _goalService.GetActiveGoalsAsync(userId);
        var response = activeGoals.Select(ResponseGoal.FromDomain);
        return Ok(response);
    }

    [HttpDelete("{goalId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteGoal(Guid userId, Guid goalId)
    {
        var result = await _goalService.DeleteGoalAsync(goalId, userId);
        if (!result)
        {
            return NotFound("Goal not found or belongs to another user.");
        }
        return Ok("Goal deleted successfully.");
    }

    [HttpGet("{goalId}/progress")]
    [ProducesResponseType(typeof(decimal), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetGoalProgress(Guid userId, Guid goalId)
    {
        var progress = await _goalService.TrackGoalProgressAsync(userId, goalId);
        return Ok(progress);
    }

    [HttpPut("{goalId}/{amount}/progress")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateGoalProgress(Guid userId, Guid goalId, decimal amount)
    {
        var UpdateGoal = await _goalService.UpdateGoalProgressAsync(userId, goalId, amount);
        var response = ResponseGoal.FromDomain(UpdateGoal);
        return Ok(response);
    }
}
