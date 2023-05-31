using Microsoft.AspNetCore.Mvc;
using MVCLab2.DIServices;

namespace MVCLab2.Controllers
{
  public class MicrosoftDIController : Controller
  {
    // Dependency Injection instance alma yöntemi
    private readonly TransientService transient1;
    private readonly TransientService transient2;

    private readonly ScopeService scope1;
    private readonly ScopeService scope2;

    private readonly SingletonService singleton1;
    private readonly SingletonService singleton2;

    public MicrosoftDIController(TransientService transient1, TransientService transient2, ScopeService scope1, ScopeService scope2, SingletonService singleton1, SingletonService singleton2)
    {
      this.transient1 = transient1;
      this.transient2 = transient2;
      this.scope1 = scope1;
      this.scope2 = scope2;
      this.singleton1 = singleton1;
      this.singleton2 = singleton2;
    }

    public IActionResult Index()
    {

      ViewBag.Transient1 = transient1.Id;
      ViewBag.Transient2 = transient2.Id;
      ViewBag.Scope1 = scope1.Id;
      ViewBag.Scope2 = scope2.Id;
      ViewBag.Singleton1 = singleton1.Id;
      ViewBag.Singleton2 = singleton2.Id;

      return View();
    }
  }
}
