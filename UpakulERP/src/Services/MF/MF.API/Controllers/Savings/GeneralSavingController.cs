using Microsoft.AspNetCore.Mvc;

namespace MF.API.Controllers.Savings
{
    public class GeneralSavingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
