using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace urbanbooks
{
    public class BookAuthorHandler
    {
        public bool DeleteBookAuthor(BookAuthor bookAuthor)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@BookID", bookAuthor.BookID),
                new SqlParameter("@AuthorID", bookAuthor.AuthorID)
            };
            return DataProvider.ExecuteNonQuery("sp_DeleteBook_Author", CommandType.StoredProcedure,
                Params);
        }

        public bool InsertBookAuthor(BookAuthor bookAuthor)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@BookID", bookAuthor.BookID),
                new SqlParameter("@AuthorID", bookAuthor.AuthorID )
            };
            return DataProvider.ExecuteNonQuery("sp_InsertBook_Author", CommandType.StoredProcedure,
                Params);
        }

        public List<BookAuthor> GetBookAuthors(int BookID)
        {
            List<BookAuthor> bookAuthors = null;

            SqlParameter[] Params = { new SqlParameter("@BookID", BookID) };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_ViewBookAuthors",
                CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count > 0)
                {
                    bookAuthors = new List<BookAuthor>();
                    foreach (DataRow row in table.Rows)
                    {
                        BookAuthor bookAuthor = new BookAuthor();
                        bookAuthor.BookID = Convert.ToInt32(row["BookID"]);
                        bookAuthor.AuthorID = Convert.ToInt32(row["AuthorID"]);
                        bookAuthors.Add(bookAuthor);
                    }
                }
            }
            return bookAuthors;
        }
    }
}