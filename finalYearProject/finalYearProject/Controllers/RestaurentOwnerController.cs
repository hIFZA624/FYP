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
        public ViewResult OwnerLogin(string uname)

        {
            ViewBag.uname = uname;
            return View("OwnerLogin");

        }
        public ViewResult ownerHomePage(string RestaurentID,string uname)
        {
            ViewBag.uname = uname;
            ViewBag.resId = RestaurentID;
            Restaurent res = pucitRepository.restaurents.Find(r => r.RestaurentID == RestaurentID);
            return View(res);
        }
        [HttpPost]
        public ViewResult OwnerLogin(Login r,string uname)
        {
            ViewBag.uname = uname;
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
     
        public ViewResult OwnerMenuList(string RestaurentID,string uname)
        {
            ViewBag.uname = uname;
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
        public ViewResult AddMenu(string RestaurentID,string uname)
        {
            RestaurentMenu rm = new RestaurentMenu();
            ViewBag.resId = RestaurentID;
            rm.RestaurentID = RestaurentID;
            return View("AddMenu", rm);
        }
        [HttpPost]
        public IActionResult AddMenu(RestaurentMenu rm,string uname)
        {
            ViewBag.uname = uname;
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
        public ViewResult PendingOrderList(string RestaurentID,string uname)
        {
            ViewBag.uname = uname;
            ViewBag.resId = RestaurentID;
            foreach (Receipt res in pucitRepository.restaurantPendingOrder)
            {
                if (res.RestaurentID == RestaurentID && res.OrderAccept==0)
                {        
                    pucitRepository.restaurantPendingOrderres.Add(res);
                }
            }
            return View("PendingOrderList", pucitRepository.restaurantPendingOrderres);
        }
        public ViewResult Accept(int ReceiptID,string RestaurentID,string uname)
        {
            ViewBag.uname = uname;
            ViewBag.resId = RestaurentID;
            //ViewBag.resId = RestaurentID;
            //CartAndOrder r = RestaurentRepository.restaurantPendingOrder.Find(r => r.CartID == CartID);
            foreach (Receipt receipt in pucitRepository.restaurantPendingOrder)
            {

                if (receipt.ReceiptID == ReceiptID)
                {
                    receipt.OrderAccept = 1;
                    pucitRepository.EditReceipt(receipt);
                    break;
                }

            }
            foreach (Receipt receipt in pucitRepository.restaurantPendingOrder)
            {
                if (receipt.RestaurentID == RestaurentID && receipt.OrderAccept==1)
                {
                    pucitRepository.restaurantOrderListres.Add(receipt);
                    // pucitRepository.res
                }

            }
            return View("OrderList", pucitRepository.restaurantOrderListres);
        }
      
        public ViewResult OrderList(string RestaurentID,string uname)
        {
            ViewBag.uname = uname;
            ViewBag.resId = RestaurentID;
            foreach (Receipt receipt in pucitRepository.restaurantPendingOrder)
            {

                if (receipt.RestaurentID == RestaurentID && receipt.OrderAccept==1)
                {
                    pucitRepository.restaurantOrderListres.Add(receipt);
                
                }

            }
            
           return View("OrderList", pucitRepository.restaurantOrderListres);
        }
        public IActionResult Decline(int ReceiptID,string RestaurentID,string uname)
        {
            ViewBag.uname = uname;
            ViewBag.resId = RestaurentID;
            //CartAndOrder r = RestaurentRepository.restaurantPendingOrder.Find(r => r.CartID == CartID);
            foreach (CartAndOrder cart in pucitRepository.order)
            {

                if (cart.ReceiptID == ReceiptID)
                {
                   pucitRepository.order.Remove(cart);
                  pucitRepository.RemoveOrder(cart.ReceiptID);
                    break;
                }
            }
            foreach (Receipt receipt in pucitRepository.restaurantPendingOrder)
            {

                if (receipt.ReceiptID == ReceiptID)
                {
                    //receipt.OrderAccept = 1;
                    pucitRepository.RemoveReceipt(ReceiptID);

                    pucitRepository.restaurantPendingOrder.Remove(receipt);

                    break;
                }
            }
            foreach (Receipt receipt in pucitRepository.restaurantPendingOrder)
            {

                if (receipt.RestaurentID == RestaurentID && receipt.OrderAccept==0)
                {
                    pucitRepository.restaurantPendingOrderres.Add(receipt);
                    // pucitRepository.res
                }

            }
            return View("PendingOrderList", pucitRepository.restaurantPendingOrderres);
        }
      
        public ViewResult Detail( string RestaurentID, int receiptID, string uname)
        {
            ViewBag.uname = uname;
            ViewBag.resId = RestaurentID;
            ViewBag.ReceiptID = receiptID;
            return View("Detail", pucitRepository.order);
        }
        public ViewResult DeleteItem(string RestaurentID, string menuid, string uname)
        {
            ViewBag.uname = uname;
            ViewBag.resId = RestaurentID;
            RestaurentMenu restaurentMenu = new RestaurentMenu();
            foreach (RestaurentMenu restaurent in pucitRepository.restaurentsMenu)
            {
                if (restaurent.RestaurentID == RestaurentID && restaurent.MenuID == int.Parse(menuid))
                {
                    pucitRepository.restaurentsMenu.Remove(restaurent);
                    break;
                }
            }
            //pucitRepository.restaurentsMenu.Remove(r);
            pucitRepository.RemoveMenu(RestaurentID, int.Parse(menuid));
            Restaurent r = pucitRepository.restaurents.Find(r => r.RestaurentID == RestaurentID);
            foreach (RestaurentMenu res in pucitRepository.restaurentsMenu)
            {
                if (res.RestaurentID == RestaurentID)
                {
                    res.RestaurentName = r.NameOfRestaurants;
                    pucitRepository.tempres.Add(res);
                }
            }
            return View("OwnerMenuList", pucitRepository.tempres);

        }
        [HttpGet]
        public ViewResult EditItem(string RestaurentID, int MenuID,string uname)
        {
            ViewBag.uname = uname;
            ViewBag.resId = RestaurentID;
            RestaurentMenu r = pucitRepository.restaurentsMenu.Find(r => r.RestaurentID == RestaurentID && r.MenuID == MenuID);
          
            return View("Edit", r);
        }
        public ViewResult EditItem(RestaurentMenu rm, string RestaurentID, string uname)
        {
            ViewBag.resId = RestaurentID;
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
                    return View("OwnerMenuList", pucitRepository.tempres);

                
            }
            return View("Edit", rm);

        }
    }
}