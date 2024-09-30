using Dapper;

using Domain.Entities;

using ExpenseTrackerGroup3.Data.Interfaces;
using ExpenseTrackerGroup3.Repositories.Interfaces;

namespace ExpenseTrackerGroup3.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDbConnectionFactory _dbConnection;

    public UserRepository(IDbConnectionFactory connectionFactory)
    {
        _dbConnection = connectionFactory;
    }

    public async Task<bool> CreateAsync(User item)
    {
        const string sql = @"
            INSERT INTO Users (Id, Name, Email, PasswordHash, CreatedAt)
            VALUES (@Id, @Name, @Email, @PasswordHash, @CreatedAt)";

        using var connection = await _dbConnection.CreateConnectionAsync();
        var affectedRows = await connection.ExecuteAsync(sql, item);
        return affectedRows > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        const string sql = "DELETE FROM Users WHERE Id = @Id";

        using var connection = await _dbConnection.CreateConnectionAsync();
        var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
        return affectedRows > 0;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        const string sql = "SELECT * FROM Users";

        using var connection = await _dbConnection.CreateConnectionAsync();
        return await connection.QueryAsync<User>(sql);
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        const string sql = "SELECT * FROM Users WHERE Id = @Id";

        using var connection = await _dbConnection.CreateConnectionAsync();
        return await connection.QuerySingleOrDefaultAsync<User>(sql, new { Id = id });
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        const string sql = "SELECT * FROM Users WHERE Email = @Email";

        using var connection = await _dbConnection.CreateConnectionAsync();
        return await connection.QuerySingleOrDefaultAsync<User>(sql, new {Email = email});
    }

    public async Task<bool> ResetPassword(string email)
    {
        const string sql = "SELECT COUNT(1) FROM Users WHERE Email = @Email";

        using var connection = await _dbConnection.CreateConnectionAsync();
        var userExists = await connection.ExecuteScalarAsync<bool>(sql, new { Email = email });
        return userExists;
    }

    public async Task<bool> UpdateAsync(User item)
    {
        const string sql = @"
            UPDATE Users
            SET Name = @Name, Email = @Email, PasswordHash = @PasswordHash
            WHERE Id = @Id";

        using var connection = await _dbConnection.CreateConnectionAsync();
        var affectedRows = await connection.ExecuteAsync(sql, item);
        return affectedRows > 0;
    }
}
