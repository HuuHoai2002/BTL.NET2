using BTL.NET2.Utils;

namespace BTL.NET2.Models;

public class User
{
  public string Id { get; set; } = new GenerateID().createID();
  public string Email { get; set; } = string.Empty;
  public string Password { get; set; } = string.Empty;
  public string Name { get; set; } = string.Empty;
  public string? Phone { get; set; }
  public string? Address { get; set; }
  public string Role { get; set; } = "user";
  public DateTime CreatedAt { get; set; } = DateTime.Now;
  // public DateTime UpdatedAt { get; set; } = DateTime.Now;
}