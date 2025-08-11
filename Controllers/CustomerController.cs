using Microsoft.AspNetCore.Mvc;

namespace StokTakipProjesi2.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
