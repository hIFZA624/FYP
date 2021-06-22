using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using finalYearProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace finalYearProject.Controllers
{
    public class RestaurentController : Controller
    {
        PUCITRepository pucitRepository = new PUCITRepository();
        public ViewResult RestaurentHomePage(string uname, string RestaurentID,string clear)
        {
            ViewBag.clear = clear;
            ViewBag.uname = uname;
            ViewBag.RestaurentID = RestaurentID;
            return View("RestaurentHomePage",pucitRepository.restaurents);
        }
        public ViewResult RestaurentMenue(string RestaurentID,string uname,string clear)
        {
            if(clear== "clearcart")
            {
                pucitRepository.Cart.Clear();
                pucitRepository.ClearAllCart();
            }
            ViewBag.uname = uname;
            ViewBag.RestaurentID = RestaurentID;
            Restaurent r = pucitRepository.restaurents.Find(r => r.RestaurentID == RestaurentID);
            foreach (RestaurentMenu res in pucitRepository.restaurentsMenu)
            {
                if (res.RestaurentID == RestaurentID)
                {
                    res.RestaurentName = r.NameOfRestaurants;
                    pucitRepository.tempres.Add(res);
                }
            }
            return View("RestaurentMenue", pucitRepository.tempres);
            //RestaurentMenu rm = RestaurentRepository.restaurentsMenu.Find(r => r.RestaurentID == RestaurentID);

        }
     
        public ViewResult MenueDetail(string RestaurentID, int MenuID,string uname)
        {
            ViewBag.RestaurentID = RestaurentID;
            ViewBag.uname = uname;
            RestaurentMenu rm = pucitRepository.restaurentsMenu.Find(rm => rm.RestaurentID == RestaurentID && rm.MenuID == MenuID);
            return View("MenueDetail", rm);
        }
        [HttpPost]
        public ViewResult AddToCart(RestaurentMenu r,string uname)
        {    
            ViewBag.RestaurentID = r.RestaurentID;
            ViewBag.uname = uname;
            if (ModelState.IsValid)
            {
                if(r.SelectedQuantity<=0)
                {
                    ModelState.AddModelError(string.Empty, "Quantity cannot be less tha 0");
                    return View("MenueDetail", r);
                }    
                RestaurentMenu rm = pucitRepository.restaurentsMenu.Find(rm => rm.RestaurentID == r.RestaurentID && rm.MenuID == r.MenuID);
                CartAndOrder a = new CartAndOrder();
                // a.CartID = pucitRepository.Cart.Count + 1;
                a.RestaurentID = rm.RestaurentID;
                a.MenuID = rm.MenuID;
                a.UserID = uname;
                //a.Quantity = Convert.ToInt32(Request["TextQuintity"].ToString());
                a.Quantity = r.SelectedQuantity;
                a.OriginalPrice = rm.Price;
                a.Price = (Int32.Parse(rm.Price) * a.Quantity).ToString();
                a.NameOfItem = rm.NameOfItem;
                pucitRepository.AddCartD(a);
                // a.UserID = RestaurentRepository.GetUserId();
                //  RestaurentRepository.AddToCart(a);
                //RestaurentRepository.AddCartD(a);
                //return View("RestaurentCartView", RestaurentRepository.Cart);
                return RestaurentCartView(r.RestaurentID, uname);
            }
            return View("MenueDetail", r);
        }
        public ViewResult RestaurentCartView(string RestaurentID, string uname)
        {
            ViewBag.RestaurentID = RestaurentID;
            ViewBag.uname = uname;
            foreach (CartAndOrder res in pucitRepository.Cart)
            {
                if (res.RestaurentID == RestaurentID && res.UserID ==uname)
                {
                    pucitRepository.CarttEMP.Add(res);
                }
            }
            viewmodel viewModel = new viewmodel();
            viewModel.Cart = pucitRepository.CarttEMP;
            Receipt re = new Receipt();
            viewModel.receipt = re;
            return View("RestaurentCartView", viewModel);
            
        }
        [HttpPost]
        public ViewResult PlaceOrder(viewmodel viewModel,string uname, string RestaurentID)
        {
            ViewBag.RestaurentID = RestaurentID;
            ViewBag.uname = uname;
            if (ModelState.IsValid)
            {
               
                Receipt receipt = new Receipt();
                receipt.ReceiptID = pucitRepository.restaurantPendingOrder.Count + 1;
                receipt.OrderTime = DateTime.Now;

                receipt.NoOfItems = pucitRepository.Cart.Count;
                receipt.RestaurentID = RestaurentID;
                receipt.OrderAccept = 0;
                receipt.OrderReceived = 0;
                receipt.TotalAmount = 0.ToString();
                receipt.UserTime = viewModel.receipt.UserTime;
                foreach (CartAndOrder cart in pucitRepository.Cart)
                {
                    receipt.UserID = cart.UserID;
                    cart.ReceiptID = receipt.ReceiptID;
                    receipt.TotalAmount = (Int32.Parse(receipt.TotalAmount) + Int32.Parse(cart.Price)).ToString();
                    pucitRepository.AddOrderD(cart);
                    pucitRepository.order.Add(cart);
                }
                pucitRepository.restaurantPendingOrder.Add(receipt);
                pucitRepository.AddReceiptD(receipt);
                pucitRepository.Cart.Clear();

                // pucitRepository.Cart.Clear();
                pucitRepository.ClearAllCart();
                //pending o
                return PendingOrder(uname, RestaurentID);
            }

            foreach (CartAndOrder res in pucitRepository.Cart)
            {
                if (res.RestaurentID == RestaurentID && res.UserID == uname)
                {
                    pucitRepository.CarttEMP.Add(res);
                }
            }
            viewmodel viewModel1 = new viewmodel();
            viewModel1.Cart = pucitRepository.CarttEMP;
            Receipt re = new Receipt();
            viewModel1.receipt = viewModel.receipt;
            return View("RestaurentCartView", viewModel1);
        }
        public ViewResult PendingOrder(string uname, string RestaurentID)
        {
            ViewBag.RestaurentID = RestaurentID;
            ViewBag.uname = uname;
            foreach (Receipt res in pucitRepository.restaurantPendingOrder)
            {
                if (res.RestaurentID == RestaurentID && res.UserID == uname && res.OrderAccept==0)
                {
                    pucitRepository.restaurantPendingOrderres.Add(res);
                }
            }
            return View("PendingOrder", pucitRepository.restaurantPendingOrderres);
        }
        public ViewResult HomePage(string uname, string RestaurentID)
        {
            ViewBag.RestaurentID = RestaurentID;
            ViewBag.uname = uname;
            return View("HomePage", pucitRepository.restaurents);
        }
        public ViewResult Detail(string uname, string RestaurentID,int receiptID)
        {
            ViewBag.RestaurentID = RestaurentID;
            ViewBag.uname = uname;
            ViewBag.ReceiptID = receiptID;
            return View("Detail", pucitRepository.order);
        }
        public ViewResult UserOrderList(string uname, string RestaurentID)
        {
            ViewBag.RestaurentID = RestaurentID;
            ViewBag.uname = uname;
            foreach (Receipt receipt in pucitRepository.restaurantPendingOrder)
            {
                if (receipt.RestaurentID == RestaurentID && receipt.UserID==uname && receipt.OrderAccept == 1)
                {
                    pucitRepository.restaurantOrderListres.Add(receipt);
                }
            }
            return View("UserOrderList", pucitRepository.restaurantOrderListres);
        }
        public ViewResult Clear(string uname, string RestaurentID)
        {
            ViewBag.RestaurentID = RestaurentID;
            ViewBag.uname = uname;
            pucitRepository.Cart.Clear();
           pucitRepository.ClearAllCart();
            viewmodel viewModel = new viewmodel();
            viewModel.Cart = pucitRepository.Cart;
            Receipt re = new Receipt();
            viewModel.receipt = re;
            return View("RestaurentCartView", viewModel);
            //return View("RestaurentCartView", RestaurentRepository.Cart);

        }
        public ViewResult Remove(int CartID, string uname, string RestaurentID)
        {
            ViewBag.RestaurentID = RestaurentID;
            ViewBag.uname = uname;
            CartAndOrder r = pucitRepository.Cart.Find(r => r.CartID == CartID);
            pucitRepository.Cart.Remove(r);
            pucitRepository.RemoveCart(r.CartID);
              return RestaurentCartView(RestaurentID,uname);
            // return View("RestaurentCartView", RestaurentRepository.Cart);


        }
        public ViewResult Increase(int CartID, string uname, string RestaurentID)
        {
            ViewBag.RestaurentID = RestaurentID;
            ViewBag.uname = uname;
            CartAndOrder r =pucitRepository.Cart.Find(r => r.CartID == CartID);


            foreach (CartAndOrder cart in pucitRepository.Cart)
            {

                if (cart.CartID == r.CartID)
                {

                    cart.Price = (Int32.Parse(r.OriginalPrice) * (r.Quantity + 1)).ToString();
                    cart.Quantity = r.Quantity + 1;
                    break;
                }

            }
           pucitRepository.EditCart(r);
            return RestaurentCartView(RestaurentID, uname);
        }
        public ViewResult Decrease(int CartID, string uname, string RestaurentID)
        {
            CartAndOrder r = pucitRepository.Cart.Find(r => r.CartID == CartID);
           viewmodel viewModel = new viewmodel();
            viewModel.Cart =pucitRepository.Cart;
            Receipt re = new Receipt();
            viewModel.receipt = re;
            if (r.Quantity == 1)
            {
                Remove(CartID,uname,RestaurentID);
            
                //return View("RestaurentCartView", RestaurentRepository.Cart);

            }

            foreach (CartAndOrder cart in pucitRepository.Cart)
            {

                if (cart.CartID == r.CartID)
                {

                    cart.Price = (Int32.Parse(r.OriginalPrice) * (r.Quantity - 1)).ToString();
                    cart.Quantity = r.Quantity - 1;
                    break;
                }

            }
           pucitRepository.EditCart(r);

            return RestaurentCartView(RestaurentID,uname);
            //return View("RestaurentCartView", RestaurentRepository.Cart);


        }
    }
}