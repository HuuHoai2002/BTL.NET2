/**
 * @author: Nghiêm Hữu Hoài
 */
namespace BTL.NET2.Models;

public class ErrorViewModel
{
  public string? RequestId { get; set; }

  public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
