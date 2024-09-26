using Domain.Entities;

namespace Domain.DTOs;

public record ResponseUser
(
  string Name,
  string Email
)
{
  public static ResponseUser FromDomain(User user)
  {
    return new ResponseUser
    (
      user.Name,
      user.Email
    );
  }
}