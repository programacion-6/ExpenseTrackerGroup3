using Domain.Entities;

namespace Domain.DTOs;

public record ResponseExpense
(
  Guid Id,
  Guid UserId,
  decimal Amount,
  string? Description,
  string Category,
  DateTime Date,
  DateTime CreatedAt,
  bool RecurringExpense
)
{
  public static ResponseExpense FromDomain(Expense expense)
  {
    return new ResponseExpense
    (
      expense.Id,
      expense.UserId,
      expense.Amount,
      expense.Description,
      expense.Category,
      expense.Date,
      expense.CreatedAt,
      expense.RecurringExpense
    );
  }
}