using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using finalYearProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace finalYearProject.Controllers
{
    [Authorize(Roles = "RestaurantAdmin,AppAdministrator")]
    public class RestaurentAdminController : Controller
    {
        PUCITRepository pucitRepository = new PUCITRepository();
        private readonly IHostingEnvironment hostingEnvironment;
        public RestaurentAdminController(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }
       
        [HttpGet]
        public ViewResult AddRestaurent(string uname)
        {
            ViewBag.uname = uname;
            return View("AddRestaurent");
        }    
        [HttpPost]
        public IActionResult AddRestaurent(Restaurent r,string uname)
        {
            ViewBag.uname = uname;
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
      
        public ViewResult ViewRestaurent(string uname)
        {
            ViewBag.uname = uname;
            //return View(RestaurentRepository.restaurents);
            return View(pucitRepository.restaurents);
        }
        [HttpGet]
        public ViewResult AddMenu(string RestaurentID,string uname)
        {
            ViewBag.uname = uname; 
            ViewBag.resid = RestaurentID;
            RestaurentMenu rm = new RestaurentMenu();
            rm.RestaurentID = RestaurentID;
            return View("AddRestaurentMenue", rm);

        }
        [HttpPost]
        public IActionResult AddMenu(RestaurentMenu rm,string uname)
        {
            ViewBag.uname = uname;
            if (ModelState.IsValid)
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
            RestaurentMenu rm1 = new RestaurentMenu();
            rm1 = rm;
            return View("AddRestaurentMenue", rm);
        }
        public ViewResult ViewRestaurentMenue(string RestaurentID,string uname)
        {
            ViewBag.uname = uname;
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
       public ViewResult Details(string RestaurentID,string uname)
        {
            ViewBag.uname = uname;
            Restaurent r = pucitRepository.restaurents.Find(r => r.RestaurentID == RestaurentID);
            return View(r);
        }
        public ViewResult Delete(string RestaurentID,string uname)
        {
            ViewBag.uname = uname;
            Restaurent r = pucitRepository.restaurents.Find(r => r.RestaurentID == RestaurentID);
            pucitRepository.restaurents.Remove(r);
            pucitRepository.RemoveRestaurent(RestaurentID);
            
            /* RestaurentMenu rm = RestaurentRepository.restaurentsMenu.Find(r => r.RestaurentID == RestaurentID);

             if (rm != null)
             {
                 RestaurentRepository.restaurentsMenu.Remove(rm);
                 //RestaurentRepository.RemoveMenu(RestaurentID, rm.MenuID);
             }*/

            return View("ViewRestaurent", pucitRepository.restaurents);

        }
        [HttpGet]
        public ViewResult Edit(string RestaurentID,string uname)
        {
            ViewBag.uname = uname;
            Restaurent r = pucitRepository.restaurents.Find(r => r.RestaurentID == RestaurentID);
            EditRes res = new EditRes();
            res.RestaurentID = r.RestaurentID;
            res.NameOfRestaurants = r.NameOfRestaurants;
            res.Location = r.Location;
            res.DeliveryCharges = r.DeliveryCharges;
            res.OpenUntil = r.OpenUntil;
            res.PhoneNo = r.PhoneNo;
            res.PhotoPATH = r.PhotoPATH;
            return View("Edit", res);
        }
        public ViewResult Edit(EditRes r,string uname)
        {
            ViewBag.uname = uname;
            string uniqueFileName = null;
            if (ModelState.IsValid)
            {
                if (r.Photo != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + r.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    r.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                r.PhotoPATH = uniqueFileName;
                pucitRepository.EditRestaurent(r);

                foreach (Restaurent restaurent in pucitRepository.restaurents)
                {


                    if (restaurent.RestaurentID == r.RestaurentID)
                    {
                        restaurent.NameOfRestaurants = r.NameOfRestaurants;
                        restaurent.Location = r.Location;
                        restaurent.PhoneNo = r.PhoneNo;
                        restaurent.OpenUntil = r.OpenUntil;
                        restaurent.DeliveryCharges = r.DeliveryCharges;
                        if (uniqueFileName != null)
                            restaurent.PhotoPATH = r.PhotoPATH;
                    }
                }
                return View("ViewRestaurent", pucitRepository.restaurents);
            }
            return View(r);
        }
        public ViewResult DeleteItem(string RestaurentID,string menuid,string uname)
        {
            ViewBag.uname = uname;
            RestaurentMenu restaurentMenu = new RestaurentMenu();
            foreach (RestaurentMenu restaurent in pucitRepository.restaurentsMenu)
            {
                if (restaurent.RestaurentID == RestaurentID && restaurent.MenuID==int.Parse(menuid))
                {
                    pucitRepository.restaurentsMenu.Remove(restaurent);
                    break;
                }
            }
            //pucitRepository.restaurentsMenu.Remove(r);
            pucitRepository.RemoveMenu(RestaurentID,int.Parse(menuid));
            Restaurent r = pucitRepository.restaurents.Find(r => r.RestaurentID == RestaurentID);
            foreach (RestaurentMenu res in pucitRepository.restaurentsMenu)
            {
                if (res.RestaurentID == RestaurentID)
                {
                    res.RestaurentName = r.NameOfRestaurants;
                    pucitRepository.tempres.Add(res);
                }
            }
            return View("ViewRestaurentMenue", pucitRepository.tempres);

        }
        [HttpGet]
        public ViewResult EditItem(string RestaurentID, int menuid,string uname)
        {
            ViewBag.uname = uname;
            RestaurentMenu r =pucitRepository.restaurentsMenu.Find(r => r.RestaurentID == RestaurentID && r.MenuID == menuid);
            return View("EditMenue", r);
        }
        public ViewResult EditItem(RestaurentMenu rm,string RestaurentID,string uname)
        {
            ViewBag.uname = uname;
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (rm.Photo2 != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + rm.Photo2.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    rm.Photo2.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                rm.PhotoPATH2 = uniqueFileName;
                pucitRepository.EditMenu(rm);
                foreach (RestaurentMenu restaurentmenu in pucitRepository.restaurentsMenu)
                {
                    if (restaurentmenu.RestaurentID == rm.RestaurentID && restaurentmenu.MenuID == rm.MenuID)
                    {

                        restaurentmenu.NameOfItem = rm.NameOfItem;

                        restaurentmenu.Price = rm.Price;
                        restaurentmenu.Quantity = rm.Quantity;
                        restaurentmenu.unit = rm.unit;
                        if (rm.PhotoPATH2 != null)
                            restaurentmenu.PhotoPATH2 = rm.PhotoPATH2;
                    }
                    Restaurent r = pucitRepository.restaurents.Find(r => r.RestaurentID == RestaurentID);
                    foreach (RestaurentMenu res in pucitRepository.restaurentsMenu)
                    {
                        if (res.RestaurentID == RestaurentID)
                        {
                            res.RestaurentName = r.NameOfRestaurants;
                            pucitRepository.tempres.Add(res);
                        }
                    }
                    return View("ViewRestaurentMenue", pucitRepository.tempres);

                }
            }
           return View(rm);
             
        }
    }
}