using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace urbanbooks
{
    public class TechnologyHandler
    {
        Company company = new Company();
        public List<Technology> GetTechnologyList()
        {
            List<Technology> TechnologyList = null;

            using (DataTable table = DataProvider.ExecuteSelectCommand("sp_ViewAllTechnologyUser",
                CommandType.StoredProcedure))
            {
                if (table.Rows.Count > 0)
                {
                    TechnologyList = new List<Technology>();
                    foreach (DataRow row in table.Rows)
                    {
                        Technology Techno = new Technology();
                        Techno.TechID = (int)row["TechID"];
                        Techno.ProductID = (int)row["ProductID"];
                        Techno.ModelName = row["ModelName"].ToString();
                        Techno.Specs = row["Specs"].ToString();
                        Techno.ModelNumber = row["ModelNumber"].ToString();
                        Techno.SupplierID = Convert.ToInt32(row["SupplierID"].ToString());
                        Techno.ManufacturerID = (int)row["ManufacturerID"];
                        Techno.TechCategoryID = (int)row["TechCategoryID"];
                        Techno.SellingPrice = Convert.ToDouble(row["SellingPrice"]);
                        Techno.ImageFront = row["ImageFront"].ToString();
                        Techno.ImageTop = row["ImageTop"].ToString();
                        Techno.ImageSide = row["ImageSide"].ToString();
                        TechnologyList.Add(Techno);
                    }
                }
            }
            return TechnologyList;
        }

        #region User
        public Technology UGetTechnologyDetails(int ProductID)
        {
            Technology Techno = null;

            SqlParameter[] Params = { new SqlParameter("@ProductID", ProductID) };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_ViewSpecificTechUser",
                CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    Techno = new Technology();
                    Techno.TechID = Convert.ToInt32(row["TechID"]);
                    Techno.ProductID = Convert.ToInt32(row["ProductID"]);
                    Techno.Specs = row["Specs"].ToString();
                    Techno.SellingPrice = Convert.ToDouble(row["SellingPrice"]);
                    Techno.ImageFront = row["ImageFront"].ToString();
                    Techno.ImageTop = row["ImageTop"].ToString();
                    Techno.ImageSide = row["ImageSide"].ToString();
                    Techno.ManufacturerID = (int)row["ManufacturerID"];
                    Techno.ModelNumber = row["ModelNumber"].ToString();
                    
                }
            }
            return Techno;
        }//STORED PROCEDURE
        #endregion

        public Technology experimentalTech(Technology tech)
        {
            Technology tc;
            SqlParameter[] Params = {
                                        new SqlParameter("@CostPrice", tech.CostPrice),
                                        new SqlParameter("@SellingPrice", tech.SellingPrice),
                                        new SqlParameter("@DateAdded", tech.DateAdded)
                                    };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_ManhattanProject", CommandType.StoredProcedure, Params))
            {
                tc = new Technology();
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    tc.ProductID = Convert.ToInt32(row["ProductID"]);
                }

            }
            return tc;
        }

        #region Admin

        public Technology GetTechnologyDetails(int TechID)
        {
            Technology Techno = null;

            SqlParameter[] Params = { new SqlParameter("@TechID", TechID) };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_ViewSpecificTechAdmin",
                CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    Techno = new Technology();
                    Techno.TechID = Convert.ToInt32(row["TechID"]);
                    //Techno.ProductID = Convert.ToInt32(row["ProductID"]);
                    Techno.ModelName = row["ModelName"].ToString();
                    Techno.Specs = row["Specs"].ToString();
                    Techno.ModelNumber = row["ModelNumber"].ToString();
                    Techno.CostPrice = Convert.ToDouble(row["CostPrice"]);
                    Techno.SellingPrice = Convert.ToDouble(row["SellingPrice"]);
                    //Techno.ManufacturerID = (int)row["ManufacturerID"];
                    //Techno.TechCategoryID = (int)row["TechCategoryID"];
                    Techno.ImageFront = row["ImageFront"].ToString();
                    Techno.ImageTop = row["ImageTop"].ToString();
                    Techno.ImageSide = row["ImageSide"].ToString();
                }
            }
            return Techno;
        }
        public bool UpdateTechnologyProduct(Technology TechnoProduct)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@ProductID", TechnoProduct.ProductID ),
                new SqlParameter("@ProductTitle", TechnoProduct.ModelName),
                new SqlParameter("@Specs", TechnoProduct.Specs),
                new SqlParameter("@CostPrice", TechnoProduct.CostPrice),
                //new SqlParameter("@MarkUp", company.MarkUp),
                new SqlParameter("@SellingPrice", TechnoProduct.SellingPrice),
                new SqlParameter("@SupplierID", TechnoProduct.SupplierID),
                new SqlParameter("@DateAdded", TechnoProduct.DateAdded),
                new SqlParameter("@TechnologyID", TechnoProduct.TechID),
                new SqlParameter("@Manufacturer", TechnoProduct.ManufacturerID),
                new SqlParameter("@TechType", TechnoProduct.TechCategoryID),
                new SqlParameter("@SerialNumber", TechnoProduct.ModelNumber)
            };
            return DataProvider.ExecuteNonQuery("sp_UpdateProduct", CommandType.StoredProcedure,
                Params);
        }

        public bool InsertTechnology(Technology TechnoProduct)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@ProductID", TechnoProduct.ProductID),
                new SqlParameter("@ModelName", TechnoProduct.ModelName),
                new SqlParameter("@ModelNumber", TechnoProduct.ModelNumber),
                new SqlParameter("@Specs", TechnoProduct.Specs),
                new SqlParameter("@ManufacturerID", TechnoProduct.ManufacturerID),
                new SqlParameter("@TechCategoryID", TechnoProduct.TechCategoryID),
                new SqlParameter("@SupplierID", TechnoProduct.SupplierID),
                new SqlParameter("@ImageFront", TechnoProduct.ImageFront),
                new SqlParameter("@ImageTop", TechnoProduct.ImageTop),
                new SqlParameter("@ImageSide", TechnoProduct.ImageSide)
            };
            return DataProvider.ExecuteNonQuery("sp_InsertTechnology", CommandType.StoredProcedure,
                Params);
        }

        //public bool InsertTechnologyProduct(Technology TechnoProduct)
        //{

        //    SqlParameter[] Params = new SqlParameter[]
        //    {
        //        new SqlParameter("@CostPrice", TechnoProduct.CostPrice),
        //        new SqlParameter("@SellingPrice", TechnoProduct.SellingPrice),
        //        new SqlParameter("@DateAdded", TechnoProduct.DateAdded),
        //        new SqlParameter("@IsBook", TechnoProduct.IsBook = false)
        //    };
        //    return DataProvider.ExecuteNonQuery("sp_InsertProduct", CommandType.StoredProcedure,
        //        Params);
        //}

        public bool UpdateTechnology(Technology TechnoProduct)
        {

            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@ProductID", TechnoProduct.ProductID),
                new SqlParameter("@TechnologyID", TechnoProduct.TechID),
                new SqlParameter("@ManufacturerID", TechnoProduct.ManufacturerID),
                new SqlParameter("@TechType", TechnoProduct.TechCategoryID),
                new SqlParameter("@ModelNumber", TechnoProduct.ModelNumber)
            };
            return DataProvider.ExecuteNonQuery("sp_UpdateTechnology", CommandType.StoredProcedure,
                Params);
        }

        public bool DeleteTechnologyProduct(int ProductID)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@_ProductID", ProductID)
            };
            return DataProvider.ExecuteNonQuery("sp_DeleteProduct", CommandType.StoredProcedure,
                Params);
        }//STORED PROCEDURE

        //STORED PROCEDURE

        #endregion
    }
}

