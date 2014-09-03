using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace urbanbooks
{
    public class BookCategoryHandler
    {

        public List<BookCategory> GetBookCategoryList()
        {
            List<BookCategory> bookCategoryList = null;

            using (DataTable table = DataProvider.ExecuteSelectCommand("sp_ViewAllBookCategories",
                CommandType.StoredProcedure))
            {
                if (table.Rows.Count > 0)
                {
                    bookCategoryList = new List<BookCategory>();
                    foreach (DataRow row in table.Rows)
                    {
                        BookCategory bookCategory = new BookCategory();
                        bookCategory.BookCategoryID = Convert.ToInt32(row["BookCategoryID"]);
                        bookCategory.CategoryName = row["CategoryName"].ToString();
                        bookCategory.CategoryDescription = row["CategoryDescription"].ToString();
                        bookCategoryList.Add(bookCategory);
                    }
                }
            }
            return bookCategoryList;
        }

        public BookCategory GetBookCategory(int BookCategoryID)
        {
            BookCategory bookCategory = null;

            SqlParameter[] Params = { new SqlParameter("@BookCategoryID", BookCategoryID) };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_ViewSpecificBookType",
                CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    bookCategory = new BookCategory();
                    bookCategory.BookCategoryID = Convert.ToInt32(row["BookCategoryID"]);
                    bookCategory.CategoryDescription = row["CategoryDescription"].ToString();
                }
            }
            return bookCategory;
        }

        public BookCategory SearchBookcategory(string Query)
        {
            BookCategory bookCategory = null;
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@CategoryDescription", Query) ///SEARCH
            };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("uspSearchBookCategory",
                CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    bookCategory = new BookCategory();
                    bookCategory.BookCategoryID = Convert.ToInt32(row["BookCategory"]);
                    bookCategory.CategoryDescription = row["CategoryDescription"].ToString();


                }
            }
            return bookCategory;
        }

        #region Admin

        public bool DeleteBookCategory(int BookCategoryID)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@BookCategoryID", BookCategoryID)
            };
            return DataProvider.ExecuteNonQuery("sp_DeleteBookCategory", CommandType.StoredProcedure,
                Params);
        }

        public bool InsertBookCategory(BookCategory bookCategory)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@CategoryName", bookCategory.CategoryName),
                new SqlParameter("@CategoryDescription", bookCategory.CategoryDescription )
            };
            return DataProvider.ExecuteNonQuery("sp_InsertBookCategory", CommandType.StoredProcedure,
                Params);
        }

        public bool UpdateBookCategory(BookCategory bookCategory)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@BookCategoryID", bookCategory.BookCategoryID),
                new SqlParameter("@CategoryName", bookCategory.CategoryName),
                new SqlParameter("@CategoryDescription", bookCategory.CategoryDescription )
            };
            return DataProvider.ExecuteNonQuery("sp_UpdateBookCategory", CommandType.StoredProcedure,
                Params);
        }

        #endregion


    }
}
