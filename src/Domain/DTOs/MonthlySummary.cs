namespace ExpenseTrackerGroup3.Domain.DTOs;

public record MonthlySummaryDTO
(
    DateTime Date,
    decimal TotalIncome,
    decimal TotalExpense,
    decimal RemainingBudget
)
{
    public static MonthlySummaryDTO FromDomain(MonthlySummaryDTO monthlySummary)
    {
        return new MonthlySummaryDTO
        (
            monthlySummary.Date,
            monthlySummary.TotalIncome,
            monthlySummary.TotalExpense,
            monthlySummary.RemainingBudget
        );
    }
}