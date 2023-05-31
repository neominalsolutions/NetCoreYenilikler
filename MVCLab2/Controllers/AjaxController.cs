using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace MVCLab2.Controllers
{
  public class AjaxController : Controller
  {
    public IActionResult Index()
    {
      ViewBag.Mesaj = "Mesaj";
      ViewData["Mesaj2"] = "Mesaj2";
      TempData["Mesaj3"] = "Mesaj3";

      return RedirectToAction("Ajax","Index");
    }

    public class TodosVM
    {
      [JsonPropertyName("title")]
      public string Title { get; set; }
    }

    public JsonResult GetTodos()
    {
      var todos = new List<TodosVM> { new TodosVM { Title = "title1" }, new TodosVM { Title = "title2" }, new TodosVM { Title = "title3" } };

      return Json(todos);
    }
  }
}
