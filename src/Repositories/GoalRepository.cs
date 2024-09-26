using Domain.Entities;

using ExpenseTrackerGroup3.Repositories.Interfaces;
using ExpenseTrackerGroup3.Data.Interfaces;
using Dapper;

namespace ExpenseTrackerGroup3.Repositories;

public class GoalRepository : IGoalRepository
{
    private readonly IDbConnectionFactory _dbConnection;

    public GoalRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnection = dbConnectionFactory;
    }

    public async Task<bool> CreateAsync(Goal item)
    {
        const string query = @"
        INSERT INTO Goal (Id, UserId, GoalAmount, Deadline, CurrentAmount, CreatedAt)
        VALUES (@Id, @UserId, @GoalAmount, @DeadLine, @CurrentAmount, @CreatedAt)
        ";

        using var connection = await _dbConnection.CreateConnectionAsync();
        var affectedRows = await connection.ExecuteAsync(query, item);

        return affectedRows > 0;
    }


    public async Task<bool> DeleteAsync(Guid id)
    {
        const string query = "DELETE FROM Goal WHERE Id = @Id";

        using var connection = await _dbConnection.CreateConnectionAsync();
        var affectedRows = await connection.ExecuteAsync(query, new { Id = id });

        return affectedRows > 0;
    }

    public async Task<IEnumerable<Goal>> GetActiveGoalsByUserIdAsync(Guid userId)
    {
        const string query = @"
        SELECT * FROM Goal 
        WHERE UserId = @UserId 
        AND DeadLine > NOW()
        ";

        using var connection = await _dbConnection.CreateConnectionAsync();
        return await connection.QueryAsync<Goal>(query, new { UserId = userId });
    }

    public async Task<IEnumerable<Goal>> GetAllAsync()
    {
        const string query = "SELECT * FROM Goal";

        using var connection = await _dbConnection.CreateConnectionAsync();

        return await connection.QueryAsync<Goal>(query);
    }

    public async Task<Goal?> GetByIdAsync(Guid id)
    {
        const string query = "SELECT * FROM Goal WHERE Id = @Id";

        using var connection = await _dbConnection.CreateConnectionAsync();
        return await connection.QuerySingleOrDefaultAsync<Goal>(query, new { Id = id });
    }

    public async Task<IEnumerable<Goal>> GetGoalsByUserIdAsync(Guid userId)
    {
        const string query = "SELECT * FROM Goal WHERE UserId = @UserId";

        using var connection = await _dbConnection.CreateConnectionAsync();
        var goals = await connection.QueryAsync<Goal>(query, new { UserId = userId });
        return goals; 
    }

    public async Task<decimal> GetGoalProgressAsync(Guid id)
    {
        const string query = @"
        SELECT 
            CASE 
                WHEN GoalAmount = 0 THEN 0 
                ELSE (CurrentAmount / GoalAmount * 100.0) 
            END AS Progress
        FROM Goal
        Where Id = @Id
        ";

        using var connection = await _dbConnection.CreateConnectionAsync();

        return await connection.ExecuteScalarAsync<decimal>(query, new { Id = id });
    }

    public async Task<bool> UpdateAsync(Goal item)
    {
        const string query = @"
        UPDATE goal
        SET UserId = @UserId, GoalAmount = @GoalAmount, DeadLine = @DeadLine, CurrentAmount = @CurrentAmount, CreatedAt = @CreatedAt
        WHERE id = @Id";

        using var connection = await _dbConnection.CreateConnectionAsync();
        var affectedRows = await connection.ExecuteAsync(query, item);

        return affectedRows > 0;
    }
}
