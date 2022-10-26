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
    return View();
  }

  [HttpGet]
  public async Task<IActionResult> Me()
  {
    string id = HttpContext.Session.GetString("userid") ?? "";
    if (id == "")
    {
      return RedirectToAction("Login", "Auth");
    }
    try
    {
      var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
      if (user == null)
      {
        return RedirectToAction("Login", "Auth");
      }
      return View(user);
    }
    catch (System.Exception)
    {
      return View();
      throw;
    }
  }
  [HttpPost]
  public async Task<IActionResult> Me(User user)
  {
    string id = HttpContext.Session.GetString("userid") ?? "";
    if (user == null || id == "")
    {
      return RedirectToAction("Login", "Auth");
    }
    try
    {
      var result = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
      if (result == null)
      {
        return RedirectToAction("Login", "Auth");
      }
      result.Name = user.Name;
      result.Phone = user.Phone;
      result.Address = user.Address;
      _context.Users.Update(result);
      await _context.SaveChangesAsync();
      TempData["Success"] = "Cập nhật thông tin thành công";
      return RedirectToAction(nameof(Me));
    }
    catch (System.Exception)
    {
      return View();
      throw;
    }
  }

  [HttpGet]
  public IActionResult ChangePassword()
  {
    if (!auth.IsAuthenticated(HttpContext))
    {
      return RedirectToAction("Login", "Auth");
    }
    return View();
  }

  [HttpPost]
  public async Task<IActionResult> ChangePassword(string NewPassword, string OldPassword, string ConfirmPassword)
  {
    string id = HttpContext.Session.GetString("userid") ?? "";
    if (id == "")
    {
      return RedirectToAction("Login", "Auth");
    }
    if (NewPassword != ConfirmPassword)
    {
      ModelState.AddModelError("ConfirmPassword", "Mật khẩu xác nhận không khớp với mật khẩu mới");
      return View();
    }
    try
    {
      var result = await _context.Users.FirstOrDefaultAsync(u => u.Id == id && u.Password == OldPassword);
      if (result == null)
      {
        ModelState.AddModelError("OldPassword", "Mật khẩu cũ không đúng");
        return View();
      }
      result.Password = NewPassword;
      _context.Users.Update(result);
      await _context.SaveChangesAsync();
      TempData["Success"] = "Đổi mật khẩu thành công";
      return RedirectToAction(nameof(Me));
    }
    catch (System.Exception)
    {
      return RedirectToAction(nameof(Me));
      throw;
    }
  }
}
