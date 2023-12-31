﻿using GesRelationClient.Models;
using GesRelationClient.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;


namespace GesRelationClient.Controllers
{
    public class AccountController : Controller 
    {
        private readonly EmployeService _employeService;
        private readonly IStringLocalizer<AccountController> _localizer;

        public AccountController(EmployeService employeService, IStringLocalizer<AccountController> localizer)
        {
            _employeService = employeService;
            _localizer = localizer;
        }

       //[HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _employeService.Authenticate(model.NomUtilisateur, model.MotDePasse);

                if (result is null)
                {
                    ViewBag.ErrorMessage = _localizer["Nom d'utilisateur ou mot de passe incorrect."];
                    return View(model);
                }

                var claims = new List<Claim>
                {
                        new Claim(ClaimTypes.Name, result.EmployeId.ToString()),
                        new Claim(ClaimTypes.Role, result.NiveauAutorisation),
                        new Claim("Nom", model.NomUtilisateur),
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var authentificationPropertie = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.Now.AddDays(1),
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authentificationPropertie);
                var claimsPrincipal = HttpContext.User;
                return RedirectToAction("Index", "Home");

            }
            else
            {
                ViewBag.ErrorMessage = _localizer["Veuillez remplir tous les champs."];
                return View(model);
            }
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }
    }
}
