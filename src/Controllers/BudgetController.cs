using Domain.DTOs;

using ExpenseTrackerGroup3.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerGroup3.Controllers;

public class BudgetController : BaseController
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
        try
        {
            var newBudget = await _budgetService.AddBudgetAsync(userId, budget);
            var response = ResponseBudget.FromDomain(newBudget);
            return CreatedAtAction(nameof(GetMonthlyBudget), new { userId, month = budget.Month }, response);
        }
        catch (Exception e)
        {
            return HandleException(e);
        }

    }

    [HttpGet("{userId}/{month}")]
    [ProducesResponseType(typeof(ResponseBudget), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMonthlyBudget(Guid userId, DateTime month)
    {
        try
        {
            var budget = await _budgetService.GetBudgetUserByMonthAsync(userId, month);
            var response = ResponseBudget.FromDomain(budget);
            return Ok(response);
        }
        catch (Exception e)
        {
            return HandleException(e);
        }
    }

    [HttpPut("{userId}")]
    [ProducesResponseType(typeof(ResponseBudget), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateBudget(Guid userId, [FromBody] CreateBudget budget)
    {
        try
        {
            var updatedBudget = await _budgetService.UpdateBudgetAsync(userId, budget);
            var response = ResponseBudget.FromDomain(budget.ToDomain());
            return Ok(response);
        }
        catch (Exception e)
        {
            return HandleException(e);
        }
    }

    [HttpDelete("{userId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBudget(Guid userId)
    {
        try
        {
            await _budgetService.DeleteBudgetAsync(userId);
            return NoContent();
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
        try
        {
            var remainingBudget = await _budgetService.GetRemainingBudgetAsync(userId);
            return Ok(remainingBudget);
        }
        catch (Exception e)
        {
            return HandleException(e);
        }
    }

    [HttpGet("{userId}/{month}/status")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CheckBudgetStatus(Guid userId, DateTime month)
    {
        try
        {
            var status = await _budgetService.CheckBudgetStatusAsync(userId, month);
            return Ok(status);
        }
        catch (Exception e)
        {
            return HandleException(e);
        }
    }
}
