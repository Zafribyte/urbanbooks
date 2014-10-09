using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using urbanbooks.Models;
using Rotativa;
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
            try
            { double nm = await GetCartTotal(Id); string[] xn = nm.ToString().Split('.'); Session["cartTotal"] = xn[0] + "," + xn[1]; }
            catch { Session["cartTotal"] = act.GetTotalAsync(Id); }
            
            Session["wishlistTotal"] = wishAct.GetWishlistTotal(thisUser.Wishlists.WishlistID);
            List<CartItem> myItems = new List<CartItem>();
            try
            { myItems =  act.GetCartItemsAsync(Id).ToList();}
            catch(ArgumentNullException)
            { myItems = null; }
            
            myHandler = new BusinessLogicHandler();
            List<Book> ifBooks = new List<Book>();
            ProductViewModel myNewModel = new ProductViewModel();
            List<Technology> ifGadget = new List<Technology>();
            if (myItems != null)
            {
                foreach (var item in myItems)
                {
                    if (myHandler.CheckProductType(item.ProductID))
                    {
                        Book book = new Book();
                        book = myHandler.GetBook(item.ProductID);
                        ifBooks.Add(book);
                    }
                    else
                    {
                        Technology device = new Technology();
                        device = myHandler.GetTechnologyDetails(item.ProductID);
                        ifGadget.Add(device);
                    }
                }

                
                myNewModel.allCartItem = new List<CartItem>();
                myNewModel.allBook = new List<Book>();
                myNewModel.allTechnology = new List<Technology>();
                List<ProductViewModel.CartHelper> itemList = new List<ProductViewModel.CartHelper>();
                ProductViewModel.CartHelper cartHelp;
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
                            itemList.Add(cartHelp);
                        }
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
                cartTotal = cartTotal / 100;
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
                myNewModel.allBook = ifBooks;
                myNewModel.allCartItem = myItems;
                myNewModel.allTechnology = ifGadget;
                myNewModel.ItsA_wrap = new List<ProductViewModel.CartConclude>();
                myNewModel.ItsA_wrap.Add(finishing);

                myNewModel.secureCart = itemList;
                return View(myNewModel);
            }
            else
            {
                myNewModel.ItsA_wrap = new List<ProductViewModel.CartConclude>();
                return View(myNewModel);
            }

        }


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
                    TempData["cartAdd"] = "1 item added to cart";
                }
                catch { }
            }
            if (myActions.AddToCartAsync(user.Carts.CartID, ProductID))
            {
                try
                {
                    double nm = await GetCartTotal(user.Carts.CartID); string[] xn = nm.ToString().Split('.'); Session["cartTotal"] = xn[0] + "," + xn[1];
                    
                }
                catch
                { Session["cartTotal"] = GetCartTotal(user.Carts.CartID); }
            }
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
            { return Redirect("/Cart/Edit"); }
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
        [RestoreModelStateFromTempData]
        public async Task<ActionResult> Checkout()
        {

            #region Data to Display
            CartActions act = new CartActions(); WishlistActions wishAct = new WishlistActions();
            ApplicationDbContext dataSocket = new ApplicationDbContext();
            UserStore<ApplicationUser> myStore = new UserStore<ApplicationUser>(dataSocket);
            userMgr = new ApplicationUserManager(myStore);
            var thisUser = await userMgr.FindByNameAsync(User.Identity.Name);
            int Id = (int)thisUser.Carts.CartID;


            try
            { double nm = await GetCartTotal(Id); string[] xn = nm.ToString().Split('.'); Session["cartTotal"] = xn[0] + "," + xn[1]; }
            catch { Session["cartTotal"] = act.GetTotalAsync(Id); }


            Session["wishlistTotal"] = wishAct.GetWishlistTotal(thisUser.Wishlists.WishlistID);
            //List<CartItem> myItems = new List<CartItem>(); 
            ProductViewModel myNewModel = new ProductViewModel();
            IEnumerable<CartItem> myItems = act.GetCartItemsAsync(Id);
            if (myItems != null)
            {
                myHandler = new BusinessLogicHandler();
                List<Book> ifBooks = new List<Book>();
                List<Technology> ifGadget = new List<Technology>();

                foreach (var item in myItems)
                {
                    if (myHandler.CheckProductType(item.ProductID))
                    {
                        Book book = new Book();
                        book = myHandler.GetBook(item.ProductID);
                        ifBooks.Add(book);
                    }
                    else
                    {
                        Technology device = new Technology();
                        device = myHandler.GetTechnologyDetails(item.ProductID);
                        ifGadget.Add(device);
                    }
                }

                myNewModel.allBook = ifBooks;
                myNewModel.allCartItem = myItems.ToList();
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
                cartTotal = cartTotal / 100;
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
            }
            else
            { return RedirectToAction("Edit"); }

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
        public ActionResult Checkout(FormCollection collection, ProductViewModel model)
        {
            Delivery shipping = new Delivery();
            IEnumerable<Book> ifBooks = (IEnumerable<Book>)Session["myBooks"];
            IEnumerable<Technology> ifGadget = (IEnumerable<Technology>)Session["myGadget"];
            List<CartItem> myItems = (List<CartItem>)Session["myItems"];
            myHandler = new BusinessLogicHandler();

            #region Get Shipping Data
            try
            {
                shipping = myHandler.GetDeliveryDetails(Convert.ToInt32(collection[1].ToString()));

                if (ModelState.ContainsKey("I_DeliveryList"))
                    ModelState["I_DeliveryList"].Errors.Clear();
            }
            catch
            { ModelState.AddModelError("deliveryHelper.DeliveryServicePrice", "Please select a delivery service from dropdown !"); }
            #endregion

            #region Cathing model errors
            var error = ModelState.Values.SelectMany(e => e.Errors);
            var errors = ModelState
    .Where(x => x.Value.Errors.Count > 0)
    .Select(x => new { x.Key, x.Value.Errors })
    .ToArray();
            #endregion

            int? IID = 0;
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
                    Invoice reciept = new Invoice { User_Id = user.Id, DateCreated = DateTime.Now, DeliveryAddress = model.deliveryHelper.DeliveryAddress, DeliveryServiceID = Convert.ToInt32(collection[1].ToString()), Status = false };
                    try
                    {
                        InvoiceItem invoiceLine = new InvoiceItem();
                        invoiceLine = myHandler.GetInvoiceLastNumber(reciept);
                        foreach (var item in myItems)
                        {
                            invoiceLine.CartItemID = item.CartItemID;
                            invoiceLine.ProductID = item.ProductID;
                            invoiceLine.Quantity = item.Quantity;

                            #region Get Product Price
                            bool chk = false;
                            chk = myHandler.CheckProductType(item.ProductID);
                            if (chk)
                            {
                                Book book = new Book();
                                book = myHandler.GetBook(item.ProductID);
                                invoiceLine.Price = book.SellingPrice;
                                myHandler.AddinvoiceItem(invoiceLine);
                            }
                            else
                            {
                                Technology device = new Technology();
                                device = myHandler.GetTechnologyDetails(item.ProductID);
                                invoiceLine.Price = device.SellingPrice;
                                myHandler.AddinvoiceItem(invoiceLine);
                            }

                            //try
                            //{
                            //    Book book = new Book();
                            //    book = myHandler.GetBook(item.ProductID);
                            //    invoiceLine.Price = book.SellingPrice;
                            //}
                            //catch
                            //{
                            //    Technology device = new Technology();
                            //    device = myHandler.GetTechnologyDetails(item.ProductID);
                            //    invoiceLine.Price = device.SellingPrice;
                            //}
                            #endregion


                        }
                        IID = invoiceLine.InvoiceID;

                    }
                    catch { }
                    #endregion


                    #region Placing the order
                    try
                    {

                        #region Prep Utilities

                        Order ord;
                        Book book = new Book();
                        Technology gadget = new Technology();
                        int supplierId = 0;
                        OrderItem orderLine = new OrderItem();
                        myHandler = new BusinessLogicHandler();
                        List<int> orders = new List<int>();
                        List<int> suppliers = new List<int>();

                        #endregion

                        foreach (var item in myItems)
                        {
                            if (myHandler.CheckProductType(item.ProductID))
                            {
                                book = myHandler.GetBook(item.ProductID);
                                supplierId = book.SupplierID;
                                if (suppliers.Contains(book.SupplierID))
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
                                    ord = new Order { DateCreated = DateTime.Now.Date, SupplierID = supplierId, InvoiceID = IID.GetValueOrDefault(), DateLastModified = DateTime.Now.Date, Status = false };
                                    orderLine = myHandler.AddOrder(ord);
                                    orders.Add(orderLine.OrderNo);
                                    orderLine.ProductID = item.ProductID;
                                    orderLine.Quantity = item.Quantity;
                                    myHandler.AddOrderItem(orderLine);
                                }

                            }
                            else
                            {
                                supplierId = ifGadget.SingleOrDefault(m => m.ProductID == item.ProductID).SupplierID;
                                if (suppliers.Contains(supplierId))
                                {
                                    int y = suppliers.IndexOf(supplierId);
                                    orderLine.OrderNo = orders.ElementAt(y);
                                    orderLine.ProductID = item.ProductID;
                                    orderLine.Quantity = item.Quantity;
                                    myHandler.AddOrderItem(orderLine);
                                }
                                else
                                {
                                    suppliers.Add(supplierId);
                                    ord = new Order { DateCreated = DateTime.Now.Date, SupplierID = supplierId, InvoiceID = IID.GetValueOrDefault(), DateLastModified = DateTime.Now.Date, Status = false };
                                    orderLine = myHandler.AddOrder(ord);
                                    orders.Add(orderLine.OrderNo);
                                    orderLine.ProductID = item.ProductID;
                                    orderLine.Quantity = item.Quantity;
                                    myHandler.AddOrderItem(orderLine);
                                }
                            }
                        }
                    }
                    catch { }
                    #endregion
                }
                catch
                {/*Navigate to custom error page*/ }
                Session["deliverData"] = model;
                return RedirectToAction("Receipt", new { IID = IID });
            }
            else
            {
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
                    model.ItsA_wrap = new List<ProductViewModel.CartConclude>();
                    model.ItsA_wrap.Add(finishing);

                    model.deliveryHelper.DeliveryServiceName = shipping.ServiceName;
                    model.deliveryHelper.DeliveryServicePrice = shipping.Price;
                    model.deliveryHelper.DeliveryServiceType = shipping.ServiceType;

                    model.secureCart = itemList;
                    model.allBook = new List<Book>();
                    model.allBook = ifBooks.ToList() ;
                    model.allCartItem = new List<CartItem>();
                    model.allCartItem = myItems;
                    model.allTechnology = new List<Technology>();
                    model.allTechnology = ifGadget.ToList();
                }
                catch { }
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
                model.I_DeliveryList = new List<SelectListItem>();
                model.I_DeliveryList = deliveryI;
                ViewData["I_Delivery"] = deliveryI;
                #endregion

                return View(model);
            }
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
                helperModel.allBook = new List<Book>();
                helperModel.allBook = ifBooks.ToList();
                helperModel.allCartItem = new List<CartItem>();
                helperModel.allCartItem = myItems;
                helperModel.allTechnology = new List<Technology>();
                helperModel.allTechnology = ifGadget.ToList();
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
            helperModel.allBook = new List<Book>();
            helperModel.allCartItem = new List<CartItem>();
            helperModel.allTechnology = new List<Technology>();
            helperModel.allBook = ifBooks.ToList();
            helperModel.allCartItem = myItems.ToList();
            helperModel.allTechnology = ifGadget.ToList();

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

        public async Task<ActionResult> Receipt(int? IID)
        {
            List<CartItem> myItems = new List<CartItem>();
            myItems = (List<CartItem>)Session["myItems"];
            ProductViewModel model = new ProductViewModel();
            myHandler = new BusinessLogicHandler();
            model = (ProductViewModel)Session["deliverData"];

            #region Get Cart Items

            List<Book> ifBooks = new List<Book>();
            List<Technology> ifGadget = new List<Technology>();

            foreach (var item in myItems)
            {
                if (myHandler.CheckProductType(item.ProductID))
                {
                    Book book = new Book();
                    book = myHandler.GetBook(item.ProductID);
                    ifBooks.Add(book);
                }
                else
                {
                    Technology gadget = new Technology();
                    gadget = myHandler.GetTechnologyDetails(item.ProductID);
                    ifGadget.Add(gadget);
                }

                //try
                //{
                //    Book book = new Book();
                //    book = myHandler.GetBook(item.ProductID);
                //    ifBooks.Add(book);
                //}
                //catch
                //{
                //    Technology gadget = new Technology();
                //    gadget = myHandler.GetTechnologyDetails(item.ProductID);
                //    ifGadget.Add(gadget);
                //}
            }

            model.allTechnology = ifGadget;
            model.allBook = ifBooks;
            model.allCartItem = myItems;

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
            model.recieptData = myHandler.GetInvoice(IID.GetValueOrDefault());

            #endregion

            #region Push Company nfo

            myHandler = new BusinessLogicHandler();
            model.company = new Company();
            model.company = myHandler.GetCompanyDetail();

            #endregion

            #region Push Delivery nfo

            Delivery shipping = new Delivery();
            myHandler = new BusinessLogicHandler();
            shipping = myHandler.GetDeliveryDetails(model.recieptData.DeliveryServiceID);
            model.deliveryHelper.DeliveryServiceName = shipping.ServiceName;
            model.deliveryHelper.DeliveryServicePrice = shipping.Price;
            model.deliveryHelper.DeliveryServiceType = shipping.ServiceType;

            #endregion

            #region Calculate

            List<ProductViewModel.CartHelper> itemList = new List<ProductViewModel.CartHelper>();
            ProductViewModel.CartHelper cartHelp;
            if (myItems != null)
            {
                if (ifBooks != null)
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
            cartTotal = cartTotal / 100;
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

            #region Clear the cart
            foreach (var item in itemList)
            {
                myHandler = new BusinessLogicHandler();
                myHandler.DeleteCartItem(item.CartItemID);
            }

            #endregion

            #region Clear SESSION
            Session["myItems"] = null;
            Session["cartTotal"] = (double) await GetCartTotal(user.Carts.CartID);
            #endregion

            #region Prep for exporting to PDF
            Session["pdf"] = model;
            #endregion

            return View(model);
        }

        public ActionResult ExportToPDF()
        {
            #region Get the data
            ProductViewModel model = new ProductViewModel();
            model = (ProductViewModel)Session["pdf"];
            #endregion

            return new ViewAsPdf(model);
        }

    }
}
