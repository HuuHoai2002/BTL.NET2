/**
 * @author: Nghiêm Hữu Hoài
 */

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BTL.NET2.Models;
using BTL.NET2.Data;
using BTL.NET2.Utils;
using BTL.NET2.Services;

namespace BTL.NET2.Controllers;

public class DashboardController : Controller
{
  private readonly ILogger<DashboardController> _logger;
  private readonly ApplicationDbContext _context;

  Authentication auth = new Authentication();
  StringConvert str = new StringConvert();

  public DashboardController(ILogger<DashboardController> logger, ApplicationDbContext context)
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
    else
    {
      if (!auth.IsAdmin(HttpContext))
      {
        return RedirectToAction("Index", "Home");
      }
    }
    return RedirectToAction(nameof(Users_Manage));
  }

  [HttpGet]
  public async Task<IActionResult> Users_Manage([FromQuery] int? page, [FromQuery] int? limit, [FromQuery] string? search)
  {
    if (!auth.IsAuthenticated(HttpContext))
    {
      return RedirectToAction("Login", "Auth");
    }
    else
    {
      if (!auth.IsAdmin(HttpContext))
      {
        return RedirectToAction("Index", "Home");
      }
    }
    var _users = _context.Users.Select(s => s);

    int _page = page ?? 1;
    int _limit = limit ?? 10;

    if (!String.IsNullOrEmpty(search))
    {
      try
      {
        _users = _users.Where(s => str.RemoveVietNameseAcccents(s.Name.ToLower()).Contains(str.RemoveVietNameseAcccents(search.ToLower())));
      }
      catch (System.Exception)
      {
        throw;
      }
    }

    var users = await Pagination<User>.CreateAsync(_users.AsNoTracking(), _page, _limit);
    return View(users);
  }

  [HttpGet]
  public async Task<IActionResult> Comments_Manage([FromQuery] int? page, [FromQuery] int? limit, [FromQuery] string? search)
  {
    if (!auth.IsAuthenticated(HttpContext))
    {
      return RedirectToAction("Login", "Auth");
    }
    else
    {
      if (!auth.IsAdmin(HttpContext))
      {
        return RedirectToAction("Index", "Home");
      }
    }
    // var _comments = from s in _context.Comments
    //                 select s;
    // linq query
    var _comments = _context.Comments.Select(s => s);

    int _page = page ?? 1;
    int _limit = limit ?? 10;

    if (!String.IsNullOrEmpty(search))
    {
      try
      {
        _comments = _comments.Where(s => s.Author.ToLower().Contains(search.ToLower()));
      }
      catch (System.Exception)
      {
        throw;
      }
    }

    var comments = await Pagination<Comment>.CreateAsync(_comments.AsNoTracking(), _page, _limit);
    return View(comments);
  }

  [HttpGet]
  public async Task<IActionResult> EditUser(string id)
  {
    if (!auth.IsAuthenticated(HttpContext))
    {
      return RedirectToAction("Login", "Auth");
    }
    else
    {
      if (!auth.IsAdmin(HttpContext))
      {
        return RedirectToAction("Index", "Home");
      }
    }
    try
    {
      var result = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
      if (result != null)
      {
        return View(result);
      }
    }
    catch (System.Exception)
    {
      throw;
    }
    return RedirectToAction(nameof(Users_Manage));
  }
  [HttpGet]
  public IActionResult AddUser()
  {
    if (!auth.IsAuthenticated(HttpContext))
    {
      return RedirectToAction("Login", "Auth");
    }
    else
    {
      if (!auth.IsAdmin(HttpContext))
      {
        return RedirectToAction("Index", "Home");
      }
    }
    return View();
  }

  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }
}
