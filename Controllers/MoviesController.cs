/**
 * @author: Nghiêm Hữu Hoài
 */

using Microsoft.AspNetCore.Mvc;


namespace BTL.NET2.Controllers;

public class MoviesController : Controller
{
  private readonly ILogger<MoviesController> _logger;

  public MoviesController(ILogger<MoviesController> logger)
  {
    _logger = logger;
  }

  [HttpGet]
  public IActionResult Index()
  {
    return View();
  }

  [HttpGet]
  public IActionResult Detail()
  {
    return View();
  }

  [HttpGet]
  public IActionResult Watch()
  {
    return View();
  }

  // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  // public IActionResult Error()
  // {
  //   return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  // }
}
