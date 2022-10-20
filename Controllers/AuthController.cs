/**
 * @author: Nghiêm Hữu Hoài
 */

using BTL.NET2.Models;
using BTL.NET2.Data;
using BTL.NET2.Utils;

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BTL.NET2.Controllers;

public class AuthController : Controller
{
  // set connect to database
  private readonly ApplicationDbContext _context;

  GenerateID generateID = new GenerateID();
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
      if (result.Name != null && result.Email != null)
      {
        HttpContext.Session.SetString("username", result.Name);
        HttpContext.Session.SetString("email", result.Email);
        HttpContext.Session.SetString("userid", result.Id.ToString());
        HttpContext.Session.SetString("role", result.Role ?? "user");
        return RedirectToAction("Index", "Home");
      }
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
        // hash password
        _context.Users.Add(new Models.User
        {
          Id = generateID.createID(),
          Email = user.Email,
          Password = user.Password,
          Name = user.Name,
          Phone = user.Phone,
          Address = user.Address,
          Role = "user",
          CreatedAt = DateTime.Now
        });
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