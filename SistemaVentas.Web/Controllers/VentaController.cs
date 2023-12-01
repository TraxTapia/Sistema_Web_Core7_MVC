using Microsoft.AspNetCore.Mvc;

namespace SistemaVentas.Web.Controllers
{
    public class VentaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
