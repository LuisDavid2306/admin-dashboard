using AdminDashboard.Models;
using AdminDashboard.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdminDashboard.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthService _service;

        public AuthController(AuthService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var token = await _service.LoginAsync(model);

            if (token == null)
            {
                ViewBag.Error = "Login incorrecto";
                return View();
            }
            HttpContext.Session.SetString("TOKEN", token);

            return RedirectToAction("Index", "Dashboard");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
