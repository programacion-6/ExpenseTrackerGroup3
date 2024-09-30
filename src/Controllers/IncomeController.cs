using Domain.DTOs;
using Domain.Entities;
using ExpenseTrackerGroup3.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerGroup3.Controllers;

[Route("api/v1/users/income")]
public class IncomeController : ApiControllerBase
{
    private readonly IIncomeService _incomeService;

    public IncomeController(IIncomeService incomeService)
    {
        _incomeService = incomeService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResponseIncome), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddIncome([FromBody] CreateIncome income)
    {
        var userId = GetAuthenticatedUserId();
        var newIncome = await _incomeService.AddIncomeAsync(userId, income);
        return Ok(newIncome);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseIncome), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetIncomesByUserId()
    {
        var userId = GetAuthenticatedUserId();
        IEnumerable<Income> income = await _incomeService.GetIncomesByUserIdAsync(userId);
        return Ok(income);
    }

    [HttpGet("{month}")]
    [ProducesResponseType(typeof(ResponseIncome), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMonthlyIncome(DateTime month)
    {
        var userId = GetAuthenticatedUserId();
        var income = await _incomeService.GetMonthlyIncomeByUserId(userId, month);
        return Ok(income);
    }

    [HttpPut("{incomeId}")]
    [ProducesResponseType(typeof(ResponseIncome), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateIncome(Guid incomeId, [FromBody] UpdateIncome income)
    {
        var updatedIncome = await _incomeService.UpdateIncomeAsync(incomeId, income);
        return Ok(updatedIncome);
    }
}
