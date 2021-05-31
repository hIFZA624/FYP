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
    [Authorize(Roles = "PharmacyCafeAdmin")]
    public class PharmacyCafeController : Controller
    {
        PUCITRepository pucitRepository = new PUCITRepository();
        private readonly IHostingEnvironment hostingEnvironment;
        public PharmacyCafeController(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        public ViewResult AddCategory()
        {
            PUCITCafeCategoryViewModel pUCITCafeCategory = new PUCITCafeCategoryViewModel();
            pUCITCafeCategory.Category = pucitRepository.CategoryPharmacy;
            return View(pUCITCafeCategory);
            // return View();
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
            pucitRepository.addPharmacyProductCategory(uniqueFileName, category.CategoryName, category.Description);
            /*PUCITCategoryViewcs pUCITCategoryView = new PUCITCategoryViewcs();
            pUCITCategoryView.Category = pucitRepository.CategoryPharmacy;
            pUCITCategoryView.title = "Pharmacy Product Categories";*/
            return View("Return");
        }
        public ViewResult Return()
        {
            return View();
        }
        public ViewResult CategoryView()
        {
            PUCITCategoryViewcs pUCITCategoryView = new PUCITCategoryViewcs();
            pUCITCategoryView.Category = pucitRepository.CategoryPharmacy;
            pUCITCategoryView.title = "Pharmacy Product Categories";
            return View("CategoryView", pUCITCategoryView);
        }

        [HttpGet]
        public ViewResult AddProduct()
        {
            PUCITCafeViewModel pUCITCafeViewModel = new PUCITCafeViewModel();
            pUCITCafeViewModel.Category = pucitRepository.CategoryPharmacy;
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
            pucitRepository.addPharmacyProduct(food.Category, food.Name, food.Price, food.Quantity, uniqueFileName);
            PUCITCategoryViewcs pUCITCategory = new PUCITCategoryViewcs();
            pUCITCategory.Category = pucitRepository.CategoryPharmacy;
            return View("Return");
        }
        public ViewResult ProductView(String CategoryName)
        {
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

    }
}