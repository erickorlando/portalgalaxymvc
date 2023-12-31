﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PortalGalaxy.Common;
using PortalGalaxy.Models;
using PortalGalaxy.WebMvc.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using PortalGalaxy.WebMvc.Models;

namespace PortalGalaxy.WebMvc.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserProxy _proxy;
        private readonly IUbigeoProxy _ubigeoProxy;

        public UserController(IUserProxy proxy, IUbigeoProxy ubigeoProxy)
        {
            _proxy = proxy;
            _ubigeoProxy = ubigeoProxy;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDtoRequest modelo)
        {
            try
            {
                var response = await _proxy.LoginAsync(modelo);
                if (response.Success)
                {
                    // Hacer el proceso de Login
                    var handler = new JwtSecurityTokenHandler();
                    var jwt = handler.ReadJwtToken(response.Token);

                    // Leer los Claims
                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

                    identity.AddClaims(jwt.Claims);

                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    // Guardamos la sesion
                    HttpContext.Session.SetString(Constantes.JwtToken, response.Token);
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("ErrorMessage", response.ErrorMessage ?? "Error");
                return View(modelo);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("ErrorMessage", ex.Message);
                return View(modelo);
            }
        }

        public async Task<IActionResult> Register()
        {
            var vm = new RegisterViewModel
            {
                ListaDepartamentos = await CargarDepartamentos()
            };

            return View(vm);
        }

        private async Task<List<SelectListItem>> CargarDepartamentos()
        {
            // Capturamos la URL del Frontend
            _ubigeoProxy.UrlBase = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";

            var listaDepartamentos = await _ubigeoProxy.ListarDepartamentos();

            return new List<SelectListItem>(listaDepartamentos.Select(p => new SelectListItem(p.Nombre, p.Codigo)));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel modelo)
        {
            try
            {
                
                var response = await _proxy.RegisterAsync(modelo.Input);
                if (response.Success)
                {
                    return RedirectToAction(nameof(Login));
                }

                ModelState.AddModelError("ErrorMessage", response.ErrorMessage ?? "Error");
                modelo.ListaDepartamentos = await CargarDepartamentos();
                return View(modelo);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("ErrorMessage", ex.Message);
                return View(modelo);
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.SetString(Constantes.JwtToken, string.Empty);

            return RedirectToAction("Index", "Home");
        }
    }
}
