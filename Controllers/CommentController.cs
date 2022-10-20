/**
 * @author: Nghiêm Hữu Hoài
 */

using BTL.NET2.Models;
using BTL.NET2.Data;

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BTL.NET2.Utils;

namespace BTL.NET2.Controllers;

public class CommentController : Controller
{
  // set connect to database
  private readonly ApplicationDbContext _context;
  // constructor
  public CommentController(ApplicationDbContext context)
  {
    _context = context;
  }

  [HttpPost]
  public async Task<IActionResult> CreateComment(Comment comment)
  {
    if (comment.AuthorId == null)
    {
      return RedirectToAction("Login", "Auth");
    }
    comment.Id = new GenerateID().createID();
    comment.CreatedAt = DateTime.Now;
    comment.UpdatedAt = DateTime.Now;
    _context.Comments.Add(comment);
    await _context.SaveChangesAsync();

    return Redirect(Request.Headers["Referer"].ToString());
  }

  [HttpPost]
  public async Task<IActionResult> Delete([FromQuery] string id)
  {
    if (id == null)
    {
      return Redirect(Request.Headers["Referer"].ToString());
    }
    var comment = await _context.Comments.FindAsync(id);
    if (comment == null)
    {
      return Redirect(Request.Headers["Referer"].ToString());
    }
    _context.Comments.Remove(comment);
    _context.SaveChanges();
    return Redirect(Request.Headers["Referer"].ToString());
  }
  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }
}

