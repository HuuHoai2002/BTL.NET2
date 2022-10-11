/**
 * @author: Nghiêm Hữu Hoài
 */

using BTL.NET2.Models;
using BTL.NET2.Data;

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BTL.NET2.Controllers;

public class AuthController : Controller
{
  // set connect to database
  private readonly ApplicationDbContext _context;
  // constructor
  public AuthController(ApplicationDbContext context)
  {
    _context = context;
  }

  [HttpGet]
  public IActionResult Login()
  {
    return View();
  }

  [HttpGet]
  public IActionResult Register()
  {
    return View();
  }

  [HttpPost]
  public async Task<IActionResult> Login(User user)
  {
    var result = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email && u.Password == user.Password);
    // save user to session
    if (result != null)
    {
      HttpContext.Session.SetString("user", JsonConvert.SerializeObject(result));

      return RedirectToAction("Index", "Home");
    }
    else
    {
      ModelState.AddModelError("", "Email hoặc mật khẩu không đúng");
      return View();
    }
  }

  [HttpPost]
  public async Task<IActionResult> Register(User user)
  {
    if (ModelState.IsValid)
    {
      // check user is exist
      var result = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
      if (result == null)
      {
        // add user to database        
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Login));
      }
      else
      {
        ModelState.AddModelError("", "Email đã tồn tại");
        return View();
      }
    }
    return View(user);
  }

  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }
}