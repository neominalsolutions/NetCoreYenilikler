using Microsoft.AspNetCore.Mvc;
using MVCLab2.Models;

namespace MVCLab2.Controllers
{
  public class ActionResultController : Controller
  {

    // MVC kendi route mekanizmasını kullanmıyor.

    [Route("resultTypes", Name ="resultTypeRoute")]
    public IActionResult Index(string name, int id)
    {
      return View();
    }

    public IActionResult ContentResult()
    {
      string htmlContent = "<p>Html Content Result</p>";


      return Content(htmlContent, "text/html");
    }

    public IActionResult PartialResult()
    {
      var model = new HeaderVM() { Title = "title", Content = "content" };

      return PartialView("~/Views/Shared/Partials/_header.cshtml",model);
    }

    public IActionResult JsonResult()
    {
      var model = new JsonVM()
      {
        Name = "test",
        Description = "test-description"
      };

      return Json(model);
    }

    public IActionResult RedirectResult()
    {
      return RedirectToAction("Index","Home");
    }

    public IActionResult FileResult()
    {

      string path = "/download/test.pdf";
      return File(path, "application/pdf");

    }

    public IActionResult NotFoundResult()
    {
      return NotFound();
    }


    public IActionResult UnauthorizedResult()
    {
      return Unauthorized();
    }

  }
}
