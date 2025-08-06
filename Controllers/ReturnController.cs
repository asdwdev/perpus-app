using Microsoft.AspNetCore.Mvc;

public class ReturnController : Controller{
  
  public IActionResult Index() {
    return View();
  }
}