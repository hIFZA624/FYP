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
                ViewBag.uname = userdata.username;
                var result = await signInManager.PasswordSignInAsync(userdata.username, userdata.password, userdata.RememberMe, false);
                var user = await userManager.FindByEmailAsync(userdata.username);
                if (result.Succeeded)
                {
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
        public ViewResult Admin()
        {
            return View();
        }
        public ViewResult CafeAdmin()
        {
            return View();
        }
        [HttpGet]
        public ViewResult ChangePassword(string email)
        {
            ViewBag.uname =email;
            return View();
        }
        [HttpPost]
        public async Task<ViewResult> ChangePassword(ChangePasswordModel model,string email)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(email);
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
        [HttpGet]
        public async  Task<ViewResult> Profile(string email)
        {
            ViewBag.uname = email;
            var userFind = await userManager.FindByEmailAsync(email);
            var user = new Users();
            user.id = userFind.Id;
            user.FirstName = userFind.firstName;
            user.LastName = userFind.LastName;
            user.email = userFind.Email; 
         
            return View("Profile", user);   
        }
        [HttpPost]
        public async Task<ViewResult> Profile(Users user)
        {
            if (ModelState.IsValid)
            {
                var userFind = await userManager.FindByIdAsync(user.id);
                userFind.firstName = user.FirstName;
                userFind.LastName = user.LastName;
                userFind.Email = user.email;
                userFind.UserName = user.email;
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
        public ViewResult Customer()
        {
            return View("Cutomer");
        }
        [HttpGet]
        public async Task<ViewResult> userProfile(string uname)
        {
            var userFind = await userManager.FindByEmailAsync(uname);
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
            return View();
        }
    }
}