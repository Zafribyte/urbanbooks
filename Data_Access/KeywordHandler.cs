using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace urbanbooks
{
    public class KeywordHandler
    {
        public List<Keyword> GetKeywordsList()
        {
            List<Keyword> KeywordsList = null;

            using (DataTable table = DataProvider.ExecuteSelectCommand("sp_ViewAllKeywords", //*Note
                CommandType.StoredProcedure))
            {
                if (table.Rows.Count > 0)
                {
                    KeywordsList = new List<Keyword>();
                    foreach (DataRow row in table.Rows)
                    {
                        Keyword key = new Keyword();
                        key.KeywordID = Convert.ToInt32(row["KeywordID"]);
                        key.Keywords = row["Keywords"].ToString();
                        key.KeywordType = row["KeywordType"].ToString();
                        key.ProductID = Convert.ToInt32(row["ProductID"]);
                        KeywordsList.Add(key);
                    }
                }
            }
            return KeywordsList;
        }

        public bool DeleteKeyword(int KeywordID)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@KeywordID", KeywordID)
            };
            return DataProvider.ExecuteNonQuery("sp_DeleteKeyword", CommandType.StoredProcedure,
                Params);
        }

        public bool InsertKeyword(Keyword key)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@Keywords", key.Keywords),
                new SqlParameter("@KeywordType", key.KeywordType),
                new SqlParameter("@ProductID", key.ProductID),
            };
            return DataProvider.ExecuteNonQuery("sp_InsertKeyword", CommandType.StoredProcedure,
                Params);
        }
    }
}
