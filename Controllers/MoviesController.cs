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
  public IActionResult Index(string id)
  {
    return View();
  }

  // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  // public IActionResult Error()
  // {
  //   return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  // }
}
