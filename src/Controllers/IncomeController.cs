using Domain.DTOs;
using Domain.Entities;

using ExpenseTrackerGroup3.Services.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerGroup3.Controllers;

[Authorize]
public class IncomeController : BaseController
{
  private readonly IIncomeService _incomeService;
  
  public IncomeController(IIncomeService incomeService)
  {
    _incomeService = incomeService;
  }

  [HttpPost("{userId}")]
  [ProducesResponseType(typeof(ResponseIncome), StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> AddIncome(Guid userId, [FromBody] CreateIncome income)
  {
    try
    {
      var newIncome = await _incomeService.AddIncomeAsync(userId, income);

      return Ok(newIncome);
    }
    catch (Exception e)
    {
      return HandleException(e);
    }
  }

  [HttpGet("{userId}")]
  [ProducesResponseType(typeof(ResponseIncome), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<IActionResult> GetIncomesByUserId(Guid userId)
  {
    try
    {
      IEnumerable<Income> income = await _incomeService.GetIncomesByUserIdAsync(userId);

      return Ok(income);
    }
    catch (Exception e)
    {
      return HandleException(e);
    }
  }

  [HttpGet("{userId}/{month}")]
  [ProducesResponseType(typeof(ResponseIncome), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<IActionResult> GetMonthlyIncome(Guid userId, DateTime month)
  {
    try
    {
      var income = await _incomeService.GetMonthlyIncomeByUserId(userId, month);

      return Ok(income);
    }
    catch (Exception e)
    {
      return HandleException(e);
    }
  }

  [HttpPut("{userId}")]
  [ProducesResponseType(typeof(ResponseIncome), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<IActionResult> UpdateIncome(Guid userId, [FromBody] UpdateIncome income)
  {
    try
    {
      var updatedIncome = await _incomeService.UpdateIncomeAsync(userId, income);

      return Ok(updatedIncome);
    }
    catch (Exception e)
    {
      return HandleException(e);
    }
  }
}
