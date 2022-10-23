/**
 * @author: Nghiêm Hữu Hoài
 */

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BTL.NET2.Data;


namespace BTL.NET2.Controllers;

public class SeriesController : Controller
{
  private readonly ILogger<SeriesController> _logger;

  private readonly ApplicationDbContext _context;

  public SeriesController(ILogger<SeriesController> logger, ApplicationDbContext context)
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
    var comments = await _context.Comments.Where(c => c.MovieId == id && c.MovieType == "serie").OrderByDescending(c => c.CreatedAt).ToListAsync();

    return View(comments);
  }
}
