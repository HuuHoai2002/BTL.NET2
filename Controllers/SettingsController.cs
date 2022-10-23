/**
 * @author: Nghiêm Hữu Hoài
 */

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BTL.NET2.Models;
using BTL.NET2.Data;
using BTL.NET2.Utils;

namespace BTL.NET2.Controllers;

public class SettingsController : Controller
{
  private readonly ILogger<SettingsController> _logger;

  private readonly ApplicationDbContext _context;

  Authentication auth = new Authentication();

  public SettingsController(ILogger<SettingsController> logger, ApplicationDbContext context)
  {
    _logger = logger;
    _context = context;
  }

  [HttpGet]
  public IActionResult Index()
  {
    if (!auth.IsAuthenticated(HttpContext))
    {
      return RedirectToAction("Login", "Auth");
    }
    return RedirectToAction(nameof(Me));
  }

  [HttpGet]
  public IActionResult Me()
  {
    if (!auth.IsAuthenticated(HttpContext))
    {
      return RedirectToAction("Login", "Auth");
    }
    return View();
  }
}
