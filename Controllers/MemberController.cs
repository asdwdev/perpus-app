using Microsoft.AspNetCore.Mvc;

public class MemberController : Controller
{
  public IActionResult Index()
  {
    return View();
  }
  public IActionResult Create()
  {
    return View();
  }
}
