using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using finalYearProject.Models;
using finalYearProject.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace finalYearProject.Controllers
{
    [Authorize(Roles = "PUCITCafeAdmin,AppAdministrator")]
    public class PUCITAdminController : Controller
    {
        PUCITRepository pucitRepository = new PUCITRepository();
        private readonly IHostingEnvironment hostingEnvironment;
        public PUCITAdminController(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }
        public ViewResult AdminHomePage()
        {
            PUCITCategoryViewcs pUCITCategoryView = new PUCITCategoryViewcs();
            pUCITCategoryView.Category = pucitRepository.Category;

            return View(pUCITCategoryView);
        }
        public ViewResult PUCITCafe()
        {
            PUCITCategoryViewcs pUCITCafeCategory = new PUCITCategoryViewcs();
            pUCITCafeCategory.Category = pucitRepository.Category;
            return View(pUCITCafeCategory);
        }
        [HttpGet]
        public ViewResult AddCategory()
        {
          
            PUCITCafeCategoryViewModel pUCITCafeCategory = new PUCITCafeCategoryViewModel();
            pUCITCafeCategory.Category = pucitRepository.Category;
            return View(pUCITCafeCategory);
        }
        [HttpPost]
        public ViewResult AddCategory(PUCITCafeCategory category)
        {
            string uniqueFileName = null;
            if (category.Photo != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + category.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                category.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            pucitRepository.addPucitProductCategory(uniqueFileName, category.CategoryName, category.Description);
            PUCITCategoryViewcs pUCITCategoryView = new PUCITCategoryViewcs();
            pUCITCategoryView.Category = pucitRepository.Category;
            pUCITCategoryView.title = "Pucit Product Categories";
            return View("CategoryView", pUCITCategoryView);
        }

        [HttpGet]
        public ViewResult AddProduct()
        {
           
            PUCITCafeViewModel pUCITCafeViewModel = new PUCITCafeViewModel();
            pUCITCafeViewModel.Category = pucitRepository.Category;
            // pUCITCafeViewModel.title = "";
            pUCITCafeViewModel.addProduct = null;
            return View("AddProduct", pUCITCafeViewModel);
        }
        [HttpPost]
        public ViewResult AddProduct(PUCITProducts food)
        {
           
            string uniqueFileName = null;
            if (food.Photo != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + food.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                food.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            pucitRepository.addPucitProduct(food.Category, food.Name, food.Price, food.Quantity, uniqueFileName);
            PUCITShowViewModel pUCITCategory = new PUCITShowViewModel();
            pUCITCategory.Category = pucitRepository.Category;
            pUCITCategory.productlist = pucitRepository.products;
            return View("ProductsView", pUCITCategory);
        }
        public ViewResult ProductView(String CategoryName)
        {
            pucitRepository.temp.Clear();
            foreach (PUCITProducts product in pucitRepository.products)
            {
                if (product.Category == CategoryName)
                {
                    pucitRepository.temp.Add(product);
                }
            }
            PUCITShowViewModel pucitShowViewModel = new PUCITShowViewModel();
            pucitShowViewModel.productlist = pucitRepository.temp;
            pucitShowViewModel.Category = pucitRepository.Category;
            return View("ProductsView", pucitShowViewModel);
        }
        public ViewResult CategoryView()
        {
          
            PUCITCategoryViewcs pUCITCategoryView = new PUCITCategoryViewcs();
            pUCITCategoryView.Category = pucitRepository.Category;
            pUCITCategoryView.title = "Pucit Product Categories";
            return View("CategoryView", pUCITCategoryView);
        }
        public ViewResult PucitCategoryDelete(int id)
        {
            pucitRepository.PUCITCategorydelete(id);
            PUCITCategoryViewcs pUCITCategoryView = new PUCITCategoryViewcs();
            pUCITCategoryView.Category = pucitRepository.Category;
            pUCITCategoryView.title = "Pucit Product Categories";
            return View("CategoryView", pUCITCategoryView);
        }
        public ViewResult Detalis(int id)
        {

            return View();
        }
        /*public ViewResult ProductViewLunch()
        {
            return View("ProductsView", pucitRepository.lunch);
        }
        [HttpGet]
        public ViewResult AddPucitBreakFast()
        {
            PUCITCafeViewModel pUCITCafeViewModel = new PUCITCafeViewModel();
            pUCITCafeViewModel.controller_Name = "/Admin/AddPucitBreakFast";
            pUCITCafeViewModel.title = "Add a BreakFast Product";
            return View("AddProduct");
        }*/
        /* [HttpPost]
         public ViewResult AddPucitBreakFast(PucitCafeProduct food)
         {
             string uniqueFileName = null;
             if (food.Photo != null)
             {
                 string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                 uniqueFileName = Guid.NewGuid().ToString() + "_" + food.Photo.FileName;
                 string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                 food.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
             }
             pucitRepository.addPucitBreakFast(food.Name, food.Price, food.Quantity, uniqueFileName);
             return View("AdminHomePage");
         }*/
        /* [HttpGet]
         public ViewResult AddPucitLunch()
         {
             PUCITCafeViewModel pUCITCafeViewModel = new PUCITCafeViewModel();
             pUCITCafeViewModel.controller_Name = "/Admin/AddPucitLunch";
             pUCITCafeViewModel.title = "Add a Lunch Product";
             pUCITCafeViewModel.addProduct = null;
             return View("AddProduct", pUCITCafeViewModel);
         }

         [HttpPost]
         public ViewResult AddPucitLunch(PucitCafeProduct food)
         {
             string uniqueFileName = null;
             if (food.Photo != null)
             {
                 string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                 uniqueFileName = Guid.NewGuid().ToString() + "_" + food.Photo.FileName;
                 string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                 food.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
             }
             pucitRepository.addPucitLunch(food.Name, food.Price, food.Quantity, uniqueFileName);
             return View("AdminHomePage");
         }*/
        public ViewResult OrderedList()
        {
            PUCITShowViewModel pucitShowViewModel = new PUCITShowViewModel();
            pucitShowViewModel.productlist = pucitRepository.temp;
            pucitShowViewModel.Category = pucitRepository.Category;
            return View(pucitShowViewModel);
        }

    }
}