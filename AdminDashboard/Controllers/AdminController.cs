using AdminDashboard.Models.DTO;
using AdminDashboard.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdminDashboard.Controllers
{
    public class AdminController : Controller
    {
        private readonly AdminService _service;

        public AdminController(AdminService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Usuarios(string? search)
        {
            var token = HttpContext.Session.GetString("TOKEN");

            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            var data = await _service.GetUsuarios(token, search);

            return View(data);
        }

        public IActionResult CrearUsuario()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CrearUsuario(CrearUsuarioDto dto)
        {
            var token = HttpContext.Session.GetString("TOKEN");

            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            var response = await _service.CrearUsuario(token, dto);

            if (!response.Success)
            {
                ViewBag.Error = response.Message;
                return View(dto);
            }

            TempData["Success"] = response.Message;
            return RedirectToAction("Usuarios");
        }
        public IActionResult EditarUsuario(int id)
        {
            return View(new EditarUsuarioDto { IdUsuario = id });
        }

        [HttpPost]
        public async Task<IActionResult> EditarUsuario(EditarUsuarioDto dto)
        {
            var token = HttpContext.Session.GetString("TOKEN");

            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            var response = await _service.EditarUsuario(token, dto);

            if (!response.Success)
            {
                ViewBag.Error = response.Message;
                return View(dto);
            }

            TempData["Success"] = response.Message;
            return RedirectToAction("Usuarios");
        }
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            var token = HttpContext.Session.GetString("TOKEN");

            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            var response = await _service.EliminarUsuario(token, id);

            if (!response.Success)
            {
                TempData["Error"] = response.Message;
            }
            else
            {
                TempData["Success"] = response.Message;
            }

            return RedirectToAction("Usuarios");
        }
        public async Task<IActionResult> Transacciones(DateTime? fechaInicio, DateTime? fechaFin, string? tipo, string? usuario)
        {
            var token = HttpContext.Session.GetString("TOKEN");

            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            var data = await _service.GetTransacciones(token, fechaInicio, fechaFin, tipo, usuario);

            return View(data);
        }

        public async Task<IActionResult> Grupos()
        {
            var token = HttpContext.Session.GetString("TOKEN");

            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            var data = await _service.GetGrupos(token);

            return View(data);
        }
        public async Task<IActionResult> GrupoDetalle(string codgrupo)
        {
            var token = HttpContext.Session.GetString("TOKEN");

            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            var data = await _service.GetGrupoDetalle(token, codgrupo);

            return View(data);
        }
    }
}
