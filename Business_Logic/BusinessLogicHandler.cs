using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace urbanbooks
{
    public class BusinessLogicHandler
    {

        #region COMMON ACTIONS

        public List<Supplier> GetSuppliers()
        { SupplierHandler myHandler = new SupplierHandler(); return myHandler.GetSupplierList(); }    //ADMIN & SYSTEM

        public List<Author> GetAuthors()
        { AuthorHandler myHandler = new AuthorHandler(); return myHandler.GetAuthorList(); } //ADMIN &SYSTEM

        public List<Company> GetCompanyDetails()
        { CompanyHandler myHandler = new CompanyHandler(); return myHandler.CompanyDetails(); }

        public BookCategory GetBookCategory(int BookCategoryID)
        { BookCategoryHandler myhandler = new BookCategoryHandler(); return myhandler.GetBookCategory(BookCategoryID); } //ADMIN, USER & SYSTEM

        public List<BookCategory> GetBookCategoryList()
        { BookCategoryHandler myhandler = new BookCategoryHandler(); return myhandler.GetBookCategoryList(); }

        public Special GetSpecialDetails(int SpecialID)
        { SpecialHandler myHandler = new SpecialHandler(); return myHandler.GetSpecialDetails(SpecialID); } //ADMIN, USER, SYSTEM

        public List<Special> GetSpecialsList()
        { SpecialHandler myHandler = new SpecialHandler(); return myHandler.GetSpecialsList(); } //ADMIN, USER & SYSTEM

        public bool DeleteCartItem(int CartItemID)
        { CartItemHandler myHandler = new CartItemHandler(); return myHandler.DeleteCartItem(CartItemID); } //SYSTEM & USER

        public bool DeleteWishlistItem(int WishlistItemID)
        { WishlistItemHandler myHandler = new WishlistItemHandler(); return myHandler.DeleteWishlistItem(WishlistItemID); } // SYSTEM & USER

        public Invoice GetInvoice(int InvoiceID)
        { InvoiceHandler myHandler = new InvoiceHandler(); return myHandler.GetInvoice(InvoiceID); } //USER & SYSTEM

        public bool DeleteInvoice(int InvoiceID)
        { InvoiceHandler myHandler = new InvoiceHandler(); return myHandler.DeleteInvoice(InvoiceID); } //USER

        public List<InvoiceItem> GetInvoiceItems()
        { InvoiceItemHandler myHandler = new InvoiceItemHandler(); return myHandler.GetInvoiceItemList(); } //SYSTEM & ADMIN! USER

        public List<OrderItem> GetOrderItemsList()
        { OrderItemHandler myHandler = new OrderItemHandler(); return myHandler.GetOrderItemList(); } //SYSTEM & ADMIN

        public bool DeleteOrderItem(int orderItemID)
        { OrderItemHandler myHandler = new OrderItemHandler(); return myHandler.DeleteOrderItem(orderItemID); } //SYSTEM & ADMIN

        public List<TechCategory> GetTechnologyTypeList()
        { TechCategoryHandler myHandler = new TechCategoryHandler(); return myHandler.GetTechCategoryList(); } //SYSTEM, ADMIN & USER 

        public TechCategory GetTechnologyType(int TechnologyTypeID)
        { TechCategoryHandler myHandler = new TechCategoryHandler(); return myHandler.GetTechCategoryDetails(TechnologyTypeID); } // SYSTEM & ADMIN

        public Delivery GetDeliveryDetails(int deliveryServiceID)
        { DeliveryHandler myHandler = new DeliveryHandler(); return myHandler.GetDeliveryDetails(deliveryServiceID); } // ADMIN & USER

        public List<Delivery> GetDeliveryServiceList()
        { DeliveryHandler myHandler = new DeliveryHandler(); return myHandler.GetDeliveryList(); }

        public List<Order> GetOrdersList()
        { OrderHandler myHandler = new OrderHandler(); return myHandler.GetOrdersList(); }

        #endregion


        #region USER ACTIONS

        public Book User_GetBook(int ProductID)
        { BookHandler myHandler = new BookHandler(); return myHandler.UGetBookDetails(ProductID); }

        public Technology User_GetTechnology(int ProductID)
        { TechnologyHandler myHandler = new TechnologyHandler(); return myHandler.UGetTechnologyDetails(ProductID); }

        public bool AddCartItem(CartItem item)
        { CartItemHandler myHandler = new CartItemHandler(); return myHandler.InsertCartItem(item); }

        public bool UpdateCartItem(CartItem item)
        { CartItemHandler myHandler = new CartItemHandler(); return myHandler.UpdateCartItem(item); }

        public bool AddWishlistItem(WishlistItem wish)
        { WishlistItemHandler myHandler = new WishlistItemHandler(); return myHandler.InsertWishlistItem(wish); }
        

        #endregion


        #region ADMIN ACTIONS

            #region BOOK
        public Book GetBook(int BookID)
        {
            BookHandler myHandler = new BookHandler();
            return myHandler.GetBookDetails(BookID);
        }

        public bool AddBook(Book book)
        {
            BookHandler myHandler = new BookHandler();
            return myHandler.InsertBook(book);
        }
        public Book AddExperimentBook(Book book)
        {
            BookHandler myHandler = new BookHandler();
            Book b = new Book();

            b = myHandler.experimentalBook(book);

            return b;
        }

        public bool UpdateBook(Book book)
        {
            bool mybinder = false;
            BookHandler myHandler = new BookHandler();
            if (myHandler.UpdateBookProduct(book) == true)
            { mybinder = true; }
            else if (myHandler.UpdateBook(book) == false)
            { mybinder = false; }
            return mybinder;
        }

        public bool DeleteBook(int BookID)
        { BookHandler myHandler = new BookHandler(); 
          return myHandler.DeleteBookProduct(BookID); }
        #endregion

            #region TECHNOLOGY

        public bool AddTechnology(Technology gadget)
        { 
            TechnologyHandler myHandler = new TechnologyHandler();
            bool myBinder = false;

            if (myHandler.InsertTechnology(gadget) == true && myHandler.InsertTechnologyProduct(gadget) == true)
            { return myBinder = true; }
            else
                return myBinder;
        }
        public Technology AddExperimentTech(Technology tech)
        {
            TechnologyHandler myHandler = new TechnologyHandler();
            Technology t = new Technology();

            t = myHandler.experimentalTech(tech);

            return t;
        }
        public bool UpdateTechnology(Technology gadget)
        {
            TechnologyHandler myHandler = new TechnologyHandler();
            bool myBinder = false;

            if (myHandler.UpdateTechnologyProduct(gadget) == true && myHandler.UpdateTechnology(gadget)==true)
            { return myBinder = true; }
            else
                return myBinder;
        }

        public bool DeleteTechnology(int ProductID)
        { TechnologyHandler myHandler = new TechnologyHandler(); return myHandler.DeleteTechnologyProduct(ProductID); }

        public Technology GetTechnologyDetails(int ProductID)
        { TechnologyHandler myHandler = new TechnologyHandler(); return myHandler.GetTechnologyDetails(ProductID); }

        #endregion 

            #region SUPPLIER

        public bool AddSupplier(Supplier supplier)
        {
            SupplierHandler myHandler = new SupplierHandler();
            return myHandler.InsertSupplier(supplier);
        }

        public bool DeleteSupplier(int SupplierID)
        { SupplierHandler myHandler = new SupplierHandler(); return myHandler.DeleteSupplierProduct(SupplierID); }

        public bool UpdateSupplier(Supplier supplier)
        { SupplierHandler myHandler = new SupplierHandler(); return myHandler.UpdateSupplier(supplier); }

        public Supplier GetSupplier(int SupplierID)
        { SupplierHandler myHandler = new SupplierHandler(); return myHandler.GetSupplierDetails(SupplierID); }

        #endregion

            #region AUTHOR

        public bool AddAuthor(Author author)
        {
            AuthorHandler myHandler = new AuthorHandler();
            return myHandler.InsertAthor(author);
        }

        public bool DeleteAuthor(int AuthorID)
        { AuthorHandler myHandler = new AuthorHandler(); return myHandler.DeleteAuthor(AuthorID); }

        public bool UpdateAuthor(Author author)
        { AuthorHandler myHandler = new AuthorHandler(); return myHandler.UpdateAuthor(author); }

        public Author GetAuthorDetails(int AuthorID)
        { AuthorHandler myHandler = new AuthorHandler(); return myHandler.GetAuthorDetails(AuthorID); }

        #endregion

            #region BOOKTYPE

        public bool AddBookType(BookCategory bookType)
        {
            BookCategoryHandler myHander = new BookCategoryHandler();
            return myHander.InsertBookCategory(bookType);
        }

        public bool UpdateBookType(BookCategory bookType)
        { BookCategoryHandler myHandler = new BookCategoryHandler(); return myHandler.UpdateBookCategory(bookType); }

        public bool DeleteBookType(int BookCategoryID)
        { BookCategoryHandler myHandler = new BookCategoryHandler(); return myHandler.DeleteBookCategory(BookCategoryID); }

        #endregion

            #region SPECIAL

        public bool AddSpecial(Special special)
        { SpecialHandler myHandler = new SpecialHandler(); return myHandler.InsertSpecial(special); }

        public bool UpdateSpecial(Special special)
        { SpecialHandler myHandler = new SpecialHandler(); return myHandler.UpdateSpecial(special); }

        public bool DeleteSpecial(int SpecialID)
        { SpecialHandler myHandler = new SpecialHandler(); return myHandler.DeleteSpecial(SpecialID); }

        #endregion

            #region ORDER

        public bool AddOrder(Order order)
        { OrderHandler myHandler = new OrderHandler(); return myHandler.CreateOrder(order); }

        public Order GetOrder (int OrderNumber)
        { OrderHandler myHandler = new OrderHandler(); return myHandler.GetOrder(OrderNumber); }

        public bool DeleteOrder(int orderNumber)
        { OrderHandler myHandler = new OrderHandler(); return myHandler.DeleteOrder(orderNumber); }

        #endregion

            #region KEYWORD

        public bool AddKeyword(Keyword key)
        { KeywordHandler myHandler = new KeywordHandler(); return myHandler.InsertKeyword(key); }

        public bool DeleteKeyword(int KeywordID)
        { KeywordHandler myHandler = new KeywordHandler(); return myHandler.DeleteKeyword(KeywordID); }

        #endregion

            #region ORDERITEM

        public bool UpdateOrderItem (OrderItem item)
        { OrderItemHandler myHandler = new OrderItemHandler(); return myHandler.UpdateOrderItem(item); }

        public bool AddOrderItem(OrderItem item)
        { OrderItemHandler myHandler = new OrderItemHandler(); return myHandler.InsertOrderItem(item); }

        #endregion

            #region TECHNOLOGYTYPE

        public bool AddTechnologyType(TechCategory type)
        { TechCategoryHandler myHandler = new TechCategoryHandler(); return myHandler.InsertTechCategory(type); }

        public bool DeleteTechnologyType(int TechnologyTypeID)
        { TechCategoryHandler myHandler = new TechCategoryHandler(); return myHandler.DeleteTechCategory(TechnologyTypeID); }

        public bool UpdateTechnologyType(TechCategory type)
        { TechCategoryHandler myHandler = new TechCategoryHandler(); return myHandler.UpdateTechCategory(type); }

        #endregion

            #region DELIVERYSERVICE

        public bool AddDeliveryService(Delivery deliveryService)
        { DeliveryHandler myHandler = new DeliveryHandler(); return myHandler.InsertDelivery(deliveryService); }

        public bool DeleteDeliveryService(int deliveryServiceID)
        { DeliveryHandler myhandler = new DeliveryHandler(); return myhandler.DeleteDelivery(deliveryServiceID); }

        public bool UpdateDeliveryServiceID(Delivery delivery)
        { DeliveryHandler myHandler = new DeliveryHandler(); return myHandler.UpdateDelivery(delivery); }

        #endregion


        #endregion


        #region SYSTEM ACTIONS

        public List<Book> GetBooks()
        {
            BookHandler myHandler = new BookHandler();
            return myHandler.GetBookList();
        }

        public List<Technology> GetTechnology()
        { TechnologyHandler myHandler = new TechnologyHandler(); return myHandler.GetTechnologyList(); }

        //public bool CreateCart( Cart cart)
        //{ CartHandler myHandler = new CartHandler(); return myHandler.CreateCart( cart); }

        //public Cart GetCart (int CartID)
        //{CartHandler myHandler = new CartHandler(); return myHandler.GetCart(CartID);}

        //public bool UpdateCart(Cart cart)
        //{ CartHandler myHandler = new CartHandler(); return myHandler.UpdateCart(cart); }

        public List<CartItem> GetCartItems(int CartID)
        { CartItemHandler myHandler = new CartItemHandler(); return myHandler.GetCartItemList(CartID); }

        //public bool CreateWishlist(string UserID, Wishlist wish)
        //{ WishlistHandler myHandler = new WishlistHandler(); return myHandler.CreateWishlist(UserID, wish); }

        //public Wishlist GetWishlist(string WishlistID)
        //{ WishlistHandler myHandler = new WishlistHandler(); return myHandler.GetWishlist(WishlistID); }

        public List<WishlistItem> GetWishlistItems(int wishlistId)
        { WishlistItemHandler myHandler = new WishlistItemHandler(); return myHandler.GetWishlistItemList(wishlistId); }

        //public bool UpdateWishlist(Wishlist list)
        //{ WishlistHandler myHandler = new WishlistHandler(); return myHandler.UpdateWishlist(list); }

        public List<Keyword> GetKeywords()
        { KeywordHandler myHandler = new KeywordHandler(); return myHandler.GetKeywordsList(); }

        public bool CreateInvoice(Invoice invoice)
        { InvoiceHandler myHandler = new InvoiceHandler(); return myHandler.CreateInvoice(invoice); }

        public bool AddinvoiceItem(InvoiceItem item)
        { InvoiceItemHandler myHandler = new InvoiceItemHandler(); return myHandler.InsertInvoiceItem(item); }

        public bool DeleteInvoiceItem(int InvoiceItemID)
        { InvoiceItemHandler myHandler = new InvoiceItemHandler(); return myHandler.DeleteInvoiceItem(InvoiceItemID); }

        public bool UpdateOrder(Order order)
        { OrderHandler myHandler = new OrderHandler(); return myHandler.UpdateOrder(order); }

        #endregion
    }
}
