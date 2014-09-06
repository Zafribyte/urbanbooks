using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace urbanbooks
{
    public class AuthorHandler
    {
        public List<Author> GetAuthorList()
        {
            List<Author> AuthorList = null;

            using (DataTable table = DataProvider.ExecuteSelectCommand("sp_ViewAllAuthors",
                CommandType.StoredProcedure))
            {
                if (table.Rows.Count > 0)
                {
                    AuthorList = new List<Author>();
                    foreach (DataRow row in table.Rows)
                    {
                        Author author = new Author();
                        author.AuthorID = Convert.ToInt32(row["AuthorID"]);
                        author.Name = row["FullName"].ToString();
                        AuthorList.Add(author);
                    }
                }
            }
            return AuthorList;
        }

        public List<Author> AuthorGlobalSearch(string query)
        {
            List<Author> AuthorList = null;
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@Search", query) ///SEARCH
            };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_AuthorGlobalSearch",
                CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count > 0)
                {
                    AuthorList = new List<Author>();
                    foreach (DataRow row in table.Rows)
                    {
                        Author author = new Author();
                        author.AuthorID = Convert.ToInt32(row["AuthorID"]);
                        author.Name = row["Name"].ToString();
                        author.Surname = row["Surname"].ToString();
                        AuthorList.Add(author);
                    }
                }
            }
            return AuthorList;
        }

        public Author GetAuthorDetails(int AuthorID)
        {
            Author author = null;

            SqlParameter[] Params = { new SqlParameter("@AuthorID", AuthorID) };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_ViewSpecificAuthor",
                CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    author = new Author();
                    author.AuthorID = Convert.ToInt32(row["AuthorID"]);
                    author.Name = row["Name"].ToString();
                    author.Surname = row["Surname"].ToString();
                }
            }
            return author;
        }

        public Author SearchAuthor(string Query)
        {
            Author author = null;
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@Name", Query) ///SEARCH
            };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("uspSearchAuthor",
                CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    author = new Author();
                    author.AuthorID = Convert.ToInt32(row["AuthorID"]);
                    author.Name = row["Name"].ToString();
                    author.Surname = row["Surname"].ToString();

                }
            }
            return author;
        }

        #region Admin
        public bool DeleteAuthor(int AuthorID)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@AuthorID", AuthorID)
            };
            return DataProvider.ExecuteNonQuery("sp_DeleteAuthor", CommandType.StoredProcedure,
                Params);
        }

        public bool InsertAthor(Author author)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@Name", author.Name ),
                new SqlParameter("@Surname", author.Surname),
            };
            return DataProvider.ExecuteNonQuery("us_AddAuthor", CommandType.StoredProcedure,
                Params);
        }

        public bool UpdateAuthor(Author author)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@AuthorID", author.AuthorID),
                new SqlParameter("@Name", author.Name ),
                new SqlParameter("@Surname", author.Surname),
            };
            return DataProvider.ExecuteNonQuery("sp_UpdateAuthor", CommandType.StoredProcedure,
                Params);
        }
        #endregion
    }
}
