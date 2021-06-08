using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using finalYearProject.Models;
using finalYearProject.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace finalYearProject.Controllers
{
    [Authorize]
    public class CafeController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        public CafeController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
            
        }
        PUCITRepository pucitRepository = new PUCITRepository();
        public ActionResult HomePage(string uname,string cafe)
        {
            ViewBag.cafe = cafe;
            ViewBag.uname = uname;
            return View();
        }
        public async Task<ViewResult> PucitCafe(string uname,string cafe)
        {

            var user = await userManager.FindByIdAsync(uname);
            ViewBag.uname = user.Id;
            ViewBag.cafe = cafe;
            ViewBag.email = user.Email;
            PUCITCategoryViewcs pUCITCategoryViewcs = new PUCITCategoryViewcs();
            pUCITCategoryViewcs.Category = pucitRepository.Category;
            return View("PucitCafe",pUCITCategoryViewcs);
        }
        public async Task<ViewResult> PharmacyCafe(string uname,string cafe)
        {

            var user = await userManager.FindByIdAsync(uname);
            ViewBag.uname = user.Id;
            ViewBag.cafe = cafe;

            return View(pucitRepository.CategoryPharmacy);
        }
        public async Task<ViewResult> ProductsView(string CategoryName,string uname,string cafe,string email)
        {
            ViewBag.Categoryname = CategoryName;
            var user = await userManager.FindByIdAsync(uname);
            ViewBag.uname = user.Id;
            ViewBag.email = email;
            ViewBag.cafe = cafe;

            PUCITShowViewModel pUCITShowViewModel = new PUCITShowViewModel();
            pucitRepository.temp.Clear();
            foreach (PUCITProducts product in pucitRepository.products)
            {
                if (product.Category == CategoryName)
                {
                    pucitRepository.temp.Add(product);
                }
            }
            pUCITShowViewModel.productlist = pucitRepository.temp;
            pUCITShowViewModel.Category = pucitRepository.Category;
            return View(pUCITShowViewModel);
        }
        public async Task<ViewResult> ProductViewPharmacy(string CategoryName, string uname, string cafe)
        {
            ViewBag.Categoryname = CategoryName;
            var user = await userManager.FindByIdAsync(uname);
            ViewBag.uname = user.Id;

            ViewBag.cafe = cafe;

            PUCITShowViewModel pUCITShowViewModel = new PUCITShowViewModel();
            pucitRepository.temp.Clear();
            foreach (PUCITProducts product in pucitRepository.productsPharmacy)
            {
                if (product.Category == CategoryName)
                {
                    pucitRepository.temp.Add(product);
                }
            }
            pUCITShowViewModel.productlist = pucitRepository.temp;
            pUCITShowViewModel.Category = pucitRepository.CategoryPharmacy;
            return View(pUCITShowViewModel);
           
        }
        
        public async Task<ViewResult> SubmitOrder(string uname,string cafe,string email)
        {
            PUCITShowViewModel pUCITShowViewModel = new PUCITShowViewModel();
            var user = await userManager.FindByIdAsync(uname);
            ViewBag.uname = user.Id;
            ViewBag.cafe = cafe;
            ViewBag.email = email;
            pUCITShowViewModel.Category = pucitRepository.Category;
            return View(pUCITShowViewModel);
        }
        public async Task<ViewResult> SubmitOrderPharmacy(string uname, string cafe)
        {
            PUCITShowViewModel pUCITShowViewModel = new PUCITShowViewModel();
            var user = await userManager.FindByIdAsync(uname);
            ViewBag.uname = user.Id;
            ViewBag.cafe = cafe;
            pUCITShowViewModel.Category = pucitRepository.CategoryPharmacy;
            return View(pUCITShowViewModel);
        }
    }
}