using Microsoft.AspNetCore.Mvc;

namespace Calabonga.Facts.Web.Controllers
{
    public class AdministratorController: Controller
    {
        public IActionResult Index() => View();
    }
}