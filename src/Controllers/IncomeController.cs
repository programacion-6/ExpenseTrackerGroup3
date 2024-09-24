using Domain.DTOs;
using ExpenseTrackerGroup3.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerGroup3.Controllers;

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
      var response = ResponseIncome.FromDomain(newIncome);
      return CreatedAtAction(nameof(GetMonthlyIncome), new { userId, month = DateTime.Now }, response);
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
      var response = ResponseIncome.FromDomain(income);
      return Ok(response);
    }
    catch (Exception e)
    {
      return HandleException(e);
    }
  }

  [HttpPut("{userId}/{incomeId}")]
  [ProducesResponseType(typeof(ResponseIncome), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<IActionResult> UpdateIncome(Guid userId, Guid incomeId, [FromBody] CreateIncome income)
  {
    try
    {
      var updatedIncome = await _incomeService.UpdateIncomeAsync(userId, incomeId, income);
      var response = ResponseIncome.FromDomain(updatedIncome);
      return Ok(response);
    }
    catch (Exception e)
    {
      return HandleException(e);
    }
  }

  [HttpDelete("{incomeId}/{userId}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<IActionResult> DeleteIncome(Guid incomeId, Guid userId)
  {
    try
    {
      var success = await _incomeService.DeleteIncomeAsync(incomeId, userId);
      if (success)
      {
        return NoContent();
      }
      return NotFound();
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
      var income = await _incomeService.GetIncomesByUserIdAsync(userId);
      var response = ResponseIncome.FromDomain(income);
      return Ok(response);
    }
    catch (Exception e)
    {
      return HandleException(e);
    }
  }
}
