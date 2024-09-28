using Domain.DTOs;

using ExpenseTrackerGroup3.Services.Interfaces;

using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerGroup3.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/users/{userId}/expenses")]
public class ExpenseController : ControllerBase
{
    private readonly IExpenseService _expenseService;

    public ExpenseController(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResponseExpense), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddExpense(Guid userId, [FromBody] CreateExpense expense)
    {
        var newExpense = await _expenseService.AddExpenseAsync(userId, expense);
        var response = ResponseExpense.FromDomain(newExpense);
        return CreatedAtAction(nameof(GetExpensesByUserId), new { userId = userId }, response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ResponseExpense>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetExpensesByUserId(Guid userId)
    {
        var expenses = await _expenseService.GetExpenseByUserIdAsync(userId);
        var response = expenses.Select(e => ResponseExpense.FromDomain(e));
        return Ok(response);
    }

    [HttpGet("highest-category")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetHighestExpenseCategory(Guid userId)
    {
        var category = await _expenseService.GetHighestExpenseUserCategoryAsync(userId);
        return Ok(category);
    }

    [HttpGet("category")]
    [ProducesResponseType(typeof(IEnumerable<ResponseExpense>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetExpensesByCategory(Guid userId, [FromQuery] DateTime month, [FromQuery] string category)
    {
        var expenses = await _expenseService.GetUserExpensesByCategoryAsync(userId, month, category);
        var response = expenses.Select(e => ResponseExpense.FromDomain(e));
        return Ok(response);
    }

    [HttpPut("{expenseId}")]
    [ProducesResponseType(typeof(ResponseExpense), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateExpense(Guid userId, Guid expenseId, [FromBody] CreateExpense expense)
    {
        var updatedExpense = await _expenseService.UpdateExpenseAsync(userId, expenseId, expense);
        var response = ResponseExpense.FromDomain(updatedExpense);
        return Ok(response);
    }

    [HttpDelete("{expenseId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteExpense(Guid userId, Guid expenseId)
    {
        await _expenseService.DeleteExpense(userId, expenseId);
        const string succesfullyMessage = "Expense deleted succesfully";
        return Ok(succesfullyMessage);
    }

    [HttpGet("most-expensive-month")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMostExpensiveMonth(Guid userId)
    {
        var month = await _expenseService.GetUserMostExpensiveMonth(userId);
        return Ok(month);
    }

    [HttpGet("recurring-expenses")]
    [ProducesResponseType(typeof(IEnumerable<ResponseExpense>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRecurringExpenses(Guid userId)
    {
        var expenses = await _expenseService.GetUserRecurringExpense(userId);
        var response = expenses.Select(e => ResponseExpense.FromDomain(e));
        return Ok(response);
    }
}
