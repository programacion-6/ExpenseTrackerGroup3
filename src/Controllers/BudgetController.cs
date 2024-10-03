using Domain.DTOs;

using ExpenseTrackerGroup3.Domain.DTOs;
using ExpenseTrackerGroup3.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerGroup3.Controllers;

[Route("api/v1/users/budgets")]
public class BudgetController : ApiControllerBase
{
    private readonly IBudgetService _budgetService;

    public BudgetController(IBudgetService budgetService)
    {
        _budgetService = budgetService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResponseBudget), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddBudget([FromBody] CreateBudget budget)
    {
        var userId = GetAuthenticatedUserId();
        var newBudget = await _budgetService.AddBudgetAsync(userId, budget);
        var response = ResponseBudget.FromDomain(newBudget);
        return CreatedAtAction(nameof(GetMonthlyBudget), new { response.Month }, response);
    }

    [HttpGet("{month}")]
    [ProducesResponseType(typeof(ResponseBudget), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMonthlyBudget(DateTime month)
    {
        var userId = GetAuthenticatedUserId();
        var budget = await _budgetService.GetBudgetUserByMonthAsync(userId, month);
        var response = ResponseBudget.FromDomain(budget!);
        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(typeof(ResponseBudget), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateBudget([FromBody] CreateBudget budget)
    {
        var userId = GetAuthenticatedUserId();
        var updatedBudget = await _budgetService.UpdateBudgetAsync(userId, budget);
        var response = ResponseBudget.FromDomain(updatedBudget);
        return Ok(response);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBudget(Guid budgetId)
    { 
        var userId = GetAuthenticatedUserId();
        await _budgetService.DeleteBudgetAsync(userId, budgetId);
        const string succesfullyMessage = "Bugdet deleted succesfully";
        return Ok(succesfullyMessage);
    }


    [HttpGet("remaining")]
    [ProducesResponseType(typeof(decimal), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRemainingBudget()
    {
        var userId = GetAuthenticatedUserId();
        var remainingBudget = await _budgetService.GetRemainingBudgetAsync(userId);
        return Ok($"Current month remaining budget: {remainingBudget}");
    }

    [HttpGet("month/status")]
    [ProducesResponseType(typeof(BudgetStatus), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CheckBudgetStatus() 
    {
        var userId = GetAuthenticatedUserId();
        var status = await _budgetService.CheckBudgetStatusAsync(userId);
        return Ok(status);
    }
}
