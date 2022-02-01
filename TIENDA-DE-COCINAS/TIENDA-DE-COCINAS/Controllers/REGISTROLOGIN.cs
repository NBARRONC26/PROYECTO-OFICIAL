using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TIENDA_DE_COCINAS.Models;

namespace TIENDA_DE_COCINAS.Controllers
{
    public class REGISTROLOGIN : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public REGISTROLOGIN(UserManager<IdentityUser> usrMgr, SignInManager<IdentityUser> signmanager) {
            _userManager = usrMgr;
            _signInManager = signmanager;
        }

        public IActionResult Index()
        {
            return View(_userManager.Users);
        }

        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registro(user modelo)
        {
           
            
            if (ModelState.IsValid)
            {
                var usuario = new IdentityUser
                {
                    UserName = modelo.email,
                    Email = modelo.email,
                };

                var resultado = await _userManager.CreateAsync(usuario, modelo.password1);

                //var usuarioRegistrado = usuario.UserName.ToString();

                if (resultado.Succeeded)
                {
                    await _userManager.AddToRoleAsync(usuario, "Visitante");
                    await _signInManager.SignInAsync(usuario, isPersistent : false);
                    //var token = await _userManager.GenerateEmailConfirmationTokenAsync(usuario);
                    //var callbackUrl = Url.Action("CuentaConfirmada", "Account", new { id = usuario.Id, token = token }, protocol: HttpContext.Request.Scheme);

                    //await _iemailSender.SendEmailAsync(modelo.Email, "Confirmar cuenta", $"Por favor haz clic en el siguiente enlace para confirma tu email: <a href='{callbackUrl}'>Haz clic aquí</a>");
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            else
            {
                ModelState.AddModelError("", "No se ha podido registrar su usuario");
            }

            return View(modelo);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(user2 usuario)
        {
            if (ModelState.IsValid)
            {
                var resultado = await _signInManager.PasswordSignInAsync(usuario.email, usuario.password1, usuario.recordar, false);

                //var usuarioLogueado = usuario.email.ToString();

                if (resultado.Succeeded)
                {   return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "El intento de Login ha sido fallido.");
                }
            }

            return View(usuario);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }

    }
}
