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
    [Authorize(Roles = "AppAdministrator")]
    public class AdministratorController : Controller
    {
         
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        private readonly SignInManager<ApplicationUser> signInManager;

        public AdministratorController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet] 
       
        public ViewResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(Roles roleS)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole();
                identityRole.Name = roleS.RoleName;
                IdentityResult result = await roleManager.CreateAsync(identityRole);
                if(result.Succeeded)
                {
                    return RedirectToAction("ViewRoles");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }
        public ViewResult ViewRoles()
        {
            List<Roles> listRoles = new List<Roles>();
            LinkedList<Roles> roling = new LinkedList<Roles>();
            var roles = roleManager.Roles;
            foreach (var role in roleManager.Roles)
            {
                if (role.Name == "AppAdministrator")
                {
                    Roles rule = new Roles();
                    rule.id = role.Id;
                    rule.RoleName = role.Name;
                    roling.AddFirst(rule);
                }
            }
            foreach (var role in roleManager.Roles)
            {
                if (role.Name == "PUCITCafeAdmin")
                {
                    Roles rule = new Roles();
                    rule.id = role.Id;
                    rule.RoleName = role.Name;
                    roling.AddLast(rule);
                }
            }
            foreach (var role in roleManager.Roles)
            {
                if (role.Name == "PharmacyCafeAdmin")
                {
                    Roles rule = new Roles();
                    rule.id = role.Id;
                    rule.RoleName = role.Name;
                    roling.AddLast(rule);
                }
            }
            foreach (var role in roleManager.Roles)
            {
                if (role.Name == "RestaurantAdmin")
                {
                    Roles rule = new Roles();
                    rule.id = role.Id;
                    rule.RoleName = role.Name;
                    roling.AddLast(rule);
                }
            }
            foreach (var role in roleManager.Roles)
            {
                if (role.Name == "RestaurantOwner")
                {
                    Roles rule = new Roles();
                    rule.id = role.Id;
                    rule.RoleName = role.Name;
                    roling.AddLast(rule);
                }
            }
            foreach(var role in roleManager.Roles)
            {
                if(role.Name!= "AppAdministrator" && role.Name != "PUCITCafeAdmin" && role.Name != "PharmacyCafeAdmin" && role.Name != "RestaurantAdmin" && role.Name != "RestaurantOwner")
                {
                    Roles rule = new Roles();
                    rule.id = role.Id;
                    rule.RoleName = role.Name;
                    roling.AddLast(rule);
                }
            }
            return View(roling);
        }
        [HttpGet]  
        public async Task<ViewResult> EditUserInRole(string roleId)
        {
           ViewBag.roleId = roleId;
            var role = await roleManager.FindByIdAsync(roleId);
            List<UserRole> listRoles = new List<UserRole>();

            foreach (var user in userManager.Users.ToList())
            {
                var userRoles = new UserRole();
                userRoles.userId = user.Id;
                userRoles.userName = user.UserName;

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoles.isSelected = true;
                }
                else
                {
                    userRoles.isSelected = false;
                }
                listRoles.Add(userRoles);
            }
            return View("EditUserInRole", listRoles);
        }
        [HttpPost]
        public async Task<IActionResult> EditUserInRole(List<UserRole> model, string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);

            for (int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].userId);
                IdentityResult identityResult = null;
                if (model[i].isSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    identityResult = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].isSelected && await userManager.IsInRoleAsync(user, role.Name))
                {
                    identityResult = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                    continue;
            }

            return RedirectToAction("ViewRoles");
        }
        public ViewResult Return()
        {
            return View();
        }
       
        public ViewResult HomePage()
        {
            return View();
        }
        [HttpGet]
        public async Task<ViewResult> EditRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            EditRole model = new EditRole();
            model.users = new List<string>();
            if (role!=null)
            {
              
                model.Id = role.Id;
                model.RoleName = role.Name;
            }
          foreach(var user in userManager.Users.ToList())
            {
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.users.Add(user.UserName);
                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<ViewResult> EditRole(EditRole model)
        {
            if (ModelState.IsValid)
            {
                var role = await roleManager.FindByIdAsync(model.Id);

                if (role != null)
                {
                    role.Name = model.RoleName;
                    var result = await roleManager.UpdateAsync(role);
                    if(result.Succeeded)
                    {
                        return View("Return");
                    }   
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);

                        }
                    }
                }      
            }  
            return View(model);
        }
        [HttpGet]
        public ViewResult ListUsers()
        {
                List<Users> userlist = new List<Users>();
                var users = userManager.Users;
                foreach (var role in userManager.Users)
                {
                    Users user = new Users();
                    user.id = role.Id;
                    user.FirstName = role.firstName;
                    user.LastName = role.LastName;
                    user.email = role.Email;
                    userlist.Add(user);

                }

                return View(userlist);
            
            
        }
        [HttpGet]
       public async Task<IActionResult> EditUser(string id)
        {
            
                var userFind = await userManager.FindByIdAsync(id);
                //FIND A USER ROLES
                var userRoles = await userManager.GetRolesAsync(userFind);
                var user = new Users();
                user.id = userFind.Id;
                user.FirstName = userFind.firstName;
                user.LastName = userFind.LastName;
                user.email = userFind.Email;
                user.Roles = userRoles;
                return View("EditUser", user);
            
          
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(Users userGet)
        {
            if (ModelState.IsValid)
            {
                var userFind = await userManager.FindByIdAsync(userGet.id);
                userFind.firstName = userGet.FirstName;
                userFind.LastName = userGet.LastName;
                userFind.Email = userGet.email;
                var result = await userManager.UpdateAsync(userFind);
                if (result.Succeeded)
                {
                    List<Users> userlist = new List<Users>();
                    var users = userManager.Users;
                    foreach (var role in userManager.Users)
                    {

                        Users user = new Users();
                        user.id = role.Id;
                        user.FirstName = role.firstName;
                        user.LastName = role.LastName;
                        user.email = role.Email;
                        userlist.Add(user);
                    }
                    return View(userlist);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View();
        }
        [HttpGet]
        public ViewResult AddUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<ViewResult> AddUser(Registration userdata)
        {
            if (ModelState.IsValid)
            {
                var users1 = new ApplicationUser { Email = userdata.email, UserName = userdata.email, firstName = userdata.firstname, LastName = userdata.lastname };
                var result = await userManager.CreateAsync(users1, userdata.password);
                if (result.Succeeded)
                {
                    List<Users> userlist = new List<Users>();
                    var users = userManager.Users;
                    foreach (var role in userManager.Users)
                    {

                        Users user = new Users();
                        user.id = role.Id;
                        user.FirstName = role.firstName;
                        user.LastName = role.LastName;
                        user.email = role.Email;
                        userlist.Add(user);

                    }
                    return View("ListUsers",userlist);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);

                }

            }
            return View(userdata);
        }
       public async Task<IActionResult> DeleteUser(string id)

        {
            var userFind = await userManager.FindByIdAsync(id);
            if(userFind==null)
            {
                ViewBag.ErrorMessege = $"User with {id} cannot be found";
                return View("Not Found");
            }
            else
            {
               var result= await userManager.DeleteAsync(userFind);
                if(result.Succeeded)
                {
                    List<Users> userlist = new List<Users>();
                    var users = userManager.Users;
                    foreach (var role in userManager.Users)
                    {

                        Users user = new Users();
                        user.id = role.Id;
                        user.FirstName = role.firstName;
                        user.LastName = role.LastName;
                        user.email = role.Email;
                        userlist.Add(user);

                    }
                    return View("ListUsers", userlist);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);

                }
            }
            return View("ListUsers");
           
        }
    }
}