namespace BTL.NET2.Models;

using BTL.NET2.Utils;
public class Comment
{
  public string Id { get; set; } = new GenerateID().createID();
  public string MovieId { get; set; } = string.Empty;
  public string Author { get; set; } = string.Empty;
  public string AuthorId { get; set; } = string.Empty;
  public string Content { get; set; } = string.Empty;
  public string MovieType { get; set; } = string.Empty;
  public DateTime CreatedAt { get; set; } = DateTime.Now;
  public DateTime UpdatedAt { get; set; } = DateTime.Now;
}