using Domain.DTOs;

using ExpenseTrackerGroup3.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerGroup3.Controllers;

public class ExpenseController : BaseController
{
    private readonly IExpenseService _expenseService;

    public ExpenseController(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    [HttpPost("{userId}")]
    [ProducesResponseType(typeof(ResponseExpense), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddExpense(Guid userId, [FromBody] CreateExpense expense)
    {
        try
        {
            var newExpense = await _expenseService.AddExpenseAsync(userId, expense);
            var response = ResponseExpense.FromDomain(newExpense);
            return CreatedAtAction(nameof(GetExpensesByUserId), new { userId = userId }, response);
        }
        catch (Exception e)
        {
            return HandleException(e);
        }
    }

    [HttpGet("{userId}")]
    [ProducesResponseType(typeof(IEnumerable<ResponseExpense>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetExpensesByUserId(Guid userId)
    {
        try
        {
            var expenses = await _expenseService.GetExpenseByUserIdAsync(userId);
            var response = expenses.Select(e => ResponseExpense.FromDomain(e));
            return Ok(response);
        }
        catch (Exception e)
        {
            return HandleException(e);
        }
    }

    [HttpGet("{userId}/highest-category")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetHighestExpenseCategory(Guid userId)
    {
        try
        {
            var category = await _expenseService.GetHighestExpenseUserCategoryAsync(userId);
            return Ok(category);
        }
        catch (Exception e)
        {
            return HandleException(e);
        }
    }

    [HttpGet("{userId}/category")]
    [ProducesResponseType(typeof(IEnumerable<ResponseExpense>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetExpensesByCategory(Guid userId, [FromQuery] DateTime month, [FromQuery] string category)
    {
        try
        {
            var expenses = await _expenseService.GetUserExpensesByCategoryAsync(userId, month, category);
            var response = expenses.Select(e => ResponseExpense.FromDomain(e));
            return Ok(response);
        }
        catch (Exception e)
        {
            return HandleException(e);
        }
    }

    [HttpPut("{userId}/{expenseId}")]
    [ProducesResponseType(typeof(ResponseExpense), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateExpense(Guid userId, Guid expenseId, [FromBody] CreateExpense expense)
    {
        try
        {
            var updatedExpense = await _expenseService.UpdateExpenseAsync(userId, expenseId, expense);
            var response = ResponseExpense.FromDomain(updatedExpense);
            return Ok(response);
        }
        catch (Exception e)
        {
            return HandleException(e);
        }
    }

    [HttpDelete("{userId}/{expenseId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteExpense(Guid userId, Guid expenseId)
    {
        try
        {
            await _expenseService.DeleteExpense(userId, expenseId);
            const string succesfullyMessage = "Expense deleted succesfully";
            return Ok(succesfullyMessage);
        }
        catch (Exception e)
        {
            return HandleException(e);
        }
    }

    [HttpGet("{userId}/most-expensive-month")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMostExpensiveMonth(Guid userId)
    {
        try
        {
            var month = await _expenseService.GetUserMostExpensiveMonth(userId);
            return Ok(month);
        }
        catch (Exception e)
        {
            return HandleException(e);
        }
    }

    [HttpGet("{userId}/recurring-expenses")]
    [ProducesResponseType(typeof(IEnumerable<ResponseExpense>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRecurringExpenses(Guid userId)
    {
        try
        {
            var expenses = await _expenseService.GetUserRecurringExpense(userId);
            var response = expenses.Select(e => ResponseExpense.FromDomain(e));
            return Ok(response);
        }
        catch (Exception e)
        {
            return HandleException(e);
        }
    }
}
