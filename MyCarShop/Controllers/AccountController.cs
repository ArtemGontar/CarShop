using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using MyCarShop.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System.Security.Claims;
using Microsoft.Owin.Security;
using MyCarShop.App_Start;

namespace MyCarShop.Controllers
{
    public class AccountController : Controller
    {
        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;
        
        public ApplicationSignInManager SignInManager =>  HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
         
        private ApplicationUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        // GET: Account
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { UserName = model.Email, Email = model.Email, Year = model.Year };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, "user");
                    await SignInManager.SignInAsync(user, isPersistent: true, rememberBrowser: false);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(model);
        }
        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                User user = await UserManager.FindAsync(model.Email, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Неверный логирн или пароль");
                }
                else
                {
                    ClaimsIdentity claim = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    if (String.IsNullOrEmpty(returnUrl))
                        return RedirectToAction("Index", "Home");
                }
            }
            ViewBag.returnUrl = returnUrl;
            return View(model);
        }
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login");
        }
        public async Task<ActionResult> Delete()
        {
            User user = await UserManager.FindByEmailAsync(User.Identity.Name);
            if (user != null)
            {
                return View();
            }
            return RedirectToAction("Login", "Account");
            
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed()
        {
            User user = await UserManager.FindByEmailAsync(User.Identity.Name);
            if (user != null)
            {
                IdentityResult result = await UserManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Logout","Account");
                }
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<ActionResult> Edit()
        {
            User user = await UserManager.FindByEmailAsync(User.Identity.Name);
            if (user != null)
            {
                EditModel model = new EditModel { Year = user.Year }; 
                return View(model);
            }
            return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        public async Task<ActionResult> Edit(EditModel model)
        {
            User user = await UserManager.FindByEmailAsync(User.Identity.Name);
            if (user != null)
            {
                user.Year = model.Year;
                IdentityResult result = await UserManager.UpdateAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");
                else
                    ModelState.AddModelError("", "Что-то пошло не так");
            }
            else
            {
                ModelState.AddModelError("", "Пользователь не найден");
            }
            return View(model);
        }
    }
}