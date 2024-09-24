using Domain.Entities;

namespace Domain.DTOs;

public record CreateExpense
(
  decimal Amount,
  string Description,
  string Category,
  DateTime Date,
  DateTime CreatedAt,
  bool RecurringExpense
)
{
  public Expense ToDomain()
  {
    return new Expense
    {
      Amount = Amount,
      Description = Description,
      Category = Category,
      Date = Date,
      CreatedAt = CreatedAt,
      RecurringExpense = RecurringExpense
    };
  }
}