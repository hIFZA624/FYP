using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using finalYearProject.Models;
using finalYearProject.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace finalYearProject.Controllers
{
   
    public class HomeController : Controller
    {
        PUCITRepository pucitRepository = new PUCITRepository();
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
            pucitRepository.Cart.Clear();
            pucitRepository.ClearAllCart();
            await  signInManager.SignOutAsync();
            return View("Login");
        }
        [Authorize]
        [HttpGet]
        public async Task<ViewResult> CommentBox(string uname)
        {
            var userFind = await userManager.FindByIdAsync(uname);
            ViewBag.uname = uname;
            ViewBag.name = userFind.firstName;
            ViewModel2 vm2 = new ViewModel2();
            vm2.Comment = pucitRepository.comments;
            
            return View("CommentBox", vm2);
        }
        [HttpPost]
        public ViewResult CreateComment(ViewModel2 c,string uname,string name)
        {
            //RestaurentRepository.receipts.Find(r => r.ReceiptID == ReceiptID);
            ViewBag.uname = uname;
            ViewBag.name = name;
            CommentBox cb = new CommentBox();
            cb.Comment = c.commentBox.Comment;
            cb.Name = name;
            cb.UserID = uname;
           DateTime dt = DateTime.Now;
            cb.date=dt.ToShortDateString();


            pucitRepository.AddCommentD(cb);
           
            ViewModel2 viewModel2 = new ViewModel2();
            viewModel2.Comment = pucitRepository.comments;

          
            return View("CommentBox", viewModel2);

        }
        [HttpGet]
        public ViewResult DeleteComment(int CommentNo,string uname,string name)
        {
            ViewBag.uname = uname;
            ViewBag.name = name;
            CommentBox c = pucitRepository.comments.Find(c => c.CommentNo == CommentNo);
            pucitRepository.comments.Remove(c);
           pucitRepository.DeleteComment(c.CommentNo);
            ViewModel2 vm2 = new ViewModel2();
            vm2.Comment = pucitRepository.comments;
            vm2.commentBox = c;
            return View("CommentBox", vm2);

        }
        [HttpGet]
        public ViewResult UpdateComment(int CommentNo,string uname, string name)
        {
            ViewBag.uname = uname;
            ViewBag.name = name;
            CommentBox c =pucitRepository.comments.Find(c => c.CommentNo == CommentNo);
            return View("UpdateComment", c);

        }
        [HttpPost]

        public ViewResult UpdateComment(CommentBox c, string uname, string name)
        {
            ViewBag.uname = uname;
            ViewBag.name = name;
            foreach (CommentBox cb in pucitRepository.comments)
            {
                if (cb.CommentNo == c.CommentNo)
                {
                    cb.Comment = c.Comment;
                    break;
                }
            }
         pucitRepository.UpdateComment(c);
            ViewModel2 vm2 = new ViewModel2();
            vm2.Comment = pucitRepository.comments;
            vm2.commentBox = c;
            return View("CommentBox", vm2);
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
        public ViewResult About(string uname)
        {
            ViewBag.uname = uname;
            return View();
        }
        [HttpGet]
        public ViewResult RestaurentContactUs(string uname)
        {
            ViewBag.uname = uname;
            return View("RestaurentContactUs");

        }
        [HttpPost]
        public ViewResult RestaurentContactUs(ContactUs u,string uname)
        {
            ViewBag.uname = uname;
            pucitRepository.AddContactUsD(u);
            ModelState.Clear();
            return View("RestaurentContactUs");

        }
    }
}