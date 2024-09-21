using System;

namespace Domain.Entities;
public class Expense
{
  public Guid Id { get; set; }
  public Guid UserId { get; set; }
  public decimal Amount { get; set; }
  public required string Description { get; set; }
  public required string Category { get; set; }
  public DateTime Date { get; set; }
  public DateTime CreatedAt { get; set; }
  public bool RecurringExpese { get; set; }
}