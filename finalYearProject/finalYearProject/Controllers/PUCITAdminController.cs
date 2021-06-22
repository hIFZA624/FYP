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
        public ViewResult AddCategory(string uname)
        {
            ViewBag.uname = uname;

            PUCITCafeCategoryViewModel pUCITCafeCategory = new PUCITCafeCategoryViewModel();
            pUCITCafeCategory.Category = pucitRepository.Category;
            return View(pUCITCafeCategory);
        }
        [HttpPost]
        public ViewResult AddCategory(PUCITCafeCategory category,string uname)
        {
            ViewBag.uname = uname;
            if (ModelState.IsValid)
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
            PUCITCafeCategoryViewModel pUCITCategoryView1 = new PUCITCafeCategoryViewModel();
            pUCITCategoryView1.Category = pucitRepository.Category;
            pUCITCategoryView1.addCategory = category;
            pUCITCategoryView1.title = "Pucit Product Categories";
            return View(pUCITCategoryView1);
        }

        [HttpGet]
        public ViewResult AddProduct(string uname)
        {
            ViewBag.uname = uname;
            PUCITProducts pUCITProducts = new PUCITProducts();
                PUCITCafeViewModel pUCITCafeViewModel = new PUCITCafeViewModel();
                pUCITCafeViewModel.Category = pucitRepository.Category;
                // pUCITCafeViewModel.title = "";
                pUCITCafeViewModel.addProduct =pUCITProducts;
                return View("AddProduct", pUCITCafeViewModel);
           

        }
        [HttpPost]
        public ViewResult AddProduct(PUCITProducts food,string uname)
        {
            ViewBag.uname = uname;
            if (ModelState.IsValid)
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
                pucitRepository.temp.Clear(); 
                foreach (PUCITProducts product in pucitRepository.products)
                {
                    if (product.Category == food.Category)
                    {
                        pucitRepository.temp.Add(product);
                    }
                }
                PUCITShowViewModel pucitShowViewModel = new PUCITShowViewModel();
                pucitShowViewModel.productlist = pucitRepository.temp;
                pucitShowViewModel.Category = pucitRepository.Category;
                return View("ProductsView", pucitShowViewModel);
            }
            PUCITCafeViewModel pUCITCafeViewModel = new PUCITCafeViewModel();
            pUCITCafeViewModel.Category = pucitRepository.Category;
            // pUCITCafeViewModel.title = "";
            pUCITCafeViewModel.addProduct = food;
            return View("AddProduct", pUCITCafeViewModel);

        }
        public ViewResult ProductView(String CategoryName,string uname)
        {
            ViewBag.uname = uname;
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
        public ViewResult CategoryView(string uname)
        {
            ViewBag.uname = uname;
            PUCITCategoryViewcs pUCITCategoryView = new PUCITCategoryViewcs();
            pUCITCategoryView.Category = pucitRepository.Category;
            pUCITCategoryView.title = "Pucit Product Categories";
            return View("CategoryView", pUCITCategoryView);
        }
        public ViewResult PucitCategoryDelete(int id,string uname)
        {
            ViewBag.uname = uname;
            pucitRepository.PUCITCategorydelete(id);
            PUCITCategoryViewcs pUCITCategoryView = new PUCITCategoryViewcs();
            pUCITCategoryView.Category = pucitRepository.Category;
            pUCITCategoryView.title = "Pucit Product Categories";
            return View("CategoryView", pUCITCategoryView);
        }
        public ViewResult Detalis(int id,string uname)
        {
            ViewBag.uname = uname;
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
        public ViewResult OrderedList(string uname)
        {
            ViewBag.uname = uname;
            PUCITShowViewModel pucitShowViewModel = new PUCITShowViewModel();
            pucitShowViewModel.productlist = pucitRepository.temp;
            pucitShowViewModel.Category = pucitRepository.Category;
            return View(pucitShowViewModel);
        }
        public ViewResult Detail(string id,string uname)
        {
            ViewBag.uname = uname;
            PUCITCafeCategoryViewModel pUCITCafeCategoryViewModel = new PUCITCafeCategoryViewModel();
         
           PUCITCafeCategory r = pucitRepository.Category.Find(r => r.Id == int.Parse(id));
            pUCITCafeCategoryViewModel.addCategory = r;
            pUCITCafeCategoryViewModel.Category = pucitRepository.Category;
            return View(pUCITCafeCategoryViewModel);
        }
        [HttpGet]
        public ViewResult EditCategory(string id,string category,string uname)
        {
            ViewBag.uname = uname;
            ViewBag.category = category;
            PUCITCafeCategoryViewModel pUCITCategoryView = new PUCITCafeCategoryViewModel();
            PUCITCafeCategory r = pucitRepository.Category.Find(r => r.Id == int.Parse(id));
            pUCITCategoryView.Category = pucitRepository.Category;
            pUCITCategoryView.addCategory = r;
            pUCITCategoryView.title = "Pucit Product Categories";
            return View("EditCategory", pUCITCategoryView);
        }
        [HttpPost]
        public ViewResult EditCategory(PUCITCafeCategory modal,string category,string uname)
        {
            ViewBag.uname = uname;
            ViewBag.category =category;
            string uniqueFileName = null;
            if (ModelState.IsValid)
            {
               
                if (modal.Photo != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + modal.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    modal.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                modal.PhotoPath = uniqueFileName;
                pucitRepository.editCategory(modal,category);
               
                PUCITCategoryViewcs pUCITCategoryView = new PUCITCategoryViewcs();
                pUCITCategoryView.Category = pucitRepository.Category;
                pUCITCategoryView.title = "Pucit Product Categories";
                return View("CategoryView", pUCITCategoryView);

            }
            PUCITCafeCategoryViewModel pUCITCategoryView1 = new PUCITCafeCategoryViewModel();
            pUCITCategoryView1.Category = pucitRepository.Category;
            pUCITCategoryView1.addCategory = modal;
            pUCITCategoryView1.title = "Pucit Product Categories";
            return View("EditCategory", pUCITCategoryView1);
        }
        public ViewResult DetailProduct(string id,string uname)
        {
            ViewBag.uname = uname;
            PUCITCafeViewModel pUCITCafeCategoryViewModel = new PUCITCafeViewModel();

           PUCITProducts r = pucitRepository.products.Find(r => r.Id == int.Parse(id));
            pUCITCafeCategoryViewModel.addProduct = r;
            pUCITCafeCategoryViewModel.Category = pucitRepository.Category;
            return View(pUCITCafeCategoryViewModel);
        }
        public ViewResult Remove(string id,string category,string uname)
        {
            ViewBag.uname = uname;
            PUCITProducts r = pucitRepository.products.Find(r => r.Id == int.Parse(id));
            pucitRepository.products.Remove(r);
            pucitRepository.RemoveProductProduct(r);
            //Return
            pucitRepository.temp.Clear();
            foreach (PUCITProducts product in pucitRepository.products)
            {
                if (product.Category == category)
                {
                    pucitRepository.temp.Add(product);
                }
            }
            PUCITShowViewModel pucitShowViewModel = new PUCITShowViewModel();
            pucitShowViewModel.productlist = pucitRepository.temp;
            pucitShowViewModel.Category = pucitRepository.Category;
            return View("ProductsView", pucitShowViewModel);    
        }
        [HttpGet]
        public ViewResult EditProduct(string id,string uname)
        {
            ViewBag.uname = uname;
            PUCITCafeViewModel pUCITCategoryView = new PUCITCafeViewModel();
            PUCITProducts r = pucitRepository.products.Find(r => r.Id == int.Parse(id));
            pUCITCategoryView.Category = pucitRepository.Category;
            pUCITCategoryView.addProduct = r;
            pUCITCategoryView.title = "Pucit Product Categories";
            return View("EditProduct", pUCITCategoryView);
        }
        [HttpPost]
        public ViewResult EditProduct(PUCITProducts modal,string category,string uname)
        {
              ViewBag.uname = uname;
            string uniqueFileName = null;
            if (ModelState.IsValid)
            {
                if (modal.Photo != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + modal.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    modal.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                modal.PhotoPath = uniqueFileName;
                pucitRepository.UpdateProduct(modal);
                pucitRepository.temp.Clear();
                foreach (PUCITProducts product in pucitRepository.products)
                {
                    if (product.Category == category)
                    {
                        pucitRepository.temp.Add(product);
                    }
                }
                PUCITShowViewModel pucitShowViewModel = new PUCITShowViewModel();
                pucitShowViewModel.productlist = pucitRepository.temp;
                pucitShowViewModel.Category = pucitRepository.Category;
                return View("ProductsView", pucitShowViewModel);

            }
            PUCITCafeViewModel pUCITCategoryView = new PUCITCafeViewModel();
            pUCITCategoryView.Category = pucitRepository.Category;
            pUCITCategoryView.addProduct = modal;
            pUCITCategoryView.title = "Pucit Product Categories";
            return View("EditProduct", pUCITCategoryView);
        }
    }
}