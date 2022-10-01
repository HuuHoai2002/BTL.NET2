/**
 * @author: Nghiêm Hữu Hoài
 */

using Microsoft.AspNetCore.Mvc;


namespace BTL.NET2.Controllers;

public class SeriesController : Controller
{
  private readonly ILogger<SeriesController> _logger;

  public SeriesController(ILogger<SeriesController> logger)
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

}
