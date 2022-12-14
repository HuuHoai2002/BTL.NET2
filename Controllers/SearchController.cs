/**
 * @author: Nghiêm Hữu Hoài
 */

using Microsoft.AspNetCore.Mvc;


namespace BTL.NET2.Controllers;

public class SearchController : Controller
{
  private readonly ILogger<SearchController> _logger;

  public SearchController(ILogger<SearchController> logger)
  {
    _logger = logger;
  }

  [HttpGet]
  public IActionResult Index([FromQuery] string keyword)
  {
    TempData["keyword"] = keyword;
    return View();
  }
}
