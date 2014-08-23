using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace urbanbooks
{
    public class TechCategoryHandler
    {
        public List<TechCategory> GetTechCategoryList()
        {
            List<TechCategory> TechCategoryList = null;

            using (DataTable table = DataProvider.ExecuteSelectCommand("sp_ViewAllTechCategories",
                CommandType.StoredProcedure))
            {
                if (table.Rows.Count > 0)
                {
                    TechCategoryList = new List<TechCategory>();
                    foreach (DataRow row in table.Rows)
                    {
                        TechCategory category = new TechCategory();
                        category.TechCategoryID = Convert.ToInt32(row["TechCategoryID"]);
                        category.CategoryName = row["CategoryName"].ToString();
                        category.CategoryDescription = row["CategoryDescription"].ToString();
                        TechCategoryList.Add(category);
                    }
                }
            }
            return TechCategoryList;
        }

        public TechCategory GetTechCategoryDetails(int TechCategoryID)
        {
            TechCategory category = null;

            SqlParameter[] Params = { new SqlParameter("@TechnologyTypeID", TechCategoryID) };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_ViewSpecificTechType",
                CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    category = new TechCategory();
                    category.TechCategoryID = Convert.ToInt32(row["TechCategoryID"]);
                    category.CategoryName = row["CategoryName"].ToString();
                    category.CategoryDescription = row["CategoryDescription"].ToString();
                }
            }
            return category;
        }

        public TechCategory SearchTechCategory(string Query)
        {
            TechCategory category = null;
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@Name", Query) ///SEARCH
            };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_...",
                CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    category = new TechCategory();
                    category.TechCategoryID = Convert.ToInt32(row["TechCategoryID"]);
                    category.CategoryName = row["CategoryName"].ToString();
                    category.CategoryDescription = row["CategoryDescription"].ToString();
                   
                }
            }
            return category;
        }

        public bool DeleteTechCategory(int TechnologyTypeID)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@TechnologyTypeID", TechnologyTypeID)
            };
            return DataProvider.ExecuteNonQuery("sp_DeleteTechCategory", CommandType.StoredProcedure,
                Params);
        }

        public bool InsertTechCategory(TechCategory categor)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@Name", categor.CategoryName )
            };
            return DataProvider.ExecuteNonQuery("sp_InsertTechCategory", CommandType.StoredProcedure,
                Params);
        }

        public bool UpdateTechCategory(TechCategory category)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@TechnologyTypeID", category.TechCategoryID),
                new SqlParameter("@CategoryName", category.CategoryName ),
                new SqlParameter("@CategoryDescription",category.CategoryDescription)
            };
            return DataProvider.ExecuteNonQuery("sp_UpdateTechnCategory", CommandType.StoredProcedure,
                Params);
        }
    }
}