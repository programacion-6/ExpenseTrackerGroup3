using Domain.Entities;

namespace Domain.DTOs;

public record ResponseUser
(
  Guid Id,
  string Name,
  string Email,
  DateTime CreatedAt
)
{
  public static ResponseUser FromDomain(User user)
  {
    return new ResponseUser
    (
      user.Id,
      user.Name,
      user.Email,
      user.CreatedAt
    );
  }
}