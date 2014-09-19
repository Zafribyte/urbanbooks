using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace urbanbooks
{
    public class BookHandler
    {
        public List<Book> GetBookList()
        {
            List<Book> BookList = null;

            using (DataTable table = DataProvider.ExecuteSelectCommand("sp_ViewAllBooksUser",
                CommandType.StoredProcedure))
            {
                if (table.Rows.Count > 0)  
                {
                    BookList = new List<Book>();
                    foreach (DataRow row in table.Rows)
                    {
                        Book book = new Book();
                        BookCategory bc = new BookCategory();
                        book.BookID = (int)row["BookID"];
                        book.ProductID = (int)row["ProductID"];
                        book.BookTitle = row["BookTitle"].ToString();
                        book.Synopsis = row["Synopsis"].ToString();
                        book.SupplierID = Convert.ToInt32(row["SupplierID"].ToString());
                        book.ISBN = row["ISBN"].ToString();
                        book.CostPrice = Convert.ToDouble(row["CostPrice"]);
                        book.SellingPrice = Convert.ToDouble(row["SellingPrice"]);
                        book.BookCategoryID = (int)row["BookCategoryID"];
                        book.CoverImage = row["CoverImage"].ToString();
                        BookList.Add(book);


                    }
                }
            }
            return BookList;
        }

        public List<Book> BooksByCategory(int CategoryID)
        {
            List<Book> BookList = null;

            SqlParameter[] Params = { new SqlParameter("@CategoryID", CategoryID) };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_ViewBooksByCategory", CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count > 0)
                {
                    BookList = new List<Book>();
                    foreach (DataRow row in table.Rows)
                    {
                        Book book = new Book();
                        book.BookID = (int)row["BookID"];
                        book.ProductID = (int)row["ProductID"];
                        book.BookTitle = row["BookTitle"].ToString();
                        book.ISBN = row["ISBN"].ToString();
                        book.SellingPrice = Convert.ToDouble(row["SellingPrice"]);
                        book.BookCategoryID = (int)row["BookCategoryID"];
                        book.CoverImage = row["CoverImage"].ToString();
                        book.PublisherID = (int)row["PublisherID"];
                        BookList.Add(book);


                    }
                }
            }
            return BookList;
        }

        public List<Book> BooksByAuthor(int AuthorID)
        {
            List<Book> BookList = null;

            SqlParameter[] Params = { new SqlParameter("@AuthorID", AuthorID) };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_ViewAllBooksByAuthor", CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count > 0)
                {
                    BookList = new List<Book>();
                    foreach (DataRow row in table.Rows)
                    {
                        Book book = new Book();
                        book.BookID = (int)row["BookID"];
                        book.ProductID = (int)row["ProductID"];
                        book.BookTitle = row["BookTitle"].ToString();
                        book.ISBN = row["ISBN"].ToString();
                        book.SellingPrice = Convert.ToDouble(row["SellingPrice"]);
                        book.BookCategoryID = (int)row["BookCategoryID"];
                        book.CoverImage = row["CoverImage"].ToString();
                        BookList.Add(book);


                    }
                }
            }
            return BookList;
        }

        #region SEARCH
        public List<Book> GloabalSearch(string query)
        {
            List<Book> BookList = null;

            SqlParameter[] Params = { new SqlParameter("@Search", query) };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_BookGloabalSearch", CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count > 0)
                {
                    BookList = new List<Book>();
                    foreach (DataRow row in table.Rows)
                    {
                        Book book = new Book();
                        book.BookID = (int)row["BookID"];
                        book.ProductID = (int)row["ProductID"];
                        book.BookTitle = row["BookTitle"].ToString();
                        book.Synopsis = row["Synopsis"].ToString();
                        book.ISBN = row["ISBN"].ToString();
                        book.SellingPrice = Convert.ToDouble(row["SellingPrice"]);
                        book.BookCategoryID = (int)row["BookCategoryID"];
                        book.CoverImage = row["CoverImage"].ToString();
                        book.PublisherID = (int)row["PublisherID"];
                        BookList.Add(book);


                    }
                }
            }
            return BookList;
        }

        public List<Book> AuthorBookSearch(string query)
        {
            List<Book> BookList = null;

            SqlParameter[] Params = { new SqlParameter("@Search", query) };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_AuthorBookSearch", CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count > 0)
                {
                    BookList = new List<Book>();
                    foreach (DataRow row in table.Rows)
                    {
                        Book book = new Book();
                        book.BookID = (int)row["BookID"];
                        book.ProductID = (int)row["ProductID"];
                        book.BookTitle = row["BookTitle"].ToString();
                        book.ISBN = row["ISBN"].ToString();
                        book.SellingPrice = Convert.ToDouble(row["SellingPrice"]);
                        book.BookCategoryID = (int)row["BookCategoryID"];
                        book.CoverImage = row["CoverImage"].ToString();
                        book.PublisherID = (int)row["PublisherID"];
                        BookList.Add(book);


                    }
                }
            }
            return BookList;
        }

        public List<Book> BookTitleBookSearch(string query)
        {
            List<Book> BookList = null;

            SqlParameter[] Params = { new SqlParameter("@Search", query) };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_BookTitleBookSearch", CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count > 0)
                {
                    BookList = new List<Book>();
                    foreach (DataRow row in table.Rows)
                    {
                        Book book = new Book();
                        book.BookID = (int)row["BookID"];
                        book.ProductID = (int)row["ProductID"];
                        book.BookTitle = row["BookTitle"].ToString();
                        book.ISBN = row["ISBN"].ToString();
                        book.SellingPrice = Convert.ToDouble(row["SellingPrice"]);
                        book.BookCategoryID = (int)row["BookCategoryID"];
                        book.CoverImage = row["CoverImage"].ToString();
                        book.PublisherID = (int)row["PublisherID"];
                        BookList.Add(book);


                    }
                }
            }
            return BookList;
        }

        public List<Book> ISBNBookSearch(string query)
        {
            List<Book> BookList = null;

            SqlParameter[] Params = { new SqlParameter("@Search", query) };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_ISBNBookSearch", CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count > 0)
                {
                    BookList = new List<Book>();
                    foreach (DataRow row in table.Rows)
                    {
                        Book book = new Book();
                        book.BookID = (int)row["BookID"];
                        book.ProductID = (int)row["ProductID"];
                        book.BookTitle = row["BookTitle"].ToString();
                        book.ISBN = row["ISBN"].ToString();
                        book.SellingPrice = Convert.ToDouble(row["SellingPrice"]);
                        book.BookCategoryID = (int)row["BookCategoryID"];
                        book.CoverImage = row["CoverImage"].ToString();
                        book.PublisherID = (int)row["PublisherID"];
                        BookList.Add(book);


                    }
                }
            }
            return BookList;
        }

        public List<Book> PublisherBookSearch(string query)
        {
            List<Book> BookList = null;

            SqlParameter[] Params = { new SqlParameter("@Search", query) };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_PublisherBookSearch", CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count > 0)
                {
                    BookList = new List<Book>();
                    foreach (DataRow row in table.Rows)
                    {
                        Book book = new Book();
                        book.BookID = (int)row["BookID"];
                        book.ProductID = (int)row["ProductID"];
                        book.BookTitle = row["BookTitle"].ToString();
                        book.ISBN = row["ISBN"].ToString();
                        book.SellingPrice = Convert.ToDouble(row["SellingPrice"]);
                        book.BookCategoryID = (int)row["BookCategoryID"];
                        book.CoverImage = row["CoverImage"].ToString();
                        book.PublisherID = (int)row["PublisherID"];
                        BookList.Add(book);


                    }
                }
            }
            return BookList;
        }

        public List<Book> CategoryBookSearch(string query)
        {
            List<Book> BookList = null;

            SqlParameter[] Params = { new SqlParameter("@Search", query) };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_CategoryBookSearch", CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count > 0)
                {
                    BookList = new List<Book>();
                    foreach (DataRow row in table.Rows)
                    {
                        Book book = new Book();
                        book.BookID = (int)row["BookID"];
                        book.ProductID = (int)row["ProductID"];
                        book.BookTitle = row["BookTitle"].ToString();
                        book.ISBN = row["ISBN"].ToString();
                        book.SellingPrice = Convert.ToDouble(row["SellingPrice"]);
                        book.BookCategoryID = (int)row["BookCategoryID"];
                        book.CoverImage = row["CoverImage"].ToString();
                        book.PublisherID = (int)row["PublisherID"];
                        BookList.Add(book);


                    }
                }
            }
            return BookList;
        }

        public List<Book> BookTitleFromQueryBookSearch(string query, double fromPrice)
        {
            List<Book> BookList = null;

            SqlParameter[] Params = { 
                                        new SqlParameter("@Search", query) ,
                                        new SqlParameter("@FromPrice", fromPrice)
                                    };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_BookTitleQueryFromBookSearch", CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count > 0)
                {
                    BookList = new List<Book>();
                    foreach (DataRow row in table.Rows)
                    {
                        Book book = new Book();
                        book.BookID = (int)row["BookID"];
                        book.ProductID = (int)row["ProductID"];
                        book.BookTitle = row["BookTitle"].ToString();
                        book.ISBN = row["ISBN"].ToString();
                        book.SellingPrice = Convert.ToDouble(row["SellingPrice"]);
                        book.BookCategoryID = (int)row["BookCategoryID"];
                        book.CoverImage = row["CoverImage"].ToString();
                        book.PublisherID = (int)row["PublisherID"];
                        BookList.Add(book);


                    }
                }
            }
            return BookList;
        }

        public List<Book> AuthorFromQueryBookSearch(string query, double fromPrice)
        {
            List<Book> BookList = null;

            SqlParameter[] Params = { 
                                        new SqlParameter("@Search", query) ,
                                        new SqlParameter("@FromPrice", fromPrice)
                                    };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_AuthorQueryFromBookSearch", CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count > 0)
                {
                    BookList = new List<Book>();
                    foreach (DataRow row in table.Rows)
                    {
                        Book book = new Book();
                        book.BookID = (int)row["BookID"];
                        book.ProductID = (int)row["ProductID"];
                        book.BookTitle = row["BookTitle"].ToString();
                        book.ISBN = row["ISBN"].ToString();
                        book.SellingPrice = Convert.ToDouble(row["SellingPrice"]);
                        book.BookCategoryID = (int)row["BookCategoryID"];
                        book.CoverImage = row["CoverImage"].ToString();
                        book.PublisherID = (int)row["PublisherID"];
                        BookList.Add(book);


                    }
                }
            }
            return BookList;
        }

        public List<Book> PublisherFromQueryBookSearch(string query, double fromPrice)
        {
            List<Book> BookList = null;

            SqlParameter[] Params = { 
                                        new SqlParameter("@Search", query) ,
                                        new SqlParameter("@FromPrice", fromPrice)
                                    };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_PublisherQueryFromBookSearch", CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count > 0)
                {
                    BookList = new List<Book>();
                    foreach (DataRow row in table.Rows)
                    {
                        Book book = new Book();
                        book.BookID = (int)row["BookID"];
                        book.ProductID = (int)row["ProductID"];
                        book.BookTitle = row["BookTitle"].ToString();
                        book.ISBN = row["ISBN"].ToString();
                        book.SellingPrice = Convert.ToDouble(row["SellingPrice"]);
                        book.BookCategoryID = (int)row["BookCategoryID"];
                        book.CoverImage = row["CoverImage"].ToString();
                        book.PublisherID = (int)row["PublisherID"];
                        BookList.Add(book);


                    }
                }
            }
            return BookList;
        }

        public List<Book> CategoryFromQueryBookSearch(string query, double fromPrice)
        {
            List<Book> BookList = null;

            SqlParameter[] Params = { 
                                        new SqlParameter("@Search", query) ,
                                        new SqlParameter("@FromPrice", fromPrice)
                                    };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_CategoryQueryFromBookSearch", CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count > 0)
                {
                    BookList = new List<Book>();
                    foreach (DataRow row in table.Rows)
                    {
                        Book book = new Book();
                        book.BookID = (int)row["BookID"];
                        book.ProductID = (int)row["ProductID"];
                        book.BookTitle = row["BookTitle"].ToString();
                        book.ISBN = row["ISBN"].ToString();
                        book.SellingPrice = Convert.ToDouble(row["SellingPrice"]);
                        book.BookCategoryID = (int)row["BookCategoryID"];
                        book.CoverImage = row["CoverImage"].ToString();
                        book.PublisherID = (int)row["PublisherID"];
                        BookList.Add(book);


                    }
                }
            }
            return BookList;
        }

        public List<Book> ISBNFromQueryBookSearch(string query, double fromPrice)
        {
            List<Book> BookList = null;

            SqlParameter[] Params = { 
                                        new SqlParameter("@Search", query) ,
                                        new SqlParameter("@FromPrice", fromPrice)
                                    };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_ISBNQueryFromBookSearch", CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count > 0)
                {
                    BookList = new List<Book>();
                    foreach (DataRow row in table.Rows)
                    {
                        Book book = new Book();
                        book.BookID = (int)row["BookID"];
                        book.ProductID = (int)row["ProductID"];
                        book.BookTitle = row["BookTitle"].ToString();
                        book.ISBN = row["ISBN"].ToString();
                        book.SellingPrice = Convert.ToDouble(row["SellingPrice"]);
                        book.BookCategoryID = (int)row["BookCategoryID"];
                        book.CoverImage = row["CoverImage"].ToString();
                        book.PublisherID = (int)row["PublisherID"];
                        BookList.Add(book);


                    }
                }
            }
            return BookList;
        }

        public List<Book> ISBNToQueryBookSearch(string query, double ToPrice)
        {
            List<Book> BookList = null;

            SqlParameter[] Params = { 
                                        new SqlParameter("@Search", query) ,
                                        new SqlParameter("@ToPrice", ToPrice)
                                    };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_ISBNQueryToBookSearch", CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count > 0)
                {
                    BookList = new List<Book>();
                    foreach (DataRow row in table.Rows)
                    {
                        Book book = new Book();
                        book.BookID = (int)row["BookID"];
                        book.ProductID = (int)row["ProductID"];
                        book.BookTitle = row["BookTitle"].ToString();
                        book.ISBN = row["ISBN"].ToString();
                        book.SellingPrice = Convert.ToDouble(row["SellingPrice"]);
                        book.BookCategoryID = (int)row["BookCategoryID"];
                        book.CoverImage = row["CoverImage"].ToString();
                        book.PublisherID = (int)row["PublisherID"];
                        BookList.Add(book);


                    }
                }
            }
            return BookList;
        }

        public List<Book> CategoryToQueryBookSearch(string query, double ToPrice)
        {
            List<Book> BookList = null;

            SqlParameter[] Params = { 
                                        new SqlParameter("@Search", query) ,
                                        new SqlParameter("@ToPrice", ToPrice)
                                    };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_CategoryQueryToBookSearch", CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count > 0)
                {
                    BookList = new List<Book>();
                    foreach (DataRow row in table.Rows)
                    {
                        Book book = new Book();
                        book.BookID = (int)row["BookID"];
                        book.ProductID = (int)row["ProductID"];
                        book.BookTitle = row["BookTitle"].ToString();
                        book.ISBN = row["ISBN"].ToString();
                        book.SellingPrice = Convert.ToDouble(row["SellingPrice"]);
                        book.BookCategoryID = (int)row["BookCategoryID"];
                        book.CoverImage = row["CoverImage"].ToString();
                        book.PublisherID = (int)row["PublisherID"];
                        BookList.Add(book);


                    }
                }
            }
            return BookList;
        }

        public List<Book> PublisherToQueryBookSearch(string query, double ToPrice)
        {
            List<Book> BookList = null;

            SqlParameter[] Params = { 
                                        new SqlParameter("@Search", query) ,
                                        new SqlParameter("@ToPrice", ToPrice)
                                    };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_PublisherQueryToBookSearch", CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count > 0)
                {
                    BookList = new List<Book>();
                    foreach (DataRow row in table.Rows)
                    {
                        Book book = new Book();
                        book.BookID = (int)row["BookID"];
                        book.ProductID = (int)row["ProductID"];
                        book.BookTitle = row["BookTitle"].ToString();
                        book.ISBN = row["ISBN"].ToString();
                        book.SellingPrice = Convert.ToDouble(row["SellingPrice"]);
                        book.BookCategoryID = (int)row["BookCategoryID"];
                        book.CoverImage = row["CoverImage"].ToString();
                        book.PublisherID = (int)row["PublisherID"];
                        BookList.Add(book);


                    }
                }
            }
            return BookList;
        }

        public List<Book> BookTitleToQueryBookSearch(string query, double ToPrice)
        {
            List<Book> BookList = null;

            SqlParameter[] Params = { 
                                        new SqlParameter("@Search", query) ,
                                        new SqlParameter("@ToPrice", ToPrice)
                                    };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_BookTitleQueryToBookSearch", CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count > 0)
                {
                    BookList = new List<Book>();
                    foreach (DataRow row in table.Rows)
                    {
                        Book book = new Book();
                        book.BookID = (int)row["BookID"];
                        book.ProductID = (int)row["ProductID"];
                        book.BookTitle = row["BookTitle"].ToString();
                        book.ISBN = row["ISBN"].ToString();
                        book.SellingPrice = Convert.ToDouble(row["SellingPrice"]);
                        book.BookCategoryID = (int)row["BookCategoryID"];
                        book.CoverImage = row["CoverImage"].ToString();
                        book.PublisherID = (int)row["PublisherID"];
                        BookList.Add(book);


                    }
                }
            }
            return BookList;
        }

        public List<Book> AuthorToQueryBookSearch(string query, double ToPrice)
        {
            List<Book> BookList = null;

            SqlParameter[] Params = { 
                                        new SqlParameter("@Search", query) ,
                                        new SqlParameter("@ToPrice", ToPrice)
                                    };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_AuthorQueryToBookSearch", CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count > 0)
                {
                    BookList = new List<Book>();
                    foreach (DataRow row in table.Rows)
                    {
                        Book book = new Book();
                        book.BookID = (int)row["BookID"];
                        book.ProductID = (int)row["ProductID"];
                        book.BookTitle = row["BookTitle"].ToString();
                        book.ISBN = row["ISBN"].ToString();
                        book.SellingPrice = Convert.ToDouble(row["SellingPrice"]);
                        book.BookCategoryID = (int)row["BookCategoryID"];
                        book.CoverImage = row["CoverImage"].ToString();
                        book.PublisherID = (int)row["PublisherID"];
                        BookList.Add(book);


                    }
                }
            }
            return BookList;
        }

        public List<Book> AuthorBETWEENQueryBookSearch(string query, double ToPrice, double FromPrice)
        {
            List<Book> BookList = null;

            SqlParameter[] Params = { 
                                        new SqlParameter("@Search", query) ,
                                        new SqlParameter("@ToPrice", ToPrice),
                                        new SqlParameter("@FromPrice", FromPrice)
                                    };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_AuthorQueryBETWEENBookSearch", CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count > 0)
                {
                    BookList = new List<Book>();
                    foreach (DataRow row in table.Rows)
                    {
                        Book book = new Book();
                        book.BookID = (int)row["BookID"];
                        book.ProductID = (int)row["ProductID"];
                        book.BookTitle = row["BookTitle"].ToString();
                        book.ISBN = row["ISBN"].ToString();
                        book.SellingPrice = Convert.ToDouble(row["SellingPrice"]);
                        book.BookCategoryID = (int)row["BookCategoryID"];
                        book.CoverImage = row["CoverImage"].ToString();
                        book.PublisherID = (int)row["PublisherID"];
                        BookList.Add(book);


                    }
                }
            }
            return BookList;
        }

        public List<Book> BookTitleBETWEENQueryBookSearch(string query, double ToPrice, double FromPrice)
        {
            List<Book> BookList = null;

            SqlParameter[] Params = { 
                                        new SqlParameter("@Search", query) ,
                                        new SqlParameter("@ToPrice", ToPrice),
                                        new SqlParameter("@FromPrice", FromPrice)
                                    };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_BookTitleQueryBETWEENBookSearch", CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count > 0)
                {
                    BookList = new List<Book>();
                    foreach (DataRow row in table.Rows)
                    {
                        Book book = new Book();
                        book.BookID = (int)row["BookID"];
                        book.ProductID = (int)row["ProductID"];
                        book.BookTitle = row["BookTitle"].ToString();
                        book.ISBN = row["ISBN"].ToString();
                        book.SellingPrice = Convert.ToDouble(row["SellingPrice"]);
                        book.BookCategoryID = (int)row["BookCategoryID"];
                        book.CoverImage = row["CoverImage"].ToString();
                        book.PublisherID = (int)row["PublisherID"];
                        BookList.Add(book);


                    }
                }
            }
            return BookList;
        }

        public List<Book> PublisherBETWEENQueryBookSearch(string query, double ToPrice, double FromPrice)
        {
            List<Book> BookList = null;

            SqlParameter[] Params = { 
                                        new SqlParameter("@Search", query) ,
                                        new SqlParameter("@ToPrice", ToPrice),
                                        new SqlParameter("@FromPrice", FromPrice)
                                    };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_PublisherQueryBETWEENBookSearch", CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count > 0)
                {
                    BookList = new List<Book>();
                    foreach (DataRow row in table.Rows)
                    {
                        Book book = new Book();
                        book.BookID = (int)row["BookID"];
                        book.ProductID = (int)row["ProductID"];
                        book.BookTitle = row["BookTitle"].ToString();
                        book.ISBN = row["ISBN"].ToString();
                        book.SellingPrice = Convert.ToDouble(row["SellingPrice"]);
                        book.BookCategoryID = (int)row["BookCategoryID"];
                        book.CoverImage = row["CoverImage"].ToString();
                        book.PublisherID = (int)row["PublisherID"];
                        BookList.Add(book);


                    }
                }
            }
            return BookList;
        }

        public List<Book> ISBNBETWEENQueryBookSearch(string query, double ToPrice, double FromPrice)
        {
            List<Book> BookList = null;

            SqlParameter[] Params = { 
                                        new SqlParameter("@Search", query) ,
                                        new SqlParameter("@ToPrice", ToPrice),
                                        new SqlParameter("@FromPrice", FromPrice)
                                    };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_ISBNQueryBETWEENBookSearch", CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count > 0)
                {
                    BookList = new List<Book>();
                    foreach (DataRow row in table.Rows)
                    {
                        Book book = new Book();
                        book.BookID = (int)row["BookID"];
                        book.ProductID = (int)row["ProductID"];
                        book.BookTitle = row["BookTitle"].ToString();
                        book.ISBN = row["ISBN"].ToString();
                        book.SellingPrice = Convert.ToDouble(row["SellingPrice"]);
                        book.BookCategoryID = (int)row["BookCategoryID"];
                        book.CoverImage = row["CoverImage"].ToString();
                        book.PublisherID = (int)row["PublisherID"];
                        BookList.Add(book);


                    }
                }
            }
            return BookList;
        }

        public List<Book> CategoryBETWEENQueryBookSearch(string query, double ToPrice, double FromPrice)
        {
            List<Book> BookList = null;

            SqlParameter[] Params = { 
                                        new SqlParameter("@Search", query) ,
                                        new SqlParameter("@ToPrice", ToPrice),
                                        new SqlParameter("@FromPrice", FromPrice)
                                    };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_CategoryQueryBETWEENBookSearch", CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count > 0)
                {
                    BookList = new List<Book>();
                    foreach (DataRow row in table.Rows)
                    {
                        Book book = new Book();
                        book.BookID = (int)row["BookID"];
                        book.ProductID = (int)row["ProductID"];
                        book.BookTitle = row["BookTitle"].ToString();
                        book.ISBN = row["ISBN"].ToString();
                        book.SellingPrice = Convert.ToDouble(row["SellingPrice"]);
                        book.BookCategoryID = (int)row["BookCategoryID"];
                        book.CoverImage = row["CoverImage"].ToString();
                        book.PublisherID = (int)row["PublisherID"];
                        BookList.Add(book);


                    }
                }
            }
            return BookList;
        }

        #endregion

        #region User
        public Book UGetBookDetails(int ProductID)
        {
            Book book = null;

            SqlParameter[] Params = { new SqlParameter("@ProductID", ProductID) };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_viewSpecificBookUser",
                CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    book = new Book();
                    book.ProductID = Convert.ToInt32(row["ProductID"]);
                    book.BookID = Convert.ToInt32(row["BookID"]);
                    book.BookTitle = row["BookTitle"].ToString();
                    book.Synopsis = row["Synopsis"].ToString();
                    book.SellingPrice = Convert.ToDouble(row["SellingPrice"]);
                    book.ISBN = row["ISBN"].ToString();
                    book.BookCategoryID = Convert.ToInt32(row["BookCategoryID"]);
                    book.PublisherID = Convert.ToInt32(row["PublisherID"]);
                    book.CoverImage = row["CoverImage"].ToString();
                }
            }
            return book;
        }
        #endregion

        #region Experiments

        public Book experimentalBook(Book book)
        {
            Book bk;
            SqlParameter[] Params = { new SqlParameter("@CostPrice", book.CostPrice),
                                      new SqlParameter("@SellingPrice", book.SellingPrice),
                                      new SqlParameter("@DateAdded", book.DateAdded)
                                    };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_ManhattanProject", CommandType.StoredProcedure, Params))
            {
                bk = new Book();
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    bk.ProductID = Convert.ToInt32(row["ProductID"]);
                }

            }
            return bk;
        }

        public BookAuthor TrailInsertBook(Book book)
        {
            BookAuthor bookAuthor = new BookAuthor();
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@ProductID", book.ProductID),
                new SqlParameter("@BookTitle", book.BookTitle),
                new SqlParameter("@Synopsis", book.Synopsis),
                new SqlParameter("@ISBN", book.ISBN),
                new SqlParameter("@BookCategoryID", book.BookCategoryID),
                new SqlParameter("@PublisherID", book.PublisherID),
                new SqlParameter("@SupplierID", book.SupplierID),
                new SqlParameter("@CoverImage", book.CoverImage)
            };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_Insert_Book", CommandType.StoredProcedure, Params))
            {
                
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    bookAuthor.BookID = Convert.ToInt32(row["BookID"]);
                }
            }
            return bookAuthor;
        }

        #endregion

        #region Admin

        public Book GetBookDetails(int ProductID)
        {
            Book book = null;

            SqlParameter[] Params = { new SqlParameter("@ProductID", ProductID) };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_ViewSpecificBookAdmin",
                CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    book = new Book();
                    book.BookID = Convert.ToInt32(row["BookID"]);
                    book.ProductID = Convert.ToInt32(row["ProductID"]);
                    book.BookTitle = row["BookTitle"].ToString();
                    book.Synopsis = row["Synopsis"].ToString();
                    book.CostPrice = Convert.ToDouble(row["CostPrice"]);
                    book.SellingPrice = Convert.ToDouble(row["SellingPrice"]);
                    book.SupplierID = Convert.ToInt32(row["SupplierID"]);
                    book.ISBN = row["ISBN"].ToString();
                    book.catName = row["Category"].ToString();
                    book.pubName = row["Publisher"].ToString();
                    book.CoverImage = row["CoverImage"].ToString();
                }
            }
            return book;
        }

        public bool UpdateBook(Book book)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@BookID", book.BookID ),
                new SqlParameter("@ProductID", book.ProductID),
                new SqlParameter("@BookTitle", book.BookTitle),
                new SqlParameter("@ISBN", book.ISBN),
                new SqlParameter("@Synopsis", book.Synopsis),
                new SqlParameter("@BookCategoryID", book.BookCategoryID),
                new SqlParameter("@SupplierID", book.SupplierID),
                new SqlParameter("@PublisherID", book.PublisherID),
                new SqlParameter("@CoverImage", book.CoverImage)
            };
            return DataProvider.ExecuteNonQuery("sp_UpdateBook", CommandType.StoredProcedure,
                Params);
        }

        public bool UpdateBookProduct(Book book)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@ProductID", book.ProductID ),
                new SqlParameter("@CostPrice", book.CostPrice),
                new SqlParameter("@SellingPrice", book.SellingPrice),
                new SqlParameter("@IsBook", book.IsBook = true),
                new SqlParameter("@DateAdded", book.DateAdded = DateTime.Now),
            };
            return DataProvider.ExecuteNonQuery("sp_UpdateProduct", CommandType.StoredProcedure,
                Params);
        }

        public bool DeleteBook(Book book)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@ProductID", book.ProductID)
            };
            return DataProvider.ExecuteNonQuery("sp_DeleteBook", CommandType.StoredProcedure, //procedure
                Params);
        }

        public bool InsertBookProduct(Book book)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@CostPrice", book.CostPrice),
                new SqlParameter("@SellingPrice", book.SellingPrice),
                new SqlParameter("@DateTime", book.DateAdded)
            };
            return DataProvider.ExecuteNonQuery("sp_InsertProduct", CommandType.StoredProcedure,
                Params);
        }

        public bool InsertBook(Book book)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@ProductID", book.ProductID),
                new SqlParameter("@BookTitle", book.BookTitle),
                new SqlParameter("@Synopsis", book.Synopsis),
                new SqlParameter("@ISBN", book.ISBN),
                new SqlParameter("@BookCategoryID", book.BookCategoryID),
                new SqlParameter("@PublisherID", book.PublisherID),
                new SqlParameter("@SupplierID", book.SupplierID),
                new SqlParameter("@CoverImage", book.CoverImage)
            };
            return DataProvider.ExecuteNonQuery("sp_InsertBook", CommandType.StoredProcedure,
                Params);
        }

        #endregion
    }
}
