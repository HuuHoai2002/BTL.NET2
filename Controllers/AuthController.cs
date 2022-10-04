/**
 * @author: Nghiêm Hữu Hoài
 */

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BTL.NET2.Models;

namespace BTL.NET2.Controllers;

public class AuthController : Controller
{
  private readonly ILogger<AuthController> _logger;

  public AuthController(ILogger<AuthController> logger)
  {
    _logger = logger;
  }

  [HttpGet]
  public IActionResult Login()
  {
    return View();
  }
  [HttpPost]
  public IActionResult Login(string email, string password)
  {
    ViewBag.email = email;
    ViewBag.password = password;
    return View();
  }
  [HttpGet]
  public IActionResult Register()
  {
    return View();
  }
  [HttpPost]
  public IActionResult Register(string email, string password)
  {
    return View();
  }

  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }
}