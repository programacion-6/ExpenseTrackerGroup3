namespace ExpenseTrackerGroup3.Utils.Hasher.Interfaces;

public interface IPasswordHasher
{
    public string HashPassword(string password);
    public bool VerifyPassword(string userPassword, string password);
}
