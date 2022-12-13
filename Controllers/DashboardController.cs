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

  GenerateID generateID = new GenerateID();
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
        _users = _users.Where(s => s.Name.ToLower().Contains(search.ToLower()));
        ViewData["search"] = search;
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
    // linq query
    var _comments = _context.Comments.Select(s => s);

    int _page = page ?? 1;
    int _limit = limit ?? 10;

    if (!String.IsNullOrEmpty(search))
    {
      try
      {
        _comments = _comments.Where(s => s.Author.ToLower().Contains(search.ToLower()));
        ViewData["search"] = search;
      }
      catch (System.Exception)
      {
        throw;
      }
    }

    var comments = await Pagination<Comment>.CreateAsync(_comments.AsNoTracking(), _page, _limit);
    return View(comments);
  }

  [HttpGet] // view
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
  [HttpPost]
  public async Task<IActionResult> AddUser(User user)
  {
    if (ModelState.IsValid)
    {
      var result = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
      if (result == null)
      {
        try
        {
          _context.Users.Add(new Models.User
          {
            Email = user.Email,
            Password = user.Password,
            Name = user.Name,
            Phone = user.Phone,
            Address = user.Address,
          });
          await _context.SaveChangesAsync();
          return RedirectToAction(nameof(Users_Manage));
        }
        catch (System.Exception)
        {
          return RedirectToAction(nameof(Users_Manage));
          throw;
        }
      }
      else
      {
        TempData["error"] = "Email đã tồn tại";
        return RedirectToAction(nameof(AddUser));
      }
    }
    return RedirectToAction("Index", "Dashboard");
  }

  [HttpGet] // view
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

  [HttpPost]
  public async Task<IActionResult> EditUser(User user)
  {
    if (user == null)
    {
      return Redirect(Request.Headers["Referer"].ToString());
    }
    try
    {
      var result = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
      if (result == null)
      {
        return Redirect(Request.Headers["Referer"].ToString());
      }
      result.Email = user.Email;
      result.Name = user.Name;
      result.Phone = user.Phone;
      result.Address = user.Address;
      result.Role = user.Role;
      result.UpdatedAt = DateTime.Now;
      _context.Users.Update(result);
      await _context.SaveChangesAsync();
      return Redirect(Request.Headers["Referer"].ToString());
    }
    catch (System.Exception)
    {
      return Redirect(Request.Headers["Referer"].ToString());
      throw;
    }
  }

  [HttpPost]
  public async Task<IActionResult> DeleteUser(string id)
  {
    if (id == null)
    {
      return RedirectToAction(nameof(Users_Manage));
    }
    try
    {
      var user = await _context.Users.FindAsync(id);
      if (user == null)
      {
        return RedirectToAction(nameof(Users_Manage));
      }
      _context.Users.Remove(user);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Users_Manage));
    }
    catch (Exception)
    {
      return RedirectToAction(nameof(Users_Manage));
    }
  }


  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }
}
