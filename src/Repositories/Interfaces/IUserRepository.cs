using Domain.Entities;

namespace ExpenseTrackerGroup3.Repositories.Interfaces;

public interface IUserRepository : IRepository<User>
{
    public Task<User?> GetByEmailAsync(string email);
    Task<bool> ResetPassword(string email);
}
