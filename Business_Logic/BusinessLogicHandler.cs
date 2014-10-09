using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using urbanbooks;

namespace urbanbooks
{
    public class BusinessLogicHandler
    {

        #region COMMON ACTIONS

        public bool CheckIsBookSupplier(int SupplierID)
        { SupplierHandler myHandler = new SupplierHandler(); return myHandler.IsBookSupplier(SupplierID); }
        public CartItem CheckIfExist(int CartID, int ProductID)
        { CartItemHandler myHandler = new CartItemHandler(); return myHandler.CheckProductDuplicates(CartID, ProductID); }
        public List<InvoiceItem> Sales()
        { InvoiceItemHandler myHandler = new InvoiceItemHandler(); return myHandler.InvoiceItems(); }
        public List<Book> GetBooksByPublisher(int PublisherId)
        { BookHandler myHandler = new BookHandler(); return myHandler.GetPublisherBooks(PublisherId); }
        public List<Publisher> PublisherGlobalSearch(string query)
        { PublisherHandler myhandler = new PublisherHandler(); return myhandler.PublisherGlobalSearch(query); }
        public List<Publisher> GetPublishers()
        { PublisherHandler myHandler = new PublisherHandler(); return myHandler.GetPublisherList(); }
        public List<Book>  GetBooksByAuthor(int AuthorID)
        { BookHandler myHandler = new BookHandler(); return myHandler.BooksByAuthor(AuthorID); }
        public List<Author> GetAuthorsPerBook(int BookID)
        { AuthorHandler myHandler = new AuthorHandler(); return myHandler.GetAuthorsPerBook(BookID); }
        public List<Technology> DevicesByCategory(int CategoryID)
        { TechnologyHandler myHandler = new TechnologyHandler(); return myHandler.TechnologyByCategory(CategoryID); }
        public List<BookAuthor> GetBookAuthors(int BookID)
        { BookAuthorHandler myHandler = new BookAuthorHandler(); return myHandler.GetBookAuthors(BookID); }
        public List<Book> GetBooksByCategory(int CategoryID)
        { BookHandler myHandler = new BookHandler(); return myHandler.BooksByCategory(CategoryID); }
        public List<Technology> ModelNameBETWEENQueryDeviceSearch(string query, double FromPrice, double ToPrice)
        { TechnologyHandler myHandler = new TechnologyHandler(); return myHandler.DeviceModelNameBETWEENQuerySearch(query, FromPrice, ToPrice); }
        public List<Technology> ModelNumberBETWEENQueryDeviceSearch(string query, double FromPrice, double ToPrice)
        { TechnologyHandler myHandler = new TechnologyHandler(); return myHandler.DeviceModelNumberBETWEENQuerySearch(query, FromPrice, ToPrice); }
        public List<Technology> ManufacturerBETWEENQueryDeviceSearch(string query, double FromPrice, double ToPrice)
        { TechnologyHandler myHandler = new TechnologyHandler(); return myHandler.DeviceManufacturerBETWEENQuerySearch(query, FromPrice, ToPrice); }
        public List<Technology> CategoryBETWEENQueryDeviceSearch(string query, double FromPrice, double ToPrice)
        { TechnologyHandler myHandler = new TechnologyHandler(); return myHandler.DeviceCategoryBETWEENQuerySearch(query, FromPrice, ToPrice); }
        public List<Technology> CategoryToQueryDeviceSearch(string query, double ToPrice)
        { TechnologyHandler myHandler = new TechnologyHandler(); return myHandler.DeviceCategoryToQuerySearch(query, ToPrice); }
        public List<Technology> ManufacturerToQueryDeviceSearch(string query, double ToPrice)
        { TechnologyHandler myHandler = new TechnologyHandler(); return myHandler.DeviceManufacturerToQuerySearch(query, ToPrice); }
        public List<Technology> ModelNumberToQueryDeviceSearch(string query, double ToPrice)
        { TechnologyHandler myHandler = new TechnologyHandler(); return myHandler.DeviceModelNumberToQuerySearch(query, ToPrice); }
        public List<Technology> ModelNameToQueryDeviceSearch(string query, double ToPrice)
        { TechnologyHandler myHandler = new TechnologyHandler(); return myHandler.DeviceModelNameToQuerySearch(query, ToPrice); }
        public List<Technology> ModelNameFromQueryDeviceSearch(string query, double FromPrice)
        { TechnologyHandler myHandler = new TechnologyHandler(); return myHandler.DeviceModelNameFromQuerySearch(query, FromPrice); }
        public List<Technology> ModelNumberFromQueryDeviceSearch(string query, double FromPrice)
        { TechnologyHandler myHandler = new TechnologyHandler(); return myHandler.DeviceModelNumberFromQuerySearch(query, FromPrice); }
        public List<Technology> ManufacturerFromQueryDeviceSearch(string query, double FromPrice)
        { TechnologyHandler myHandler = new TechnologyHandler(); return myHandler.DeviceManufacturerFromQuerySearch(query, FromPrice); }
        public List<Technology> CategoryFromQueryDeviceSeach(string query, double FromPrice)
        { TechnologyHandler myHandler = new TechnologyHandler(); return myHandler.DeviceCategoryFromQuerySearch(query, FromPrice); }
        public List<Technology> CategoryDeviceSearch(string query)
        { TechnologyHandler myHandler = new TechnologyHandler(); return myHandler.DeviceCategorySearch(query); }
        public List<Technology> ManufacturerDeviceSearch(string query)
        { TechnologyHandler myHandler = new TechnologyHandler(); return myHandler.ManufacturerDeviceSearch(query); }
        public List<Technology> ModelNumberSearch(string query)
        { TechnologyHandler myHandler = new TechnologyHandler(); return myHandler.ModelNumberDeviceSearch(query); }
        public List<Technology> ModelNameDeviceSearch(string query)
        { TechnologyHandler myHandler = new TechnologyHandler(); return myHandler.ModelNameDeviceSearch(query); }
        public List<Book> CategoryBETWEENQueryBookSeach(string query, double FromPrice, double ToPrice)
        { BookHandler myHandler = new BookHandler(); return myHandler.CategoryBETWEENQueryBookSearch(query, ToPrice, FromPrice); }
        public List<Book> ISBNBETWEENQueryBookSearch(string query, double FromPrice, double ToPrice)
        { BookHandler myHandler = new BookHandler(); return myHandler.ISBNBETWEENQueryBookSearch(query, ToPrice, FromPrice); }
        public List<Book> PublisherBETWEENQueryBookSearch(string query, double FromPrice, double ToPrice)
        { BookHandler myHandler = new BookHandler(); return myHandler.PublisherBETWEENQueryBookSearch(query, ToPrice, FromPrice); }
        public List<Book> BookTitleBETWEENQueryBookSearch(string query, double FromPrice, double ToPrice)
        { BookHandler myHandler = new BookHandler(); return myHandler.BookTitleBETWEENQueryBookSearch(query, ToPrice, FromPrice); }
        public List<Book> AuthorBETWEENQueryBookSearch(string query, double FromPrice, double ToPrice)
        { BookHandler myhandler = new BookHandler(); return myhandler.AuthorBETWEENQueryBookSearch(query, ToPrice, FromPrice); }
        public List<Book> AuthorToQueryBookSearch(string query, double ToPrice)
        { BookHandler myHandler = new BookHandler(); return myHandler.AuthorToQueryBookSearch(query, ToPrice); }
        public List<Book> CategoryToQueryBookSearch(string query, double ToPrice)
        { BookHandler myHandler = new BookHandler(); return myHandler.CategoryToQueryBookSearch(query, ToPrice); }
        public List<Book> BookTitleToQueryBookSearch(string query, double ToPrice)
        { BookHandler myHandler = new BookHandler(); return myHandler.BookTitleToQueryBookSearch(query, ToPrice); }
        public List<Book> PublisherToQueryBookSearch(string query, double ToPrice)
        { BookHandler myHandler = new BookHandler(); return myHandler.PublisherToQueryBookSearch(query, ToPrice); }
        public List<Book> ISBNToQueryBookSearch(string query, double ToPrice)
        { BookHandler myHandler = new BookHandler(); return myHandler.ISBNToQueryBookSearch(query, ToPrice); }
        public List<Book> PublisherFromQueryBookSearch(string query, double FromPrice)
        { BookHandler myHandler = new BookHandler(); return myHandler.PublisherFromQueryBookSearch(query, FromPrice); }
        public List<Book> CategoryFromQueryBookSearch(string query, double FromPrice)
        { BookHandler myHandler = new BookHandler(); return myHandler.CategoryFromQueryBookSearch(query, FromPrice); }
        public List<Book> AuthorFromQueryBookSearch(string query, double FromPrice)
        { BookHandler myHandler = new BookHandler(); return myHandler.AuthorFromQueryBookSearch(query, FromPrice); }
        public List<Book> ISBNFromQueryBookSearch(string query, double FromPrice)
        { BookHandler myHandler = new BookHandler(); return myHandler.ISBNFromQueryBookSearch(query, FromPrice); }
        public List<Book> BookTitleFromQueryBookSearch(string query, double FromPrice)
        { BookHandler myHandler = new BookHandler(); return myHandler.BookTitleFromQueryBookSearch(query, FromPrice); }
        public List<Book> CategoryBookSearch(string query)
        { BookHandler myHandler = new BookHandler(); return myHandler.CategoryBookSearch(query); }
        public List<Book> AuthorBookSearch (string query)
        { BookHandler myHandler = new BookHandler(); return myHandler.AuthorBookSearch(query); }
        public List<Book> PublisherBookSearch(string query)
        { BookHandler myHandler = new BookHandler(); return myHandler.PublisherBookSearch(query); }
        public List<Book> ISBNBookSearch(string query)
        { BookHandler myHandler = new BookHandler(); return myHandler.ISBNBookSearch(query); }
        public List<Book> BookTitleBookSearch(string query)
        { BookHandler myHandler = new BookHandler(); return myHandler.BookTitleBookSearch(query); }
        public List<TechCategory> DeviceGlobalSearch(string query)
        { TechCategoryHandler myHandler = new TechCategoryHandler(); return myHandler.TechnologyCategoryGloablSearch(query); }
        public List<Technology> TechnologyGlobalSearch(string query)
        { TechnologyHandler myHandler = new TechnologyHandler(); return myHandler.TechnologyGlobalSearch(query); }
        public List<Author> AuthorGlobalSearch(string query)
        { AuthorHandler myHandler = new AuthorHandler(); return myHandler.AuthorGlobalSearch(query); }
        public List<BookCategory> BookCategoryGlobalSearch(string query)
        { BookCategoryHandler myHandler = new BookCategoryHandler(); return myHandler.BookCategoryGlobalSearch(query); }
        public List<Book> BookGlobalSearch(string query)
        { BookHandler myHandler = new BookHandler(); return myHandler.GloabalSearch(query); }
        public List<Manufacturer> GetManufacturers()
        { ManufacturerHandler myHandler = new ManufacturerHandler(); return myHandler.GetManufacturerList(); }
        public List<Supplier> GetBookSuppliers()
        { SupplierHandler myHandler = new SupplierHandler(); return myHandler.GetBookSupplierList(); }    //ADMIN & SYSTEM
        public List<Supplier> GetTechSuppliers()
        { SupplierHandler myHandler = new SupplierHandler(); return myHandler.GetTechSupplierList(); }
        public List<Author> GetAuthors()
        { AuthorHandler myHandler = new AuthorHandler(); return myHandler.GetAuthorList(); } //ADMIN &SYSTEM
        public Company GetCompanyDetail()
        { CompanyHandler myHandler = new CompanyHandler(); return myHandler.GetCompanyDetail(); }
        public List<Company> GetCompanyDetails()
        { CompanyHandler myHandler = new CompanyHandler(); return myHandler.CompanyDetails(); }

        public BookCategory GetBookType(int BookCategoryID)
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

        public List<InvoiceItem> GetInvoiceItems(int InvoiceID)
        { InvoiceItemHandler myHandler = new InvoiceItemHandler(); return myHandler.GetInvoiceItemList(InvoiceID); } //SYSTEM & ADMIN! USER

        public List<OrderItem> GetOrderItemsList(int orderNo)
        { OrderItemHandler myHandler = new OrderItemHandler(); return myHandler.GetOrderItemList(orderNo); } //SYSTEM & ADMIN

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

        #region COMPANY

        public bool UpdateCompany(Company company)
        { CompanyHandler myHandler = new CompanyHandler(); return myHandler.UpdateCompany(company); }


        #endregion

        #region BOOK

        public Book GetBook(int ProductID)
        {
            BookHandler myHandler = new BookHandler();
            return myHandler.GetBookDetails(ProductID);
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
        public bool RestoreBook(Book book)
        {
            BookHandler myHandler = new BookHandler();
            return myHandler.RestoreBook(book);
        }
        public bool UpdateBook(Book book)
        {
            BookHandler myHandler = new BookHandler();
            return myHandler.UpdateBook(book);
        }
        public bool UpdateBookProduct(Book book)
        {
            BookHandler myHandler = new BookHandler();
            Book bk = new Book();

            return myHandler.UpdateBookProduct(book);
        }
        public bool DeleteBook(Book book)
        {
            BookHandler myHandler = new BookHandler();
            return myHandler.DeleteBook(book);
        }
        public BookAuthor TrialInsertBook(Book book)
        { BookHandler myHandler = new BookHandler(); return myHandler.TrailInsertBook(book); }

        #endregion

        #region Book Author

        public bool InsertBookAuthor(BookAuthor bookAuthor)
        { BookAuthorHandler myHandler = new BookAuthorHandler(); return myHandler.InsertBookAuthor(bookAuthor); }

        public bool DeleteBookAuthor(BookAuthor bookAuthor)
        { BookAuthorHandler myHandler = new BookAuthorHandler(); return myHandler.DeleteBookAuthor(bookAuthor); }

        #endregion

        #region TECHNOLOGY

        public bool UpdateTechProduct(Technology gadget)
        { TechnologyHandler myHandler = new TechnologyHandler(); return myHandler.UpdateTechnologyProduct(gadget); }
        public bool AddTechnology(Technology gadget)
        {
            TechnologyHandler myHandler = new TechnologyHandler();
            return myHandler.InsertTechnology(gadget);
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
            return myHandler.UpdateTechnology(gadget);
        }

        public bool UpdateTechnologyProduct(Technology gadget)
        {
            TechnologyHandler myHandler = new TechnologyHandler();
            Technology tech = new Technology();

            return myHandler.UpdateTechnologyProduct(gadget);
        }

        public bool DeleteTechnology(Technology tech)
        { 
            TechnologyHandler myHandler = new TechnologyHandler(); 
            return myHandler.DeleteTechnology(tech); 
        }
        public bool RestoreDevice(Technology tech)
        {
            TechnologyHandler myHandler = new TechnologyHandler();
            return myHandler.RestoreDevice(tech);
        }

        public Technology GetTechnologyDetails(int ProductID)
        { TechnologyHandler myHandler = new TechnologyHandler(); return myHandler.GetTechnologyDetails(ProductID); }


        #endregion

        #region SUPPLIER
        public Supplier MyProperty { get; set; }
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

        #region PUBLISHER

        public Publisher GetPublisher(int PublisherID)
        { PublisherHandler myHandler = new PublisherHandler(); return myHandler.GetPublisherDeatils(PublisherID); }

        public bool UpdatePublisher(Publisher publisher)
        { PublisherHandler myHandler = new PublisherHandler(); return myHandler.UpdatePublisher(publisher); }

        public bool AddPublisher(Publisher publisher)
        { PublisherHandler myHandler = new PublisherHandler(); return myHandler.InsertPublisher(publisher); }

        public bool DeletePublisher(int PublisherID)
        { PublisherHandler myHandler = new PublisherHandler(); return myHandler.DeletePublisher(PublisherID); }
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

        public List<Order> GetAllOrdersForInvoice(int InvoiceID)
        { OrderHandler myHandler = new OrderHandler(); return myHandler.GetOrdersForInvoice(InvoiceID); }

        public OrderItem AddOrder(Order order)
        { OrderHandler myHandler = new OrderHandler(); return myHandler.CreateOrder(order); }

        public List<Order> GetAllCompletedOrders()
        { OrderHandler myHandler = new OrderHandler(); return myHandler.GetAllCompletedOrders(); }

        public List<Order> GetAllPendingOrders()
        { OrderHandler myHandler = new OrderHandler(); return myHandler.GetAllPendingOrders(); }

        public Order GetOrder(int orderNo)
        { OrderHandler myHandler = new OrderHandler(); return myHandler.GetOrder(orderNo); }

        public bool DeleteOrder(int orderNo)
        { OrderHandler myHandler = new OrderHandler(); return myHandler.DeleteOrder(orderNo); }

        #endregion

        #region Manufacturer

        public Manufacturer GetManufacturer(int ManufacturerID)
        { ManufacturerHandler myHandler = new ManufacturerHandler(); return myHandler.GetManufacturerDetails(ManufacturerID); }

        public bool AddManufacturer(Manufacturer manufacturer)
        { ManufacturerHandler myHandler = new ManufacturerHandler(); return myHandler.InsertManufacturer(manufacturer); }

        public bool UpdateManufacturer(Manufacturer manufacturer)
        { ManufacturerHandler myHandler = new ManufacturerHandler(); return myHandler.UpdateManufacturer(manufacturer); }

        public List<Manufacturer> ManufacturerGlobalSearch(string query)
        { ManufacturerHandler myHandler = new ManufacturerHandler(); return myHandler.ManufacturerGlobalSearch(query); }
        public bool DeleteManufacturer(int ManufacturerID)
        { ManufacturerHandler myHandler = new ManufacturerHandler(); return myHandler.DeleteManufacturer(ManufacturerID); }

        #endregion


        #region KEYWORD

        public bool AddKeyword(Keyword key)
        { KeywordHandler myHandler = new KeywordHandler(); return myHandler.InsertKeyword(key); }

        public bool DeleteKeyword(int KeywordID)
        { KeywordHandler myHandler = new KeywordHandler(); return myHandler.DeleteKeyword(KeywordID); }

        #endregion

        #region ORDERITEM

        public bool UpdateOrderItem(OrderItem item)
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

        public TechCategory GetTechCategory(int TechCategoryID)
        { TechCategoryHandler myHandler = new TechCategoryHandler(); return myHandler.GetTechCategoryDetails(TechCategoryID); }
        #endregion
        #region BOOKTYPE

        public bool AddBookType(BookCategory bookType)
        {
            BookCategoryHandler myHander = new BookCategoryHandler();
            return myHander.InsertBookCategory(bookType);
        }
        public BookCategory GetBookCategory(int CategoryID)
        { BookCategoryHandler myHandler = new BookCategoryHandler(); return myHandler.GetBookCategory(CategoryID); }
        public bool UpdateBookType(BookCategory bookType)
        { BookCategoryHandler myHandler = new BookCategoryHandler(); return myHandler.UpdateBookCategory(bookType); }

        public bool DeleteBookType(int BookCategoryID)
        { BookCategoryHandler myHandler = new BookCategoryHandler(); return myHandler.DeleteBookCategory(BookCategoryID); }

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
        public List<BookCategory> CheckDuplicatedBookCategory(string category)
        { BookCategoryHandler myHandler = new BookCategoryHandler(); return myHandler.CheckDuplicateBookCategory(category); }
        public List<Technology> CheckDuplicatedDevice(string modelNumber)
        { TechnologyHandler myHandler = new TechnologyHandler(); return myHandler.CheckDuplicateDevice(modelNumber); }
        public List<Publisher> CheckDuplicatedPublisher(string name)
        { PublisherHandler myHandler = new PublisherHandler(); return myHandler.CheckDuplicatePublisher(name); }
        public List<Book> CheckDuplicatedBook(string isbn)
        { BookHandler myHandler = new BookHandler(); return myHandler.CheckDuplicateBook(isbn); }
        public List<Author> CheckDuplicatedAuthor(string name, string surname)
        { AuthorHandler myHandler = new AuthorHandler(); return myHandler.CheckDuplicateAuthor(name, surname); }
        public List<InvoiceItem> SalesGroupedByInvoiceID(DateTime dateFrom, DateTime dateTo)
        { InvoiceItemHandler myHandler = new InvoiceItemHandler(); return myHandler.InvoiceItemsGroupedByInvoiceID(dateFrom, dateTo); }
        public List<Invoice> GetInvoiceInDateRange(string startDate, string endDate)
        { InvoiceHandler myHandler = new InvoiceHandler(); return myHandler.GetInvoicesInRange(startDate, endDate); }
        public bool BookAuthorUpdateDelete(int BookID)
        { BookAuthorHandler myHandler = new BookAuthorHandler(); return myHandler.DeleteUpdateBookAuthor(BookID); }
        public bool CheckProductType(int ProductId)
        { BookHandler myHandler = new BookHandler(); return myHandler.CheckProductStatus(ProductId); }
        public List<Technology> GetNewDevices()
        { TechnologyHandler myHandler = new TechnologyHandler(); return myHandler.GetNewDeviceList(); }
        public List<Book> GetNewBooks()
        { BookHandler myHandler = new BookHandler(); return myHandler.GetNewBookList(); }
        public List<Order> GetOrderByRange(string from, string to, int SupplierID)
        { OrderHandler myHandler = new OrderHandler(); return myHandler.GetOrdersInRasnge(from, to, SupplierID); }

        public List<Book> GetBooks()
        {
            BookHandler myHandler = new BookHandler();
            return myHandler.GetBookList();
        }

        public List<Book> GetDeletedBooks()
        {
            BookHandler myHandler = new BookHandler();
            return myHandler.GetDeletedBooks();
        }
        public List<Technology> GetDeletedDevices()
        {
            TechnologyHandler myHandler = new TechnologyHandler();
            return myHandler.GetDeletedDevices();
        }

        public bool AssignOrderToSupplier(Order ord)
        { OrderHandler myHandler = new OrderHandler(); return myHandler.AssignSupplierToOrder(ord); }

        public InvoiceItem GetInvoiceLastNumber(Invoice reciept)
        { InvoiceHandler myHandler = new InvoiceHandler(); return myHandler.GetInvoiceNumber(reciept); }

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

        #region SUPPLIER ACTIONS

        public List<Order> GetSupplierOrders(int SupplierID)
        { OrderHandler myHandler = new OrderHandler(); return myHandler.GetOrdersForSupplier(SupplierID); }

        public Supplier GetSupplier(string User_Id)
        { SupplierHandler myHandler = new SupplierHandler(); return myHandler.GetSupplier(User_Id); }

        #endregion
    }
}
