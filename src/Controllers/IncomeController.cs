using Domain.DTOs;
using Domain.Entities;

using ExpenseTrackerGroup3.Services.Interfaces;

using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerGroup3.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/users/{userId}/income")]
public class IncomeController : ControllerBase
{
    private readonly IIncomeService _incomeService;

    public IncomeController(IIncomeService incomeService)
    {
        _incomeService = incomeService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResponseIncome), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddIncome(Guid userId, [FromBody] CreateIncome income)
    {
        var newIncome = await _incomeService.AddIncomeAsync(userId, income);
        return Ok(newIncome);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseIncome), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetIncomesByUserId(Guid userId)
    {
        IEnumerable<Income> income = await _incomeService.GetIncomesByUserIdAsync(userId);
        return Ok(income);
    }

    [HttpGet("{month}")]
    [ProducesResponseType(typeof(ResponseIncome), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMonthlyIncome(Guid userId, DateTime month)
    {
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
