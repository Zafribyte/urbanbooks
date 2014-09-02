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
        Company company = new Company();
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
                        book.AuthName = row["Author"].ToString();
                        book.BookCategoryID = (int)row["BookCategoryID"];
                        book.CoverImage = row["CoverImage"].ToString();
                        BookList.Add(book);


                    }
                }
            }
            return BookList;
        }

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
                    book.BookTitle = row["ProductTitle"].ToString();
                    book.Synopsis = row["Description"].ToString();
                    book.SellingPrice = Convert.ToDouble(row["SellingPrice"]);
                    book.ISBN = row["ISBN"].ToString();
                    book.BookCategoryID = Convert.ToInt32(row["BookCategoryID"]);
                    book.PublisherID = Convert.ToInt32(row["Publisher"]);
                    book.AuthorID = Convert.ToInt32(row["AuthorID"]);
                }
            }
            return book;
        }
        #endregion
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
                    //book.SupplierID = Convert.ToInt32(row["SupplierID"]);
                    book.ISBN = row["ISBN"].ToString();
                    book.catName = row["Category"].ToString();
                    book.pubName = row["Publisher"].ToString();
                    book.AuthName = row["Author"].ToString();
                    book.CoverImage = row["CoverImage"].ToString();
                }
            }
            return book;
        }

        public bool UpdateBook(Book book)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@ProductID", book.ProductID ),
                new SqlParameter("@BookTitle", book.BookTitle),
                new SqlParameter("@isbn", book.ISBN),
                new SqlParameter("@BookCategoryID", book.BookCategoryID)
            };
            return DataProvider.ExecuteNonQuery("sp_UpdateBook", CommandType.StoredProcedure,
                Params);
        }

        public bool UpdateBookProduct(Book book)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@ProductID", book.ProductID ),
                new SqlParameter("@Title", book.BookTitle),
                new SqlParameter("@Description", book.Synopsis),
                new SqlParameter("@CostPrice", book.CostPrice),
                //new SqlParameter("@MarkUp", company.MarkUp),
                new SqlParameter("@SellingPrice", book.SellingPrice),
                new SqlParameter("@SupplierID", book.SupplierID),
                //new SqlParameter("@EmployeeID", book.EmployeeID),
                new SqlParameter("@DateAdded", book.DateAdded),
            };
            return DataProvider.ExecuteNonQuery("sp_UpdateProduct", CommandType.StoredProcedure,
                Params);
        }

        public bool DeleteBookProduct(int BookID)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@BookID", BookID)
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
                new SqlParameter("@AuthorID", book.AuthorID),
                new SqlParameter("@CoverImage", book.CoverImage)
            };
            return DataProvider.ExecuteNonQuery("sp_InsertBook", CommandType.StoredProcedure,
                Params);
        }

        #endregion
    }
}
