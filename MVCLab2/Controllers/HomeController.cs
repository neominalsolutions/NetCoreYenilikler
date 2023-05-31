using Microsoft.AspNetCore.Mvc;
using MVCLab2.Extensions;
using MVCLab2.Middlewares;
using MVCLab2.Models;
using System.Diagnostics;

namespace MVCLab2.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
      _logger = logger;
    }

    public IActionResult Index()
    {
      // custom extension kullanım örneği
      string prettyDate = DateTime.Now.GetPrettyDateToString();

      return View();
    }


    //[HttpGet("{controller}/{action}/{name}")]
    [Route("{controller}/{action}/{name}")]
    public IActionResult Test(string name)
    {
      

      return View();
    }

    public IActionResult Privacy()
    {
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}