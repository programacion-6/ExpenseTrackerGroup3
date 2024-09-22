
using Dapper;

using Domain.Entities;

using ExpenseTrackerGroup3.Data.Interfaces;
using ExpenseTrackerGroup3.Repositories.Interfaces;

namespace ExpenseTrackerGroup3.Repositories;

public class ExpenseRepository : IExpenseRepository
{
    private readonly IDbConnectionFactory _dbConnection;

    public ExpenseRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnection = dbConnectionFactory;
    }

    public async Task<bool> CreateAsync(Expense item)
    {
        const string query = @"
        INSERT INTO Expense (Id, UserId, Amount, Description, Category, Date, CreatedAt, RecurringExpense)
        VALUES (@Id, @UserId, @Amount, @Description, @Category, @Date, @CreatedAt, @RecurringExpense)";

        using var connection = await _dbConnection.CreateConnectionAsync();
        var affectedRows = await connection.ExecuteAsync(query, item);

        return affectedRows > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        const string query = "DELETE FROM Expense WHERE Id = @Id";

        using var connection = await _dbConnection.CreateConnectionAsync();
        var affectedRows = await connection.ExecuteAsync(query, new { Id = id });

        return affectedRows > 0;
    }

    public async Task<IEnumerable<Expense>> GetAllAsync()
    {
        const string query = "SELECT * FROM Expense";

        using var connection = await _dbConnection.CreateConnectionAsync();

        return await connection.QueryAsync<Expense>(query);
    }

    public async Task<Expense?> GetByIdAsync(Guid id)
    {
        const string query = "SELECT * FROM Expense WHERE Id = @Id";

        using var connection = await _dbConnection.CreateConnectionAsync();

        return await connection.QuerySingleOrDefaultAsync<Expense>(query, new { Id = id });
    }

    public async Task<string?> GetHighestSpendingCategoryByUserId(Guid userId)
    {
        const string query = @"
        SELECT Category, 
        SUM(Amount) AS TotalSpent
        FROM Expense
        WHERE UserId = @UserId
        GROUP BY Category
        ORDER BY TotalSpent DESC
        LIMIT 1;
    ";

        using var connection = await _dbConnection.CreateConnectionAsync();
        return await connection.QueryFirstOrDefaultAsync<string>(query, new { UserId = userId });
    }

    public async Task<Expense?> GetMonthlyExpensesAsync(Guid userId, DateTime month)
    {
        const string sql = @"
            SELECT SUM(Amount) as Amount
            FROM Expenses
            WHERE UserId = @UserId AND date_trunc('month', Date) = date_trunc('month', @Month)";

        var connection = await _dbConnection.CreateConnectionAsync();
        var totalExpenses = await connection.QuerySingleOrDefaultAsync<Expense>(sql, new { UserId = userId, Month = month });

        return totalExpenses;
    }

    public async Task<DateTime> GetMostExpensiveMonthByUserId(Guid userId)
    {
        const string query = @"
        SELECT DATE_TRUNC('month', Date) AS Month, 
        SUM(Amount) AS TotalSpent
        FROM Expense
        WHERE UserId = @UserId
        GROUP BY Month
        ORDER BY TotalSpent DESC
        LIMIT 1;
    ";

        using var connection = await _dbConnection.CreateConnectionAsync();
        return await connection.QueryFirstOrDefaultAsync<DateTime>(query, new { UserId = userId });
    }

    public async Task<bool> UpdateAsync(Expense item)
    {
        const string query = @"
        UPDATE Expense
        SET UserId = @UserId, 
            Amount = @Amount, 
            Description = @Description, 
            Category = @Category, 
            Date = @Date, 
            CreatedAt = @CreatedAt, 
            RecurringExpense = @RecurringExpense
        WHERE Id = @Id;
    ";

        using var connection = await _dbConnection.CreateConnectionAsync();
        var affectedRows = await connection.ExecuteAsync(query, item);

        return affectedRows > 0;
    }

}
