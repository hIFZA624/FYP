using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using finalYearProject.Models;
using finalYearProject.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace finalYearProject.Controllers
{
    [Authorize(Roles = "PharmacyCafeAdmin,AppAdministrator")]
    public class PharmacyCafeController : Controller
    {
        PUCITRepository pucitRepository = new PUCITRepository();
        private readonly IHostingEnvironment hostingEnvironment;
        public PharmacyCafeController(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        public ViewResult AddCategory(string uname)
        {
            ViewBag.uname = uname;
            PUCITCafeCategoryViewModel pUCITCafeCategory = new PUCITCafeCategoryViewModel();
                pUCITCafeCategory.Category = pucitRepository.CategoryPharmacy;
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
                pucitRepository.addPharmacyProductCategory(uniqueFileName, category.CategoryName, category.Description);
                /*PUCITCategoryViewcs pUCITCategoryView = new PUCITCategoryViewcs();
                pUCITCategoryView.Category = pucitRepository.CategoryPharmacy;
                pUCITCategoryView.title = "Pharmacy Product Categories";*/
                PUCITCategoryViewcs pUCITCategoryView = new PUCITCategoryViewcs();
                pUCITCategoryView.Category = pucitRepository.CategoryPharmacy;
                pUCITCategoryView.title = "Pharmacy Product Categories";
                return View("CategoryView", pUCITCategoryView);
            }
            PUCITCafeCategoryViewModel pUCITCafeCategory = new PUCITCafeCategoryViewModel();
            pUCITCafeCategory.Category = pucitRepository.CategoryPharmacy;
            pUCITCafeCategory.addCategory = category;
            return View(pUCITCafeCategory);
        }
        public ViewResult Return()
        {
            return View();
        }
        public ViewResult CategoryView(string uname)
        {
            ViewBag.uname = uname;
            PUCITCategoryViewcs pUCITCategoryView = new PUCITCategoryViewcs();
            pUCITCategoryView.Category = pucitRepository.CategoryPharmacy;
            pUCITCategoryView.title = "Pharmacy Product Categories";
            return View("CategoryView", pUCITCategoryView);
        }

        [HttpGet]
        public ViewResult AddProduct(string uname)
        {
            ViewBag.uname = uname;
            PUCITCafeViewModel pUCITCafeViewModel = new PUCITCafeViewModel();
                pUCITCafeViewModel.Category = pucitRepository.CategoryPharmacy;
                pUCITCafeViewModel.addProduct = null;
                return View("AddProduct", pUCITCafeViewModel);
            
        }
        [HttpPost]
        public IActionResult AddProduct(PUCITProducts food,string uname)
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
                pucitRepository.addPharmacyProduct(food.Category,food.Name,food.Price,food.Quantity,uniqueFileName);
                pucitRepository.temp.Clear();
                foreach (PUCITProducts product in pucitRepository.productsPharmacy)
                {
                    if (product.Category == food.Category)
                    {
                        pucitRepository.temp.Add(product);
                    }
                }
                PUCITShowViewModel pucitShowViewModel = new PUCITShowViewModel();
                pucitShowViewModel.productlist = pucitRepository.temp;
                pucitShowViewModel.Category = pucitRepository.CategoryPharmacy;
                return View("ProductsView", pucitShowViewModel);
            }
            PUCITCafeViewModel pUCITCafeViewModel = new PUCITCafeViewModel();
            pUCITCafeViewModel.Category = pucitRepository.CategoryPharmacy;
            pUCITCafeViewModel.addProduct = food;
            return View("AddProduct", pUCITCafeViewModel);
        }
        public ViewResult ProductView(String CategoryName,string uname)
        {
            ViewBag.uname = uname;
            pucitRepository.temp.Clear();
            foreach (PUCITProducts product in pucitRepository.productsPharmacy)
            {
                if (product.Category == CategoryName)
                {
                    pucitRepository.temp.Add(product);
                }
            }
            PUCITShowViewModel pucitShowViewModel = new PUCITShowViewModel();
            pucitShowViewModel.productlist = pucitRepository.temp;
            pucitShowViewModel.Category = pucitRepository.CategoryPharmacy;
            return View("ProductsView", pucitShowViewModel);
        }
        public ViewResult OrderedList(string uname)
        {
            ViewBag.uname = uname;
            PUCITShowViewModel pucitShowViewModel = new PUCITShowViewModel();
            pucitShowViewModel.productlist = pucitRepository.temp;
            pucitShowViewModel.Category = pucitRepository.CategoryPharmacy;
            return View(pucitShowViewModel);
        }
        public ViewResult Detail(string id,string uname)
        {
            ViewBag.uname = uname;
            PUCITCafeCategoryViewModel pUCITCafeCategoryViewModel = new PUCITCafeCategoryViewModel();

            PUCITCafeCategory r = pucitRepository.CategoryPharmacy.Find(r => r.Id == int.Parse(id));
            pUCITCafeCategoryViewModel.addCategory = r;
            pUCITCafeCategoryViewModel.Category = pucitRepository.CategoryPharmacy;
            return View(pUCITCafeCategoryViewModel);
        }
      public ViewResult  PharmacyCategoryDelete(int id,string uname)
        {
            ViewBag.uname = uname;
            pucitRepository.PharmacyCategorydelete(id);
            PUCITCategoryViewcs pUCITCategoryView = new PUCITCategoryViewcs();
            pUCITCategoryView.Category = pucitRepository.CategoryPharmacy;
            pUCITCategoryView.title = "Pharmacy Product Categories";
            return View("CategoryView", pUCITCategoryView);
           
        }
        [HttpGet]
        public ViewResult EditCategory(string id, string category,string uname)
        {
            ViewBag.uname = uname;
            ViewBag.category = category;
            PUCITCafeCategoryViewModel pUCITCategoryView = new PUCITCafeCategoryViewModel();
            PUCITCafeCategory r = pucitRepository.CategoryPharmacy.Find(r => r.Id == int.Parse(id));
            pUCITCategoryView.Category = pucitRepository.CategoryPharmacy;
            pUCITCategoryView.addCategory = r;
            pUCITCategoryView.title = "Pharmacy Product Categories";
            return View("EditCategory", pUCITCategoryView);
        }
        [HttpPost]
        public ViewResult EditCategory(PUCITCafeCategory modal, string category,string uname)
        {
            ViewBag.uname = uname;
            ViewBag.category = category;
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
               pucitRepository.editCategoryPharmacy(modal, category);

                PUCITCategoryViewcs pUCITCategoryView = new PUCITCategoryViewcs();
                pUCITCategoryView.Category = pucitRepository.CategoryPharmacy;
                pUCITCategoryView.title = "Pharmacy Product Categories";
                return View("CategoryView", pUCITCategoryView);

            }
            PUCITCafeCategoryViewModel pUCITCategoryView1 = new PUCITCafeCategoryViewModel();
            pUCITCategoryView1.Category = pucitRepository.CategoryPharmacy;
            pUCITCategoryView1.addCategory = modal;
            pUCITCategoryView1.title = "Pharmacy Product Categories";
            return View("EditCategory", pUCITCategoryView1);
        }
        public ViewResult DetailProduct(string id,string uname)
        {
            ViewBag.uname = uname;
            PUCITCafeViewModel pUCITCafeCategoryViewModel = new PUCITCafeViewModel();
            PUCITProducts r = pucitRepository.productsPharmacy.Find(r => r.Id == int.Parse(id));
            pUCITCafeCategoryViewModel.addProduct = r;
            pUCITCafeCategoryViewModel.Category = pucitRepository.CategoryPharmacy;
            return View(pUCITCafeCategoryViewModel);
        }
        public ViewResult Remove(string id, string category,string uname)
        {
            ViewBag.uname = uname;
            PUCITProducts r = pucitRepository.productsPharmacy.Find(r => r.Id == int.Parse(id));
            pucitRepository.productsPharmacy.Remove(r);
            pucitRepository.RemoveProductPharmacy(r);
            //Return
            pucitRepository.temp.Clear();
            foreach (PUCITProducts product in pucitRepository.productsPharmacy)
            {
                if (product.Category == category)
                {
                    pucitRepository.temp.Add(product);
                }
            }
            PUCITShowViewModel pucitShowViewModel = new PUCITShowViewModel();
            pucitShowViewModel.productlist = pucitRepository.temp;
            pucitShowViewModel.Category = pucitRepository.CategoryPharmacy;
            return View("ProductsView", pucitShowViewModel);
        }
        [HttpGet]
        public ViewResult EditProduct(string id,string uname)
        {
            ViewBag.uname = uname;
            PUCITCafeViewModel pUCITCategoryView = new PUCITCafeViewModel();
            PUCITProducts r = pucitRepository.productsPharmacy.Find(r => r.Id == int.Parse(id));
            pUCITCategoryView.Category = pucitRepository.CategoryPharmacy;
            pUCITCategoryView.addProduct = r;
            pUCITCategoryView.title = "Pharmacy Product Categories";
            return View("EditProduct", pUCITCategoryView);
        }
        [HttpPost]
        public ViewResult EditProduct(PUCITProducts modal, string category,string uname)
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
               pucitRepository.updateProductPharmacy(modal,category);
                pucitRepository.temp.Clear();
                foreach (PUCITProducts product in pucitRepository.productsPharmacy)
                {
                    if (product.Category == category)
                    {
                        pucitRepository.temp.Add(product);
                    }
                }
                PUCITShowViewModel pucitShowViewModel = new PUCITShowViewModel();
                pucitShowViewModel.productlist = pucitRepository.temp;
                pucitShowViewModel.Category = pucitRepository.CategoryPharmacy;
                return View("ProductsView", pucitShowViewModel);

            }
            PUCITCafeViewModel pUCITCategoryView = new PUCITCafeViewModel();
            pUCITCategoryView.Category = pucitRepository.CategoryPharmacy;
            pUCITCategoryView.addProduct = modal;
            pUCITCategoryView.title = "Pucit Product Categories";
            return View("EditProduct", pUCITCategoryView);
        }
    }
}