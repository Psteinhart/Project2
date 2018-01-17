using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Chatbot.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chatbot.Client.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        //from dotnetcoreturorials.com
        // POST: Login/Create
       // [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserInfo loginModel)
        {
            if (LoginUser(loginModel.Email, loginModel.Password))
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, loginModel.Email)
            };

                var userIdentity = new ClaimsIdentity(claims, "login");

                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);

                //Just redirect to our index after logging in. 
                return Redirect("/Home/About");
            }
            return View();
        }

        private bool LoginUser(string username, string password)
        {
            //As an example. This method would go to our data store and validate that the combination is correct. 
            //For now just return true. 
            return true;
        }

        [Authorize]
        [HttpGet]
        public ActionResult Register()
        {

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]

        public ActionResult Register(UserInfo acc)
        {
            if (ModelState.IsValid)
            {
                return View("Register");
            }
            return RedirectToAction("Login", "Register");
            // return View(acc);
        }
    }
}
