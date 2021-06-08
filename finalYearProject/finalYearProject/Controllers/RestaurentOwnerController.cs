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
    public class RestaurentOwnerController : Controller
    {

        PUCITRepository pucitRepository = new PUCITRepository();
        private readonly IHostingEnvironment hostingEnvironment;
        public RestaurentOwnerController(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        public ViewResult OwnerLogin()

        {
            return View("OwnerLogin");

        }
        public ViewResult ownerHomePage(string RestaurentID)
        {
            ViewBag.resId = RestaurentID;
            Restaurent res = pucitRepository.restaurents.Find(r => r.RestaurentID == RestaurentID);
            return View(res);
        }
        [HttpPost]
        public ViewResult OwnerLogin(Login r)
        {
            bool flag = false;
            string ResId="";
            if (ModelState.IsValid)
            {
                ViewBag.resId=r.username;
                foreach (Restaurent r1 in pucitRepository.restaurents)
                {
                    if ((r1.RestaurentID == r.username) && (r1.Password == r.password))
                    {
                        flag = true;
                        ResId = r1.RestaurentID;
                    }
                }
                if (flag == true)
                {
                    Restaurent res = pucitRepository.restaurents.Find(r => r.RestaurentID == ResId);
                    return View("ownerHomePage",res);
                }
                else
                    ModelState.AddModelError(string.Empty, "Invalid Login");
            }
            return View(r);
        }
     
        public ViewResult OwnerMenuList(string RestaurentID)
        {
            ViewBag.resId = RestaurentID;
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
        [HttpGet]
        public ViewResult AddMenu(string RestaurentID)
        {
            RestaurentMenu rm = new RestaurentMenu();
            ViewBag.resId = RestaurentID;
            rm.RestaurentID = RestaurentID;
            return View("AddMenu", rm);
        }
        [HttpPost]
        public IActionResult AddMenu(RestaurentMenu rm)
        {
            string uniqueFileName = null;
            ViewBag.resId = rm.RestaurentID;
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
            return View("OwnerMenuList", pucitRepository.tempres);
        }
    }
}