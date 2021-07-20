using LanchesMC.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanchesMC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManeger;

        public AccountController(UserManager<IdentityUser> userManenger,
                                 SignInManager<IdentityUser> signinManeger )
        {
            _userManager = userManenger;
            _signInManeger = signinManeger;
        }

        [HttpGet]
        public IActionResult Login(string returUrl)
        {
            return View(new LoginViewModel()
            {
                ReturnUrl = returUrl
            });
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid)
                return View(loginVM);


            var User = await _userManager.FindByNameAsync(loginVM.UserName);

            if(User != null)
            {
                var result = await _signInManeger.PasswordSignInAsync(User,
                    loginVM.Password, false, false);


                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(loginVM.ReturnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    return RedirectToAction(loginVM.ReturnUrl);
                }

            }
            ModelState.AddModelError("", "Usuário/Senha Inválidos ou não Localizados");
            return View(loginVM); 
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();  

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Register(LoginViewModel registroVM)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser() { UserName = registroVM.UserName };

                var result = await _userManager.CreateAsync(user, registroVM.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }


            }
            return View(registroVM);

        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManeger.SignOutAsync();
            return RedirectToAction("Index", "Home");

        }




    } 
   
}
