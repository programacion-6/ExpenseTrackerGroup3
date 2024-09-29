namespace ExpenseTrackerGroup3.Domain.DTOs;

public record BudgetStatus(
    bool IsOverThreshold, 
    decimal TotalBudget, 
    decimal TotalExpenses, 
    decimal AlertThreshold, 
    string Message
);
