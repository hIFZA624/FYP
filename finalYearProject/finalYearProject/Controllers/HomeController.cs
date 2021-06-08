using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using finalYearProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace finalYearProject.Controllers
{
   
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        
        public HomeController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            
        }
        [HttpGet]
        
        public ViewResult login()
        {
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> login(Login userdata)
        {
            bool res = false;

            if (ModelState.IsValid)
            {
                
                var result = await signInManager.PasswordSignInAsync(userdata.username, userdata.password, userdata.RememberMe, false);
               
                if (result.Succeeded)
                {
                    var user = await userManager.FindByEmailAsync(userdata.username);
                    ViewBag.uname = user.Id;
                    foreach (var role in roleManager.Roles.ToList())
                    {
                        if (await userManager.IsInRoleAsync(user, role.Name))
                        {
                            res = true;
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

                }

                if (res == true && result.Succeeded)
                {
                    return View("Admin");
                }
                else if (res == false && result.Succeeded )
                    return View("Cutomer");
            }

            return View(userdata);
        }
        [HttpGet]
       
        public ViewResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<ViewResult> Register(Registration userdata)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { Email = userdata.email, UserName = userdata.email, firstName = userdata.firstname, LastName = userdata.lastname };
                var result = await userManager.CreateAsync(user, userdata.password);
                if (result.Succeeded)
                {
                    var users = await userManager.FindByEmailAsync(userdata.email);
                    ViewBag.uname = users.Id;
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return View("Cutomer");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);

                }
               
            }
            return View(userdata);
        }
        [Authorize]
        public ViewResult Admin(string uname)
        {
            ViewBag.uname = uname;
            return View();
        }
        [Authorize]
        public ViewResult CafeAdmin()
        {
            return View();
        }
        [Authorize]
        [HttpGet]
        public ViewResult ChangePassword(string email)
        {
            ViewBag.uname =email;
            return View();
        }
        [HttpPost]
        public async Task<ViewResult> ChangePassword(ChangePasswordModel model,string email)
        {
            ViewBag.uname = email;
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(email);
                var result = await userManager.ChangePasswordAsync(user, model.currentPassword, model.newPassword);
                if (result.Succeeded)
                {
                     ViewBag.IsSuccess = true;
                     ModelState.Clear();
                     return View();
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
        [Authorize]
        [HttpGet]
        public async  Task<ViewResult> Profile(string Email)
        {
            ViewBag.uname = Email;
            var userFind = await userManager.FindByIdAsync(Email);
            var user = new Users();
            user.id = userFind.Id;
            user.FirstName = userFind.firstName;
            user.LastName = userFind.LastName;
            user.username = userFind.UserName; 
            return View("Profile", user);   
        }
        [HttpPost]
        public async Task<ViewResult> Profile(Users user,string email)
        {
            ViewBag.uname = email;
            if (ModelState.IsValid)
            {
                var userFind = await userManager.FindByIdAsync(user.id);
                userFind.firstName = user.FirstName;
                userFind.LastName = user.LastName;
                userFind.Email = user.username;
                userFind.UserName = user.username;
                var result = await userManager.UpdateAsync(userFind);
                // var users = userManager.Users;
                if (result.Succeeded)
                {
                    await signInManager.SignOutAsync();
                    return View("Login");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);

                }
            }
            return View(user);
        }
        public async Task<ActionResult> Logout()
        {
            await  signInManager.SignOutAsync();
            return View("Login");
        }
        [Authorize]
        public async  Task<ViewResult> Customer(string uname)
        {
            var userFind = await userManager.FindByIdAsync(uname);
            ViewBag.uname=userFind.Id;
            return View("Cutomer");
        }
       
        [HttpGet]
        public async Task<ViewResult> userProfile(string uname)
        {
            ViewBag.uname = uname;
            var userFind = await userManager.FindByIdAsync(uname);
            var user = new Users();
            user.id = userFind.Id;
            user.FirstName = userFind.firstName;
            user.LastName = userFind.LastName;
            user.email = userFind.Email;
            return View("userProfile", user);
          
        }
       
        [HttpGet]
        public ViewResult ChangePasswd(string uname)
        {
            ViewBag.uname = uname;
            return View();
        }
        public ViewResult About()
        {
            return View();
        }
    }
}