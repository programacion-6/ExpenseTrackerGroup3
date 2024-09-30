using Domain.Entities;

namespace Domain.DTOs;

public record CreateUser
(
  string Name,
  string Email,
  string Password
)
{
  public User ToDomain()
  {
    return new User
    {
      Name = Name,
      Email = Email,
      PasswordHash = Password
    };
  }
}
