using Domain.Entities;

namespace ExpenseTrackerGroup3.Repositories.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<bool> ResetPassword(string email);
}
