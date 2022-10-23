namespace BTL.NET2.Utils;

class Authentication
{
  // private readonly HttpContext context;
  // public Authentication(HttpContext context)
  // {
  //   this.context = context;
  // }
  public bool IsAuthenticated(HttpContext context)
  {
    return context.Session.GetString("email") != null;
  }
  public bool IsAdmin(HttpContext context)
  {
    return context.Session.GetString("role") == "admin" && IsAuthenticated(context);
  }
  public string GetUsername(HttpContext context)
  {
    return context.Session.GetString("username") ?? "";
  }

  public string GetEmail(HttpContext context)
  {
    return context.Session.GetString("email") ?? "";
  }

  public string GetRole(HttpContext context)
  {
    return context.Session.GetString("role") ?? "";
  }

  public string GetUserId(HttpContext context)
  {
    return context.Session.GetString("userid") ?? "";
  }
}