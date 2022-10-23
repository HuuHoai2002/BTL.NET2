/**
 * @author: Nghiêm Hữu Hoài
 */

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BTL.NET2.Data;


namespace BTL.NET2.Controllers;

public class MoviesController : Controller
{
  private readonly ILogger<MoviesController> _logger;
  private readonly ApplicationDbContext _context;

  public MoviesController(ILogger<MoviesController> logger, ApplicationDbContext context)
  {
    _logger = logger;
    _context = context;
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
  public async Task<IActionResult> Watch([FromQuery] string id)
  {
    var comments = await _context.Comments.Where(c => c.MovieId == id && c.MovieType == "movie").OrderByDescending(c => c.CreatedAt).ToListAsync();

    return View(comments);
  }

  // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  // public IActionResult Error()
  // {
  //   return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  // }
}
