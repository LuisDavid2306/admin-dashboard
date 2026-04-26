using AdminDashboard.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdminDashboard.Controllers
{
    public class DashboardController : Controller
    {
        private readonly DashboardService _service;

        public DashboardController(DashboardService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(DateTime? fechaInicio, DateTime? fechaFin)
        {
            var token = HttpContext.Session.GetString("TOKEN");

            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            var data = await _service.ObtenerDashboardAsync(token, fechaInicio, fechaFin);

            return View(data);
        }
    }
}
