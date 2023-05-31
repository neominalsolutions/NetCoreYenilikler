using Microsoft.AspNetCore.Mvc;
using MVCLab2.Models;

namespace MVCLab2.Shared.Components.Menu
{
  public class MenuViewComponent : ViewComponent
  {
    public async Task<IViewComponentResult> InvokeAsync()
    {
      // veritabanına bağlanıp çekilebiliriz.
      // her bir viewcomponentin bir html sahip olması lazım
      var model = new List<MenuViewModel>
            {
                new MenuViewModel
                {
                    
                    ActionName = "Index",
                    ControllerName = "Home",
                    Title= "Anasayfa"
                },
                new MenuViewModel
                {
                  ActionName = "Privacy",
                  ControllerName = "Home",
                  Title = "Gizlilik"
                },
                 new MenuViewModel
                {
                  ActionName = "Index",
                  ControllerName = "MicrosoftDI",
                  Title = "Service Injection"
                },
                   new MenuViewModel
                {
                  ActionName = "Index",
                  ControllerName = "ActionResult",
                  Title = "Result Types"
                },
                   new MenuViewModel
                {
                  ActionName = "Index",
                  ControllerName = "Ajax",
                  Title = "Jquery Ajax JsonResult"
                }

            };

      return View(await Task.FromResult(model));
    }
  }
}
