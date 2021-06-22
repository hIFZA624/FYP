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
        PUCITRepository pucitRepository = new PUCITRepository();
        private readonly SignInManager<ApplicationUser> signInManager;

        public AdministratorController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet] 
       
        public ViewResult CreateRole(string uname)
        {
            ViewBag.uname = uname;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(Roles roleS,string uname)
        {
            ViewBag.uname = uname;
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole();
                identityRole.Name = roleS.RoleName;
                IdentityResult result = await roleManager.CreateAsync(identityRole);
                if(result.Succeeded)
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
                    foreach (var role in roleManager.Roles)
                    {
                        if (role.Name != "AppAdministrator" && role.Name != "PUCITCafeAdmin" && role.Name != "PharmacyCafeAdmin" && role.Name != "RestaurantAdmin" && role.Name != "RestaurantOwner")
                        {
                            Roles rule = new Roles();
                            rule.id = role.Id;
                            rule.RoleName = role.Name;
                            roling.AddLast(rule);
                        }
                    }
                    return View("ViewRoles", roling);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }
        public ViewResult ViewRoles(string uname)
        {
            ViewBag.uname = uname;
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
        public async Task<ViewResult> EditUserInRole(string roleId,string uname)
        {
           ViewBag.roleId = roleId;
            ViewBag.uname = uname;
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
        public async Task<IActionResult> EditUserInRole(List<UserRole> model, string roleId,string uname)
        {
            ViewBag.uname = uname;
            var role1 = await roleManager.FindByIdAsync(roleId);

            for (int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].userId);
                IdentityResult identityResult = null;
                if (model[i].isSelected && !(await userManager.IsInRoleAsync(user, role1.Name)))
                {
                    identityResult = await userManager.AddToRoleAsync(user, role1.Name);
                }
                else if (!model[i].isSelected && await userManager.IsInRoleAsync(user, role1.Name))
                {
                    identityResult = await userManager.RemoveFromRoleAsync(user, role1.Name);
                }
                else
                    continue;
            }
           
            EditRole model1 = new EditRole();
            model1.users = new List<string>();
            if (role1 != null)
            {

                model1.Id = role1.Id;
                model1.RoleName = role1.Name;
            }
            foreach (var user in userManager.Users.ToList())
            {
                if (await userManager.IsInRoleAsync(user, role1.Name))
                {
                    model1.users.Add(user.UserName);
                }
            }
            return View("EditRole",model1);
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
        public async Task<ViewResult> EditRole(string id,string uname)
        {
            ViewBag.uname = uname;
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
        public async Task<IActionResult> EditRole(EditRole model,string uname)
        {
            ViewBag.uname = uname;
            model.users = new List<string>();
            var role1 = await roleManager.FindByIdAsync(model.Id);
            foreach (var user in userManager.Users.ToList())
            {
                if (await userManager.IsInRoleAsync(user, role1.Name))
                {
                    model.users.Add(user.UserName);
                }
            }
            
            if (ModelState.IsValid)
            {
                var role2 = await roleManager.FindByIdAsync(model.Id);

                if (role2 != null)
                {
                    role2.Name = model.RoleName;
                    var result = await roleManager.UpdateAsync(role2);
                    if(result.Succeeded)
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
                            if (role.Name != "AppAdministrator" && role.Name != "PUCITCafeAdmin" && role.Name != "PharmacyCafeAdmin" && role.Name != "RestaurantAdmin")
                            {
                                Roles rule = new Roles();
                                rule.id = role.Id;
                                rule.RoleName = role.Name;
                                roling.AddLast(rule);
                            }
                        }
                        return View("ViewRoles", roling);
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
        public ViewResult ListUsers(string uname)
        {
              ViewBag.uname = uname;
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
       public async Task<IActionResult> EditUser(string id,string uname)
        {
            ViewBag.uname = uname;
            var userFind = await userManager.FindByIdAsync(id);
                //FIND A USER ROLES
                var userRoles = await userManager.GetRolesAsync(userFind);
                var user = new EditUser();
                user.id = userFind.Id;
                user.FirstName = userFind.firstName;
                user.LastName = userFind.LastName;
                user.email = userFind.Email;
                user.Roles = userRoles;
                return View("EditUser", user);
            
          
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(EditUser userGet,string uname)
        {
            ViewBag.uname = uname;
            var userFind = await userManager.FindByIdAsync(userGet.id);
            var userRoles = await userManager.GetRolesAsync(userFind);
            userGet.Roles = userRoles;
            if (ModelState.IsValid)
            {
              
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
                    return View("ListUsers", userlist);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);

                }
              
            }
          
            return View(userGet);
        }
        [HttpGet]
        public ViewResult AddUser(string uname)
        {
            ViewBag.uname = uname; 
            return View();
        }
        [HttpPost]
        public async Task<ViewResult> AddUser(Registration userdata,string uname)
        {
            ViewBag.uname = uname;
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
       public async Task<IActionResult> DeleteUser(string id,string uname)

        {
            ViewBag.uname = uname;
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
        public async Task<IActionResult> DeleteRole(string id, string uname)
        {
            ViewBag.uname = uname;
            var roleFind = await roleManager.FindByIdAsync(id);
            if (roleFind == null)
            {
                ViewBag.ErrorMessege = $"Role with {id} cannot be found";
                return View("Not Found");
            }
            //FIND A USER ROLES

            else
            {
                var result = await roleManager.DeleteAsync(roleFind);
                if (result.Succeeded)
                {
                   
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);

                }
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
                foreach (var role in roleManager.Roles)
                {
                    if (role.Name != "AppAdministrator" && role.Name != "PUCITCafeAdmin" && role.Name != "PharmacyCafeAdmin" && role.Name != "RestaurantAdmin" && role.Name != "RestaurantOwner")
                    {
                        Roles rule = new Roles();
                        rule.id = role.Id;
                        rule.RoleName = role.Name;
                        roling.AddLast(rule);
                    }
                }
                return View("ViewRoles", roling);
            }
           
        }
        [HttpGet]
        public ViewResult Complaints()
        {
            return View(pucitRepository.complaint);
        }
    }
}