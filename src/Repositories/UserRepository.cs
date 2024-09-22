
using Dapper;

using Domain.Entities;

using ExpenseTrackerGroup3.Data;
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
            INSERT INTO User (Id, Name, Email, PasswordHash, CreatedAt)
            VALUES (@Id, @Name, @Email, @PasswordHash, @CreatedAt)";

        using var connection = await _dbConnection.CreateConnectionAsync();
        var affectedRows = await connection.ExecuteAsync(sql, item);
        return affectedRows > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        const string sql = "DELETE FROM User WHERE Id = @Id";

        using var connection = await _dbConnection.CreateConnectionAsync();
        var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
        return affectedRows > 0;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        const string sql = "SELECT * FROM User";

        using var connection = await _dbConnection.CreateConnectionAsync();
        return await connection.QueryAsync<User>(sql);
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        const string sql = "SELECT * FROM User WHERE ID = @Id";

        using var connection = await _dbConnection.CreateConnectionAsync();
        return await connection.QuerySingleOrDefaultAsync<User>(sql, new { Id = id });
    }

    public Task<bool> ResetPassword(string email)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateAsync(User item)
    {
        const string sql = @"
            UPDATE User
            SET Name = @Name, Email = @Email, PasswordHash = @PasswordHash
            WHERE Id = @Id";

        using var connection = await _dbConnection.CreateConnectionAsync();
        var affectedRows = await connection.ExecuteAsync(sql, item);
        return affectedRows > 0;
    }
}
