using Domain.Entities;

namespace Domain.DTOs;

public record CreateUser
(
  string Name,
  string Email,
  string PasswordHash,
  DateTime CreatedAt
)
{
  public User ToDomain()
  {
    return new User
    {
      Name = Name,
      Email = Email,
      PasswordHash = PasswordHash,
      CreatedAt = CreatedAt
    };
  }
}