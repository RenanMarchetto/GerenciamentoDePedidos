using Microsoft.AspNetCore.Mvc;

public class MainController : Controller
{
  public IActionResult Index() => View();
}