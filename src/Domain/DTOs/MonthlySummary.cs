namespace ExpenseTrackerGroup3.Domain.DTOs;

public record MonthlySummaryDTO
(
    DateTime Date,
    decimal TotalIncome,
    decimal TotalExpense,
    decimal TotalBudget,
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
            monthlySummary.TotalBudget,
            monthlySummary.RemainingBudget
        );
    }
}
