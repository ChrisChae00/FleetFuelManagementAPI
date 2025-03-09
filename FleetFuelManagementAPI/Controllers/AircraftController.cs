using Microsoft.AspNetCore.Mvc;

namespace FleetFuelManagementAPI.Controllers
{
    public class AircraftController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
