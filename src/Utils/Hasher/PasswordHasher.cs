using ExpenseTrackerGroup3.Utils.Hasher.Interfaces;

namespace ExpenseTrackerGroup3.Utils;

public class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string userPassword, string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, userPassword);
    }
}
