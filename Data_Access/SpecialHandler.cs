using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace urbanbooks
{
    public class SpecialHandler
    {
        public bool InsertSpecial(Special special)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@StartDate", special.StartDate),
                new SqlParameter("@EndDate", special.EndDate),
                new SqlParameter("@Description", special.Description),
                new SqlParameter("@CutDownPercentage", special.CutDownPercentage),
            };
            return DataProvider.ExecuteNonQuery("sp_InsertSpecial", CommandType.StoredProcedure,
                Params);
        }

        public Special GetSpecialDetails(int SpecialID)
        {
            Special special = null;

            SqlParameter[] Params = { new SqlParameter("@SpecialID", SpecialID) };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_ViewSpecificSpecial",
                CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    special = new Special();
                    special.SpecialID = Convert.ToInt32(row["SpecialID"]);
                    special.StartDate = Convert.ToDateTime(row["StartDate"]);
                    special.EndDate = Convert.ToDateTime(row["EndDate"]);
                    special.Description = row["Description"].ToString();
                    special.CutDownPercentage = Convert.ToInt32(row["CutDownPercentage"]);
                    //special.SpecialPrice = Convert.ToDouble(row["SpecialPrice"]);
                }
            }
            return special;
        }

        public List<Special> GetSpecialsList()
        {
            List<Special> SpecialsList = null;

            using (DataTable table = DataProvider.ExecuteSelectCommand("sp_ViewAllSpecials", //*Note
                CommandType.StoredProcedure))
            {
                if (table.Rows.Count > 0)
                {
                    SpecialsList = new List<Special>();
                    foreach (DataRow row in table.Rows)
                    {
                        Special special = new Special();
                        special.SpecialID = Convert.ToInt32(row["SpecialID"]);
                        special.StartDate = Convert.ToDateTime(row["StartDate"]);
                        special.EndDate = Convert.ToDateTime(row["EndDate"]);
                        special.Description = row["Description"].ToString();
                        special.CutDownPercentage = Convert.ToInt32(row["CutDownPercentage"]);
                        //special.SpecialPrice = Convert.ToDouble(row["SpecialPrice"]);
                        SpecialsList.Add(special);
                    }
                }
            }
            return SpecialsList;
        }

        public bool UpdateSpecial(Special special)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@SpecialID", special.SpecialID ),
                new SqlParameter("@StartDate", special.StartDate),
                new SqlParameter("@EndDate", special.EndDate),
                new SqlParameter("@Description", special.Description),
                new SqlParameter("@CutDownPercentage", special.CutDownPercentage),
               // new SqlParameter("@SpecialPrice", special.SpecialPrice)
            };
            return DataProvider.ExecuteNonQuery("sp_UpdateSpecials", CommandType.StoredProcedure,
                Params);
        }

        public bool DeleteSpecial(int SpecialID)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@SpecialID", SpecialID)
            };
            return DataProvider.ExecuteNonQuery("ps_DeleteSpecial", CommandType.StoredProcedure,
                Params);
        }
    }
}
