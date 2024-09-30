namespace ExpenseTrackerGroup3.Exceptions;

public class ValidationError
{
    public string? PropertyName { get; set; }
    public string? ErrorMessage { get; set; }
}
