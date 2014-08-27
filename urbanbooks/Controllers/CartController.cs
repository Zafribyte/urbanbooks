﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using urbanbooks.Models;
using System.Web.ModelBinding;
using urbanbooks.Controllers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;

namespace urbanbooks.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        BusinessLogicHandler myHandler;
        public async Task<ActionResult> Edit()
        {
            CartActions act = new CartActions(); WishlistActions wishAct = new WishlistActions();
            ApplicationDbContext dataSocket = new ApplicationDbContext();
            UserStore<ApplicationUser> myStore = new UserStore<ApplicationUser>(dataSocket);
            userMgr = new ApplicationUserManager(myStore);
            var thisUser = await userMgr.FindByNameAsync(User.Identity.Name);
            int Id = (int)thisUser.Carts.CartID;
            Session["cartTotal"] = act.GetTotalAsync(Id);
            Session["wishlistTotal"] =  wishAct.GetWishlistTotal(thisUser.Wishlists.WishlistID);
            IEnumerable<CartItem> myItems = act.GetCartItemsAsync(Id);
            myHandler = new BusinessLogicHandler();
            IEnumerable<Book> ifBooks = myHandler.GetBooks();
            IEnumerable<Technology> ifGadget = myHandler.GetTechnology();
            ProductViewModel myNewModel = new ProductViewModel();
            myNewModel.allBook = ifBooks;
            myNewModel.allCartItem = myItems;
            myNewModel.allTechnology = ifGadget;
            List<ProductViewModel.CartHelper> itemList = new List<ProductViewModel.CartHelper>();
            ProductViewModel.CartHelper cartHelp;
            if (myItems != null)
            {
                var revised = from rev in ifBooks
                              join item in myItems on rev.ProductID equals item.ProductID
                              where rev.ProductID == item.ProductID
                              select new { rev.ProductID, rev.SellingPrice, item.Quantity };
                foreach (var ite in revised)
                {
                    cartHelp = new ProductViewModel.CartHelper();
                    cartHelp.ProductID = ite.ProductID;
                    cartHelp.TotalPerItem = (ite.SellingPrice * ite.Quantity);
                    itemList.Add(cartHelp);
                }
            }
            if (myItems != null)
            {
                if (ifGadget != null)
                {
                    var revised = from rev in ifGadget
                                  join item in myItems on rev.ProductID equals item.ProductID
                                  where rev.ProductID == item.ProductID
                                  select new { rev.ProductID, rev.SellingPrice, item.Quantity };
                    foreach (var ite in revised)
                    {
                        cartHelp = new ProductViewModel.CartHelper();
                        cartHelp.ProductID = ite.ProductID;
                        cartHelp.TotalPerItem = (ite.SellingPrice * ite.Quantity);
                        itemList.Add(cartHelp);
                    }
                }
            }
            double cartTotal = Convert.ToDouble(Session["cartTotal"].ToString());
            List<Company> company = myHandler.GetCompanyDetails();
            double vat = 0;
            foreach (var item in company)
            { vat = item.VATPercentage; }
            double vatAmount = (cartTotal * vat);
            double subTotal = (cartTotal - vatAmount);
            ProductViewModel.CartConclude finishing = new ProductViewModel.CartConclude();
            finishing.CartTotal = cartTotal;
            finishing.VatAddedTotal = vatAmount;
            finishing.SubTotal = subTotal;
            myNewModel.ItsA_wrap = new List<ProductViewModel.CartConclude>();
            myNewModel.ItsA_wrap.Add(finishing);

            myNewModel.secureCart = itemList;

            return View(myNewModel);
        }


        //public ActionResult Edit(int CartItemID)
        //{
        //    CartItem item = new CartItem();
        //    Cart cart = new Cart();
        //    BusinessLogicHandler myHandler = new BusinessLogicHandler();
        //    try
        //    {
        //        TryUpdateModel(cart);
        //        TryUpdateModel(item);
        //        if (ModelState.IsValid)
        //        {
        //            //myHandler.UpdateCart(cart);
        //            myHandler.UpdateCartItem(item);
        //        }
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        ApplicationUserManager userMgr;

        public async Task<ActionResult> AddToCart(int ProductID)
        {
            string userName = User.Identity.GetUserName();
            ApplicationDbContext dataSocket = new ApplicationDbContext();
            UserStore<ApplicationUser> myStore = new UserStore<ApplicationUser>(dataSocket);
            userMgr = new ApplicationUserManager(myStore);
            var user = await userMgr.FindByEmailAsync(userName);
            CartActions myActions = new CartActions();
            Cart cart = new Cart();
            cart.CartID = user.Carts.CartID;
            myHandler = new BusinessLogicHandler();
            IEnumerable<WishlistItem> inWishes = myHandler.GetWishlistItems(user.Wishlists.WishlistID);
            if (inWishes != null)
            {
                WishlistItem item = new WishlistItem();
                try
                {
                    item = inWishes.SingleOrDefault(items => items.ProductID == ProductID);
                    myHandler.DeleteWishlistItem(item.WishlistItemID);
                }
                catch { }
            }
            if (myActions.AddToCartAsync(cart.CartID, ProductID))
            { Session["cartTotal"] = await GetCartTotal(cart.CartID); }
            else
            { }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<ActionResult> UpdateQuantity(string quantity, string itemId)
        {

            string userName = User.Identity.GetUserName();
            ApplicationDbContext dataSocket = new ApplicationDbContext();
            UserStore<ApplicationUser> myStore = new UserStore<ApplicationUser>(dataSocket);
            userMgr = new ApplicationUserManager(myStore);
            var user = await userMgr.FindByEmailAsync(userName);


            myHandler = new BusinessLogicHandler();
            CartItem item = new CartItem();
            item.CartItemID = Convert.ToInt32(itemId);
            item.CartID = user.Carts.CartID;
            item.Quantity = Convert.ToInt32(quantity);
            if (myHandler.UpdateCartItem(item))
            { return Json(new { success = true }); }
            else
            { return Json("Error updating quantity"); }
        }

        public async Task<double> GetCartTotal(int CartID)
        {
            double total;
            CartActions myA = new CartActions();
            total = myA.GetTotalAsync(CartID);
            return total;
        }

        public ActionResult Delete(int CartItemID)
        {
            myHandler = new BusinessLogicHandler();
            myHandler.DeleteCartItem(CartItemID);

            return RedirectToAction("Edit");
        }

        public async Task<ActionResult> Checkout()
        {
            #region Data to Display
            CartActions act = new CartActions(); WishlistActions wishAct = new WishlistActions();
            ApplicationDbContext dataSocket = new ApplicationDbContext();
            UserStore<ApplicationUser> myStore = new UserStore<ApplicationUser>(dataSocket);
            userMgr = new ApplicationUserManager(myStore);
            var thisUser = await userMgr.FindByNameAsync(User.Identity.Name);
            int Id = (int)thisUser.Carts.CartID;
            Session["cartTotal"] =  act.GetTotalAsync(Id);
            Session["wishlistTotal"] = wishAct.GetWishlistTotal(thisUser.Wishlists.WishlistID);
            //List<CartItem> myItems = new List<CartItem>(); 
             IEnumerable<CartItem> myItems = act.GetCartItemsAsync(Id);

            myHandler = new BusinessLogicHandler();
            IEnumerable<Book> ifBooks = myHandler.GetBooks();
            IEnumerable<Technology> ifGadget = myHandler.GetTechnology();
            ProductViewModel myNewModel = new ProductViewModel();
            myNewModel.allBook = ifBooks;
            myNewModel.allCartItem = myItems;
            myNewModel.allTechnology = ifGadget;
            List<ProductViewModel.CartHelper> itemList = new List<ProductViewModel.CartHelper>();
            ProductViewModel.CartHelper cartHelp;
            if (myItems != null)
            {
                var revised = from rev in ifBooks
                              join item in myItems on rev.ProductID equals item.ProductID
                              where rev.ProductID == item.ProductID
                              select new { rev.ProductID, rev.SellingPrice, item.Quantity };
                foreach (var ite in revised)
                {
                    cartHelp = new ProductViewModel.CartHelper();
                    cartHelp.ProductID = ite.ProductID;
                    cartHelp.TotalPerItem = (ite.SellingPrice * ite.Quantity);
                    itemList.Add(cartHelp);
                }
            }
            if (myItems != null)
            {
                if (ifGadget != null)
                {
                    var revised = from rev in ifGadget
                                  join item in myItems on rev.ProductID equals item.ProductID
                                  where rev.ProductID == item.ProductID
                                  select new { rev.ProductID, rev.SellingPrice, item.Quantity };
                    foreach (var ite in revised)
                    {
                        cartHelp = new ProductViewModel.CartHelper();
                        cartHelp.ProductID = ite.ProductID;
                        cartHelp.TotalPerItem = (ite.SellingPrice * ite.Quantity);
                        itemList.Add(cartHelp);
                    }
                }
            }
            double cartTotal = Convert.ToDouble(Session["cartTotal"].ToString());
            List<Company> company = myHandler.GetCompanyDetails();
            double vat = 0;
            foreach (var item in company)
            { vat = item.VATPercentage; }
            double vatAmount = (cartTotal * vat);
            double subTotal = (cartTotal - vatAmount);
            ProductViewModel.CartConclude finishing = new ProductViewModel.CartConclude();
            finishing.CartTotal = cartTotal;
            finishing.VatAddedTotal = vatAmount;
            finishing.SubTotal = subTotal;
            myNewModel.ItsA_wrap = new List<ProductViewModel.CartConclude>();
            myNewModel.ItsA_wrap.Add(finishing);

            myNewModel.secureCart = itemList;

            #endregion

            #region Drop down data
            DeliveryHandler deliver = new DeliveryHandler();
            IEnumerable<Delivery> delivery = (IEnumerable<Delivery>)deliver.GetDeliveryList();
            var dataStore = from name in delivery
                            select new { Value = name.DeliveryServiceID, Text = name.ServiceName };
            ViewBag.DeliveryList = new SelectList(dataStore.ToList());

            List<SelectListItem> deliveryI = new List<SelectListItem>();
            deliveryI.Add(new SelectListItem { Text = "Delivery Service", Value = "", Selected = true });
            foreach (var item in delivery)
            { deliveryI.Add(new SelectListItem { Text = item.ServiceName, Value = item.DeliveryServiceID.ToString() }); }
            myNewModel.I_DeliveryList = new List<SelectListItem>();
            myNewModel.I_DeliveryList = deliveryI;
            ViewData["I_Delivery"] = deliveryI;
            #endregion

            #region Default Address
            if (thisUser.Address != null)
            { myNewModel.deliveryHelper = new DeliveryHelper(); myNewModel.deliveryHelper.DeliveryAddress = thisUser.Address; }
            #endregion
            return View(myNewModel);
        }

        [HttpPost]
        public ActionResult Checkout(ProductViewModel helperModel, FormCollection collection)
        {
            Delivery shipping = new Delivery();
            IEnumerable<Book> ifBooks = (IEnumerable<Book>)Session["myBooks"];
            IEnumerable<Technology> ifGadget = (IEnumerable<Technology>)Session["myGadget"];
            List<CartItem> myItems = (List<CartItem>)Session["myItems"];
            myHandler = new BusinessLogicHandler();
            shipping = myHandler.GetDeliveryDetails(Convert.ToInt32(collection[1].ToString()));

            #region Feed The Model


            CartItem thishereItem = new CartItem();
            try
            {
                ProductViewModel.CartHelper cartHelp;
                List<ProductViewModel.CartHelper> itemList = new List<ProductViewModel.CartHelper>();
                // if (myItems.Count == 0)
                // { return RedirectToAction("Edit"); }
                double cartTotal = 0;
                if (myItems != null)
                {
                    if (ifBooks != null)
                    {
                        var revised = from rev in ifBooks
                                      join item in myItems on rev.ProductID equals item.ProductID
                                      where rev.ProductID == item.ProductID
                                      select new { rev.ProductID, rev.SellingPrice, item.Quantity };
                        foreach (var ite in revised)
                        {
                            cartHelp = new ProductViewModel.CartHelper();
                            cartHelp.ProductID = ite.ProductID;
                            cartHelp.TotalPerItem = (ite.SellingPrice * ite.Quantity);
                            cartTotal += (ite.SellingPrice * ite.Quantity);
                            itemList.Add(cartHelp);
                        }
                    }
                    if (ifGadget != null)
                    {
                        var revised = from rev in ifGadget
                                      join item in myItems on rev.ProductID equals item.ProductID
                                      where rev.ProductID == item.ProductID
                                      select new { rev.ProductID, rev.SellingPrice, item.Quantity };
                        foreach (var ite in revised)
                        {
                            cartHelp = new ProductViewModel.CartHelper();
                            cartHelp.ProductID = ite.ProductID;
                            cartHelp.TotalPerItem = (ite.SellingPrice * ite.Quantity);
                            cartTotal += (ite.SellingPrice * ite.Quantity);
                            itemList.Add(cartHelp);
                        }
                    }
                }
                List<Company> company = new List<Company>(); myHandler = new BusinessLogicHandler();
                company = myHandler.GetCompanyDetails();
                double vat = 0;
                foreach (var item in company)
                { vat = item.VATPercentage; }
                //calc
                double vatAmount = (cartTotal * vat);
                double subTotal = (cartTotal - vatAmount);
                ProductViewModel.CartConclude finishing = new ProductViewModel.CartConclude();
                finishing.CartTotal = cartTotal;
                finishing.VatAddedTotal = vatAmount;
                finishing.SubTotal = subTotal;
                helperModel.ItsA_wrap = new List<ProductViewModel.CartConclude>();
                helperModel.ItsA_wrap.Add(finishing);

                helperModel.deliveryHelper.DeliveryServiceName = shipping.ServiceName;
                helperModel.deliveryHelper.DeliveryServicePrice = shipping.Price;
                helperModel.deliveryHelper.DeliveryServiceType = shipping.ServiceType;

                helperModel.secureCart = itemList;
                helperModel.allBook = ifBooks;
                helperModel.allCartItem = myItems;

                helperModel.allTechnology = ifGadget;
            }
            catch { }
            #endregion

            if (ModelState.IsValid)
            {
                #region Get User
                string userName = User.Identity.GetUserName();
                ApplicationDbContext dataSocket = new ApplicationDbContext();
                UserStore<ApplicationUser> myStore = new UserStore<ApplicationUser>(dataSocket);
                userMgr = new ApplicationUserManager(myStore);
                var user = userMgr.FindByEmail(userName);
                #endregion

                int bookSupplier = 0;
                int technologySupplier = 0;
                try
                {
                    #region Creating the reciept/invoice
                    Invoice reciept = new Invoice {User_Id = user.Id, DateCreated = DateTime.Now, DeliveryAddress = helperModel.deliveryHelper.DeliveryAddress, DeliveryServiceID = Convert.ToInt32(collection[1].ToString()), Status = false };
                    try
                    {
                        InvoiceItem invoiceLine = new InvoiceItem();
                        invoiceLine = myHandler.GetInvoiceLastNumber(reciept);
                        foreach (var item in myItems)
                        {
                            invoiceLine.CartItemID = item.CartItemID;
                            invoiceLine.ProductID = item.ProductID;
                            invoiceLine.Quantity = item.Quantity;
                            myHandler.AddinvoiceItem(invoiceLine);
                        }

                    }
                    catch { }
                    #endregion



                    #region Placing the order
                    try
                    {
                        foreach (var item in myItems)
                        {
                            if (ifBooks != null)
                            {
                                Order ord = new Order { DateCreated = DateTime.Now.Date, DateLastModified = DateTime.Now.Date, Status = false };
                                OrderItem orderLine = new OrderItem();
                                orderLine = myHandler.AddOrder(ord);
                                foreach (var book in ifBooks)
                                {
                                   
                                    

                                    if(book.ProductID == item.ProductID)
                                    {
                                        ord.SupplierID = book.SupplierID;
                                        
                                        orderLine.ProductID = book.ProductID;
                                        orderLine.Quantity = item.Quantity;
                                        myHandler.AddOrderItem(orderLine);
                                    }
                                    
                                }
                            }
                            if (ifGadget != null)
                            {

                                foreach(var gadget in ifGadget)
                                {
                                    Order ord = new Order { DateCreated = DateTime.Now.Date, DateLastModified = DateTime.Now.Date, SupplierID = gadget.SupplierID, Status = false };
                                    OrderItem orderLine = new OrderItem();
                                    if (gadget.ProductID == item.ProductID)
                                    {

                                        orderLine = myHandler.AddOrder(ord);
                                        orderLine.ProductID = gadget.ProductID;
                                        orderLine.Quantity = item.Quantity;
                                        myHandler.AddOrderItem(orderLine);
                                    }
                                }
                            }
                        }
                    }
                    catch { }
                    #endregion
                }
                catch
                {/*Navigate to custom error page*/ }
                Session["deliverData"] = helperModel;/////////////////
                return RedirectToAction("Reciept");
            }
            else
            { return View(helperModel); }
        }

        public async Task<ActionResult> _AddToCart(int ProductID, int wishID)
        {
            string userName = User.Identity.GetUserName();
            ApplicationDbContext dataSocket = new ApplicationDbContext();
            UserStore<ApplicationUser> myStore = new UserStore<ApplicationUser>(dataSocket);
            userMgr = new ApplicationUserManager(myStore);
            var user = await userMgr.FindByEmailAsync(userName);
            //int customer = (int)user.Customers.CustomerID;
            CartActions myActions = new CartActions();
            Cart cart = new Cart();
            cart.CartID = user.Carts.CartID;
            if (myActions.AddToCartAsync(cart.CartID, ProductID))
            { Session["cartTotal"] = await GetCartTotal(cart.CartID); myHandler = new BusinessLogicHandler(); myHandler.DeleteWishlistItem(wishID); }
            else
            { }
            //return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            return RedirectToAction("Index", "Home");
        }


        public ActionResult _Remove(int Id)
        {
            ProductViewModel helperModel = new ProductViewModel();
            IEnumerable<Book> ifBooks = (IEnumerable<Book>)Session["myBooks"];
            IEnumerable<Technology> ifGadget = (IEnumerable<Technology>)Session["myGadget"];
            List<CartItem> myItems = (List<CartItem>)Session["myItems"];
            CartItem thishereItem = new CartItem();
            try
            {
                thishereItem = myItems.Single(kill => kill.CartItemID == Id);
                int pId = thishereItem.ProductID;
                ProductViewModel.CartHelper cartHelp;
                List<ProductViewModel.CartHelper> itemList = new List<ProductViewModel.CartHelper>();
                myItems.Remove(thishereItem);
                if (myItems.Count == 0)
                { return RedirectToAction("Edit"); }
                double cartTotal = 0;
                if (myItems != null)
                {
                    var revised = from rev in ifBooks
                                  join item in myItems on rev.ProductID equals item.ProductID
                                  where rev.ProductID == item.ProductID
                                  select new { rev.ProductID, rev.SellingPrice, item.Quantity };
                    foreach (var ite in revised)
                    {
                        cartHelp = new ProductViewModel.CartHelper();
                        cartHelp.ProductID = ite.ProductID;
                        cartHelp.TotalPerItem = (ite.SellingPrice * ite.Quantity);
                        cartTotal += (ite.SellingPrice * ite.Quantity);
                        itemList.Add(cartHelp);
                    }
                }
                if (myItems != null)
                {
                    var revised = from rev in ifGadget
                                  join item in myItems on rev.ProductID equals item.ProductID
                                  where rev.ProductID == item.ProductID
                                  select new { rev.ProductID, rev.SellingPrice, item.Quantity };
                    foreach (var ite in revised)
                    {
                        cartHelp = new ProductViewModel.CartHelper();
                        cartHelp.ProductID = ite.ProductID;
                        cartHelp.TotalPerItem = (ite.SellingPrice * ite.Quantity);
                        cartTotal += (ite.SellingPrice * ite.Quantity);
                        itemList.Add(cartHelp);
                    }
                }
                List<Company> company = new List<Company>(); BusinessLogicHandler myHandler = new BusinessLogicHandler();
                company = myHandler.GetCompanyDetails();
                double vat = 0;
                foreach (var item in company)
                { vat = item.VATPercentage; }
                double vatAmount = (cartTotal * vat);
                double subTotal = (cartTotal - vatAmount);
                ProductViewModel.CartConclude finishing = new ProductViewModel.CartConclude();
                finishing.CartTotal = cartTotal;
                finishing.VatAddedTotal = vatAmount;
                finishing.SubTotal = subTotal;
                helperModel.ItsA_wrap = new List<ProductViewModel.CartConclude>();
                helperModel.ItsA_wrap.Add(finishing);

                helperModel.secureCart = itemList;
                helperModel.allBook = ifBooks;
                helperModel.allCartItem = myItems;

                helperModel.allTechnology = ifGadget;
                return View("Checkout", helperModel);
            }
            catch
            { return RedirectToAction("Edit"); }
        }
        [HttpPost]
        public ActionResult GetDeliveryPrice(string selectedValue)
        {
            myHandler = new BusinessLogicHandler();
            Delivery myHelper = new Delivery();
            myHelper = myHandler.GetDeliveryDetails(Convert.ToInt32(selectedValue.ToString()));
            string thiz = decimal.Round(myHelper.Price, 2, MidpointRounding.AwayFromZero).ToString();
            return Json(thiz);
        }

        public ActionResult Confirm()
        {
            ProductViewModel helperModel = new ProductViewModel();
            IEnumerable<Book> ifBooks = (IEnumerable<Book>)Session["myBooks"];
            IEnumerable<Technology> ifGadget = (IEnumerable<Technology>)Session["myGadget"];
            List<CartItem> myItems = (List<CartItem>)Session["myItems"];

            helperModel.allBook = ifBooks;
            helperModel.allCartItem = myItems;
            helperModel.allTechnology = ifGadget;

            //Counting and Totalling
            double cartTotal = 0;
            ProductViewModel.CartHelper cartHelp;
            List<ProductViewModel.CartHelper> itemList = new List<ProductViewModel.CartHelper>();
            if (myItems != null)
            {
                var revised = from rev in ifBooks
                              join item in myItems on rev.ProductID equals item.ProductID
                              where rev.ProductID == item.ProductID
                              select new { rev.ProductID, rev.SellingPrice, item.Quantity };
                foreach (var ite in revised)
                {
                    cartHelp = new ProductViewModel.CartHelper();
                    cartHelp.ProductID = ite.ProductID;
                    cartHelp.TotalPerItem = (ite.SellingPrice * ite.Quantity);
                    cartTotal += (ite.SellingPrice * ite.Quantity);
                    itemList.Add(cartHelp);
                }
            }
            if (myItems != null)
            {
                var revised = from rev in ifGadget
                              join item in myItems on rev.ProductID equals item.ProductID
                              where rev.ProductID == item.ProductID
                              select new { rev.ProductID, rev.SellingPrice, item.Quantity };
                foreach (var ite in revised)
                {
                    cartHelp = new ProductViewModel.CartHelper();
                    cartHelp.ProductID = ite.ProductID;
                    cartHelp.TotalPerItem = (ite.SellingPrice * ite.Quantity);
                    cartTotal += (ite.SellingPrice * ite.Quantity);
                    itemList.Add(cartHelp);
                }
            }
            List<Company> company = new List<Company>(); BusinessLogicHandler myHandler = new BusinessLogicHandler();
            company = myHandler.GetCompanyDetails();
            double vat = 0;
            foreach (var item in company)
            { vat = item.VATPercentage; }
            double vatAmount = (cartTotal * vat);
            double subTotal = (cartTotal - vatAmount);
            ProductViewModel.CartConclude finishing = new ProductViewModel.CartConclude();
            finishing.CartTotal = cartTotal;
            finishing.VatAddedTotal = vatAmount;
            finishing.SubTotal = subTotal;
            helperModel.ItsA_wrap = new List<ProductViewModel.CartConclude>();
            helperModel.ItsA_wrap.Add(finishing);

            helperModel.secureCart = itemList;

            //HAVE TO ADD BILLING ************************
            return PartialView(helperModel);
        }

        public ActionResult Reciept(ProductViewModel model)
        {
            IEnumerable<Book> ifBooks = (IEnumerable<Book>)Session["myBooks"];
            IEnumerable<Technology> ifGadget = (IEnumerable<Technology>)Session["myGadget"];
            List<CartItem> myItems = (List<CartItem>)Session["myItems"];
            model = (ProductViewModel)Session["deliverData"];///////////////////////////////////////////

            //model.allBook = ifBooks;
            //model.allTechnology = ifGadget;
            //model.allCartItem = myItems;

            #region Calculate 

            List<ProductViewModel.CartHelper> itemList = new List<ProductViewModel.CartHelper>();
            ProductViewModel.CartHelper cartHelp;
            if (myItems != null)
            {
                var revised = from rev in ifBooks
                              join item in myItems on rev.ProductID equals item.ProductID
                              where rev.ProductID == item.ProductID
                              select new { rev.ProductID, rev.SellingPrice, item.Quantity };
                foreach (var ite in revised)
                {
                    cartHelp = new ProductViewModel.CartHelper();
                    cartHelp.ProductID = ite.ProductID;
                    cartHelp.TotalPerItem = (ite.SellingPrice * ite.Quantity);
                    itemList.Add(cartHelp);
                }
            }
            if (myItems != null)
            {
                if (ifGadget != null)
                {
                    var revised = from rev in ifGadget
                                  join item in myItems on rev.ProductID equals item.ProductID
                                  where rev.ProductID == item.ProductID
                                  select new { rev.ProductID, rev.SellingPrice, item.Quantity };
                    foreach (var ite in revised)
                    {
                        cartHelp = new ProductViewModel.CartHelper();
                        cartHelp.ProductID = ite.ProductID;
                        cartHelp.TotalPerItem = (ite.SellingPrice * ite.Quantity);
                        itemList.Add(cartHelp);
                    }
                }
            }

            double cartTotal = Convert.ToDouble(Session["cartTotal"].ToString());
            myHandler = new BusinessLogicHandler();
            List<Company> company = myHandler.GetCompanyDetails();
            double vat = 0;
            foreach (var item in company)
            { vat = item.VATPercentage; }
            double vatAmount = (cartTotal * vat);
            double subTotal = (cartTotal - vatAmount);
            ProductViewModel.CartConclude finishing = new ProductViewModel.CartConclude();
            finishing.CartTotal = cartTotal;
            finishing.VatAddedTotal = vatAmount;
            finishing.SubTotal = subTotal;
            model.ItsA_wrap = new List<ProductViewModel.CartConclude>();
            model.ItsA_wrap.Add(finishing);

            model.secureCart = itemList;
            #endregion

            #region Push User Details

            string userName = User.Identity.GetUserName();
            ApplicationDbContext dataSocket = new ApplicationDbContext();
            UserStore<ApplicationUser> myStore = new UserStore<ApplicationUser>(dataSocket);
            userMgr = new ApplicationUserManager(myStore);
            var user = userMgr.FindByEmail(userName);

            model.UserDetails = new ProvideUser();
            model.UserDetails.Address = user.Address;
            model.UserDetails.email = user.Email;
            model.UserDetails.PhoneNumber = user.PhoneNumber;
            CustomerContext customer = new CustomerContext();
            Customer thisCust = new Customer();
            thisCust = customer.Customers.FirstOrDefault(cust => cust.User_Id == user.Id);
            model.UserDetails.LName = thisCust.LastName;
            model.UserDetails.Name = thisCust.FirstName;
            #endregion

            return View(model);
        }

    }
}
