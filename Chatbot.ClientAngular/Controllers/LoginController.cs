using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Chatbot.Client.Models;
using Chatbot.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HomeAngular = Chatbot.ClientAngular.Controllers;
namespace Chatbot.ClientAngular.Controllers
{
    public class LoginController : Controller
    {
        //database first data
        private static SpotDBContext _db = new SpotDBContext();
        //private readonly UserManager<UserInfo> _userManager;
        //private readonly SignInManager<UserInfo> _signInManager;
        //private readonly ILogger _logger;

        //public LoginController(
        //    UserManager<UserInfo> userManager,
        //        SignInManager<UserInfo> signInManager,
        //        ILogger<LoginController> logger)
        //{
        //    _userManager = userManager;
        //    _signInManager = signInManager;
        //    _logger = logger;
        //}
        
    

        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        //from dotnetcoreturorials.com
        // POST: Login/Create
       // [Authorize]
       [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(Login loginModel)
        {

            if (ModelState.IsValid)
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
                    return Redirect("/Home/Index");
                }
            }
            ViewBag.Message = "Incorrect Email or Password";
            return View();
        }

        private bool LoginUser(string username, string password)
        {
            //As an example. This method would go to our data store and validate that the combination is correct. 
            //For now just return true. 
            var user = _db.UserInfo.Where(x => x.Email == username && x.Password == password).FirstOrDefault();
            if (user == null )
                {
                return false;
            }
            return true;
        }

        //[Authorize]
        [HttpGet]
        public ActionResult Register()
        {

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
       // [Authorize]
       [AllowAnonymous]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                //use data here to register
               var userA = new UserInfo
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Password = user.Password,
                    ModifiedDate = DateTime.Now,
                    Active = true
                };

                if (!_db.UserInfo.Any(u => u.Email == user.Email))
                    {
                    _db.UserInfo.Add(userA);
                    _db.SaveChanges();
                    return RedirectToAction("Login/Login");
                }
                else
                {
                    ViewBag.Message = "User with this Email already Exist";
                }
                //var result = await _userManager.CreateAsync(userA,user.Password);
                //if (result.Succeeded)
                //{
                //    _logger.LogInformation("user created");                    
             }   
            return View("Register");
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/Login/Login");
        }
    }
}
