using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class WebsiteController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}