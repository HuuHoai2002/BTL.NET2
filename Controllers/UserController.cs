/**
 * @author: Nghiêm Hữu Hoài
 */

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BTL.NET2.Data;
using BTL.NET2.Models;

namespace BTL.NET2.Controllers;

public class UserController : Controller
{
  private readonly ILogger<UserController> _logger;
  private readonly ApplicationDbContext _context;

  public UserController(ILogger<UserController> logger, ApplicationDbContext context)
  {
    _logger = logger;
    _context = context;
  }

  [HttpGet]
  public async Task<IActionResult> Index([FromQuery] string id)
  {
    if (id == null)
    {
      return NotFound();
    }
    try
    {
      var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
      if (user == null)
      {
        return NotFound();
      }
      return View(user);
    }
    catch (System.Exception)
    {
      return NotFound();
      throw;
    }
  }

  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }
}
