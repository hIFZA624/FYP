using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using finalYearProject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace finalYearProject.Controllers
{
    public class RestaurentAdminController : Controller
    {
        PUCITRepository pucitRepository = new PUCITRepository();
        private readonly IHostingEnvironment hostingEnvironment;
        public RestaurentAdminController(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }
       
        [HttpGet]
        public ViewResult AddRestaurent()
        {
            return View("AddRestaurent");
        }    
        [HttpPost]
        public IActionResult AddRestaurent(Restaurent r)
        {
            bool flag = false;
            if (ModelState.IsValid)
            {
                foreach(Restaurent res in pucitRepository.restaurents)
                {
                    if(res.RestaurentID==r.RestaurentID)
                    {
                        ModelState.AddModelError(string.Empty, "Restaurant Id Alredy exists");
                        flag = true;
                    }
                }
                if (flag == false)
                {
                    string uniqueFileName = null;

                    if (r.Photo != null)
                    {
                        string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + r.Photo.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        r.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                    }
                    r.PhotoPATH = uniqueFileName;
                    pucitRepository.AddRestaurentD(r);
                    return RedirectToAction("ViewRestaurent");
                }
               
            }
            return View(r);
        }
      
        public ViewResult ViewRestaurent()
        {
            //return View(RestaurentRepository.restaurents);
            return View(pucitRepository.restaurents);
        }
        [HttpGet]
        public ViewResult AddMenu(string RestaurentID)
        {
            ViewBag.resid = RestaurentID;
            RestaurentMenu rm = new RestaurentMenu();
            rm.RestaurentID = RestaurentID;
            return View("AddRestaurentMenue", rm);

        }
        [HttpPost]
        public IActionResult AddMenu(RestaurentMenu rm)
        {
            string uniqueFileName = null;
            ViewBag.resid = rm.RestaurentID;
            if (rm.Photo2 != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + rm.Photo2.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                rm.Photo2.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            rm.PhotoPATH2 = uniqueFileName;
            pucitRepository.AddMenuD(rm);
            Restaurent r = pucitRepository.restaurents.Find(r => r.RestaurentID == rm.RestaurentID);
            foreach (RestaurentMenu res in pucitRepository.restaurentsMenu)
            {
                if (res.RestaurentID == rm.RestaurentID)
                {
                    res.RestaurentName = r.NameOfRestaurants;
                    pucitRepository.tempres.Add(res);
                }
            }
            return View("ViewRestaurentMenue", pucitRepository.tempres);
        }
        public ViewResult ViewRestaurentMenue(string RestaurentID)
        {
            Restaurent r = pucitRepository.restaurents.Find(r => r.RestaurentID == RestaurentID);
            foreach (RestaurentMenu res in pucitRepository.restaurentsMenu)
            {
                if (res.RestaurentID == RestaurentID)
                {
                    res.RestaurentName = r.NameOfRestaurants;
                    pucitRepository.tempres.Add(res);
                }
            }
            return View(pucitRepository.tempres);
        }
       public ViewResult Details(string RestaurentID)
        {

            Restaurent r = pucitRepository.restaurents.Find(r => r.RestaurentID == RestaurentID);
            return View(r);
        }
    }
}