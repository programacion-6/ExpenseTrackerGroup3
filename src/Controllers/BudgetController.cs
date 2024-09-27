using Domain.DTOs;

using ExpenseTrackerGroup3.Services.Interfaces;

using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerGroup3.Controllers;
[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class BudgetController : ControllerBase
{
    private readonly IBudgetService _budgetService;

    public BudgetController(IBudgetService budgetService)
    {
        _budgetService = budgetService;
    }

    [HttpPost("{userId}")]
    [ProducesResponseType(typeof(ResponseBudget), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddBudget(Guid userId, [FromBody] CreateBudget budget)
    {
        var newBudget = await _budgetService.AddBudgetAsync(userId, budget);
        var response = ResponseBudget.FromDomain(newBudget);
        return CreatedAtAction(nameof(GetMonthlyBudget), new { userId, month = budget.Month }, response);
    }

    [HttpGet("{userId}/{month}")]
    [ProducesResponseType(typeof(ResponseBudget), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMonthlyBudget(Guid userId, DateTime month)
    {
        var budget = await _budgetService.GetBudgetUserByMonthAsync(userId, month);
        var response = ResponseBudget.FromDomain(budget);
        return Ok(response);
    }

    [HttpPut("{userId}/{budgetId}")]
    [ProducesResponseType(typeof(ResponseBudget), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateBudget(Guid userId, Guid budgetId, [FromBody] CreateBudget budget)
    {
        var updatedBudget = await _budgetService.UpdateBudgetAsync(userId, budgetId, budget);
        var response = ResponseBudget.FromDomain(updatedBudget);
        return Ok(response);
    }

    [HttpDelete("{userId}/{budgetId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBudget(Guid budgetId, Guid userId)
    {
        try
        {
            await _budgetService.DeleteBudgetAsync(budgetId, userId);
            const string succesfullyMessage = "Bugdet deleted succesfully";
            return Ok(succesfullyMessage);
        }
        catch (Exception e)
        {
            return HandleException(e);
        }
    }


    [HttpGet("{userId}/remaining")]
    [ProducesResponseType(typeof(decimal), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRemainingBudget(Guid userId)
    {
        var remainingBudget = await _budgetService.GetRemainingBudgetAsync(userId);
        return Ok(remainingBudget);
    }

    [HttpGet("{userId}/{month}/status")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CheckBudgetStatus(Guid userId, DateTime month)
    {
        var status = await _budgetService.CheckBudgetStatusAsync(userId, month);
        return Ok(status);
    }
}
