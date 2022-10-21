/**
 * @author: Nghiêm Hữu Hoài
 */

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BTL.NET2.Models;
using BTL.NET2.Data;

namespace BTL.NET2.Controllers;

public class SettingsController : Controller
{
  private readonly ILogger<SettingsController> _logger;

  private readonly ApplicationDbContext _context;

  public SettingsController(ILogger<SettingsController> logger, ApplicationDbContext context)
  {
    _logger = logger;
    _context = context;
  }

  [HttpGet]
  public IActionResult Index()
  {
    return View();
  }

  [HttpPost]
  public async Task<IActionResult> Index(User user)
  {
    // if (ModelState.IsValid)
    // {
    //   var result = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
    //   if (result != null)
    //   {
    //     user.UpdatedAt = DateTime.Now;
    //     _context.Users.Update(user);
    //   }
    //   return View();
    // }
    return View();
  }
}
