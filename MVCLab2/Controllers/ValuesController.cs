using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static MVCLab2.Controllers.AjaxController;

namespace MVCLab2.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ValuesController : ControllerBase
  {

    [HttpGet]
    public IActionResult Get()
    {

      var todos = new List<TodosVM> { new TodosVM { Title = "title1" }, new TodosVM { Title = "title2" }, new TodosVM { Title = "title3" } };

      return Ok(todos);
    }
  }
}
