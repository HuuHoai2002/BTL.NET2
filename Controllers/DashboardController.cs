/**
 * @author: Nghiêm Hữu Hoài
 */

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BTL.NET2.Models;
using BTL.NET2.Data;

namespace BTL.NET2.Controllers;

public class DashboardController : Controller
{
  private readonly ILogger<DashboardController> _logger;
  private readonly ApplicationDbContext _context;

  public DashboardController(ILogger<DashboardController> logger, ApplicationDbContext context)
  {
    _logger = logger;
    _context = context;
  }

  [HttpGet]
  public async Task<IActionResult> Index()
  {
    var user = await _context.Users.ToListAsync();

    return View(user);
  }

  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }
}
