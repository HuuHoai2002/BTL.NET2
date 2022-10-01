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
  public IActionResult Index()
  {
    return View();
  }

  // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  // public IActionResult Error()
  // {
  //   return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  // }
}