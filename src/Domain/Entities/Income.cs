using System;

namespace Domain.Entities:

public class Income
{
  public Guid Id { get; set; }
  public Guid UserId { get; set; }
  public decimal Amount { get; set; }
  public string Source { get; set; }
  public DateTime CreatedAt { get; set; }
}
