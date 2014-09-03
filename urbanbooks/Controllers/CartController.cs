using System;
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
            Session["wishlistTotal"] = wishAct.GetWishlistTotal(thisUser.Wishlists.WishlistID);
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
            vat = vat+1;
            double subTotal = cartTotal / vat;
            double vatAmount = cartTotal - subTotal;
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

        public async Task<ActionResult> AddToCart(int ProductID, string returnUrl)
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
            return Redirect(returnUrl);
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
            { return Json("Error updating quantity"); }//
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
            Session["cartTotal"] = act.GetTotalAsync(Id);
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
            vat = vat + 1;
            double subTotal = cartTotal / vat;
            double vatAmount = cartTotal - subTotal;
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

                try
                {
                    #region Creating the reciept/invoice
                    Invoice reciept = new Invoice { User_Id = user.Id, DateCreated = DateTime.Now, DeliveryAddress = helperModel.deliveryHelper.DeliveryAddress, DeliveryServiceID = Convert.ToInt32(collection[1].ToString()), Status = false };
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
                        Session["InvoiceID"] = invoiceLine.InvoiceID;

                    }
                    catch { }
                    #endregion


                    #region Placing the order
                    try
                    {
                        Order ord;
                        int supplierId = 0;
                        OrderItem orderLine = new OrderItem();
                        myHandler = new BusinessLogicHandler();
                        List<int> orders = new List<int>();
                        List<int> suppliers = new List<int>();
                        foreach (var item in myItems)
                        {
                            try
                            {
                                supplierId = ifBooks.SingleOrDefault(m => m.ProductID == item.ProductID).SupplierID;
                                if (suppliers.Contains(supplierId))
                                {
                                    int x = suppliers.IndexOf(supplierId);
                                    orderLine.OrderNo = orders.ElementAt(x);
                                    orderLine.ProductID = item.ProductID;
                                    orderLine.Quantity = item.Quantity;
                                    myHandler.AddOrderItem(orderLine);
                                }
                                else
                                {
                                    suppliers.Add(supplierId);
                                    ord = new Order { DateCreated = DateTime.Now.Date, SupplierID = supplierId, InvoiceID = (int)Session["InvoiceID"], DateLastModified = DateTime.Now.Date, Status = false };
                                    orderLine = myHandler.AddOrder(ord);
                                    orders.Add(orderLine.OrderNo);
                                    orderLine.ProductID = item.ProductID;
                                    orderLine.Quantity = item.Quantity;
                                    myHandler.AddOrderItem(orderLine);
                                }

                            }
                            catch
                            {
                                supplierId = ifGadget.SingleOrDefault(m => m.ProductID == item.ProductID).SupplierID;
                                if (suppliers.Contains(supplierId))
                                {

                                }
                                else
                                {
                                    suppliers.Add(supplierId);
                                    ord = new Order { DateCreated = DateTime.Now.Date, SupplierID = supplierId, InvoiceID = (int)Session["InvoiceID"], DateLastModified = DateTime.Now.Date, Status = false };
                                    orderLine = myHandler.AddOrder(ord);
                                    orders.Add(orderLine.OrderNo);
                                }
                            }
                        }
                    }
                    catch { }
                    #endregion
                }
                catch
                {/*Navigate to custom error page*/ }
                Session["deliverData"] = helperModel;
                return RedirectToAction("Reciept");
            }
            else
            { return View(helperModel); }
        }

        public async Task<ActionResult> _AddToCart(int ProductID, int wishID, string returnUrl)
        {
            string userName = User.Identity.GetUserName();
            ApplicationDbContext dataSocket = new ApplicationDbContext();
            UserStore<ApplicationUser> myStore = new UserStore<ApplicationUser>(dataSocket);
            userMgr = new ApplicationUserManager(myStore);
            var user = await userMgr.FindByEmailAsync(userName);
            //int customer = (int)user.Customers.CustomerID;
            CartActions myActions = new CartActions();
            WishlistActions wishes = new WishlistActions();
            Cart cart = new Cart();
            cart.CartID = user.Carts.CartID;
            if (myActions.AddToCartAsync(cart.CartID, ProductID))
            {
                myHandler = new BusinessLogicHandler(); 
                myHandler.DeleteWishlistItem(wishID); 
                Session["cartTotal"] = await GetCartTotal(cart.CartID);
                Session["wishlistTotal"] = wishes.GetWishlistTotal(user.Wishlists.WishlistID);
            }
            else
            { }
            //return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            return Redirect(returnUrl); //RedirectToAction(returnUrl);
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

                #region Calculate Total Per Item
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
                #endregion

                #region Totalling 
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
#endregion

                #region Feeding the model
                helperModel.ItsA_wrap = new List<ProductViewModel.CartConclude>();
                helperModel.ItsA_wrap.Add(finishing);

                helperModel.secureCart = itemList;
                helperModel.allBook = ifBooks;
                helperModel.allCartItem = myItems;

                helperModel.allTechnology = ifGadget;
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
                helperModel.I_DeliveryList = new List<SelectListItem>();
                helperModel.I_DeliveryList = deliveryI;
                ViewData["I_Delivery"] = deliveryI;
                #endregion

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
            vat = vat + 1;
            double subTotal = cartTotal / vat;
            double vatAmount = cartTotal - subTotal;
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
            model = (ProductViewModel)Session["deliverData"];

            #region Calculate

            List<ProductViewModel.CartHelper> itemList = new List<ProductViewModel.CartHelper>();
            ProductViewModel.CartHelper cartHelp;
            if (myItems != null)
            {
                var revised = from rev in ifBooks
                              join item in myItems on rev.ProductID equals item.ProductID
                              where rev.ProductID == item.ProductID
                              select new { rev.ProductID, rev.SellingPrice, item.Quantity, item.CartItemID };
                foreach (var ite in revised)
                {
                    cartHelp = new ProductViewModel.CartHelper();
                    cartHelp.ProductID = ite.ProductID;
                    cartHelp.CartItemID = ite.CartItemID;
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
                                  select new { rev.ProductID, rev.SellingPrice, item.Quantity, item.CartItemID };
                    foreach (var ite in revised)
                    {
                        cartHelp = new ProductViewModel.CartHelper();
                        cartHelp.ProductID = ite.ProductID;
                        cartHelp.CartItemID = ite.CartItemID;
                        cartHelp.TotalPerItem = (ite.SellingPrice * ite.Quantity);
                        itemList.Add(cartHelp);
                    }
                }
            }

            double cartTotal = Convert.ToDouble(Session["cartTotal"].ToString());
            myHandler = new BusinessLogicHandler();
            List<Company> company = myHandler.GetCompanyDetails();
            double vat = 0;
            cartTotal += Convert.ToDouble(model.deliveryHelper.DeliveryServicePrice);
            foreach (var item in company)
            { vat = item.VATPercentage; }
            vat = vat + 1;
            double subTotal = cartTotal / vat;
            double vatAmount = cartTotal - subTotal;
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

            #region Push Invoice nfo
            model.recieptData = new Invoice();
            model.recieptData.DateCreated = DateTime.Now.Date;
            model.recieptData = new Invoice();
            model.recieptData.InvoiceID = (int)Session["InvoiceID"];

            #endregion

            #region Push Company nfo

            myHandler = new BusinessLogicHandler();
            model.company = new Company();
            model.company = myHandler.GetCompanyDetail();

            #endregion

            #region Clear the cart
            foreach (var item in itemList)
            {
                myHandler = new BusinessLogicHandler();
                myHandler.DeleteCartItem(item.CartItemID);
            }
            
            #endregion

            return View(model);
        }

    }
}
