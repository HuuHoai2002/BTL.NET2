using BTL.NET2.Utils;

namespace BTL.NET2.Models;

public class User
{
  public string Id { get; set; } = new GenerateID().createID();
  public string? Email { get; set; }
  public string? Password { get; set; }
  public string? Name { get; set; }
  public string? Phone { get; set; }
  public string? Address { get; set; }
  public string? Role { get; set; }
  public DateTime CreatedAt { get; set; } = DateTime.Now;
}