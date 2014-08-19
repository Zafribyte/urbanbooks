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
        Keyword keys = new Keyword();
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
                        Techno.ProductID = Convert.ToInt32(row["ProductID"]);
                        Techno.ModelName = row["ModelName"].ToString();
                        Techno.SellingPrice = Convert.ToDouble(row["SellingPrice"]);
                        Techno.Specifications = row["Specs"].ToString();
                        Techno.TechCategoryID= (int)row["TechCategoryID"];
                        Techno.ModelNumber = row["ModelNumber"].ToString();
                        Techno.ManufacturerID = Convert.ToInt32(row["ManufacturerID"]);
                        if (row["ImageFront"] != DBNull.Value)
                        { Techno.ImageFront = (byte)row["ImageFront"]; }
                        if (row["ImageSide"] != DBNull.Value)
                        { Techno.ImageTop = (byte)row["ImageSide"]; }
                        if (row["ImageTop"] != DBNull.Value)
                        { Techno.ImageSide = (byte)row["ImageTop"]; }
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

            SqlParameter[] Params = { new SqlParameter("@_ProductID", ProductID) };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_ViewSpecificTechUser",
                CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    Techno = new Technology();
                    Techno.ProductID = Convert.ToInt32(row["ProductID"]);
                    Techno.Specifications = row["Description"].ToString();
                    Techno.SellingPrice = Convert.ToDouble(row["SellingPrice"]);
                    Techno.TechID = Convert.ToInt32(row["TechnologyID"]);
                    Techno.Manufacturer = row["Manufacturer"].ToString();
                    Techno.ModelNumber = row["ModelNumber"].ToString();
                    if (row["ProductImageFront"] != DBNull.Value)
                    { Techno.ImageFront = (byte)row["ProductImageFront"]; }
                    if (row["ProductImageTop"] != DBNull.Value)
                    { Techno.ImageTop = (byte)row["ProductImageTop"]; }
                    if (row["ProductImageSide"] != DBNull.Value)
                    { Techno.ImageSide = (byte)row["ProductImageSide"]; }
                }
            }
            return Techno;
        }//STORED PROCEDURE
        #endregion

        #region Admin

        public bool UpdateTechnologyProduct(Technology TechnoProduct)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@ProductID", TechnoProduct.ProductID ),
                new SqlParameter("@ProductTitle", TechnoProduct.ModelName),
                new SqlParameter("@Description", TechnoProduct.Specifications),
                new SqlParameter("@CostPrice", TechnoProduct.CostPrice),
                new SqlParameter("@MarkUp", company.MarkUp),
                new SqlParameter("@SellingPrice", TechnoProduct.SellingPrice),
                new SqlParameter("@SupplierID", TechnoProduct.SupplierID),
                new SqlParameter("@DateAdded", TechnoProduct.DateAdded),
                new SqlParameter("@KeyWords", keys.KeywordID), //Note
                //SOME MISSING PARAMS
                new SqlParameter("@TechnologyID", TechnoProduct.TechID),
                new SqlParameter("@Make", TechnoProduct.Manufacturer),
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
                new SqlParameter("@TechnologyID", TechnoProduct.TechID ),
                new SqlParameter("@ModelNumber", TechnoProduct.ModelNumber),
                new SqlParameter("@Manufacturer", TechnoProduct.Manufacturer),
                new SqlParameter("@TechnologyTypeID", TechnoProduct.TechCategoryID)
            };
            return DataProvider.ExecuteNonQuery("sp_InsertTechnology", CommandType.StoredProcedure,
                Params);
        }

        public bool InsertTechnologyProduct(Technology TechnoProduct)
        {

            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@ProductTitle", TechnoProduct.ModelName),
                new SqlParameter("@Description", TechnoProduct.Specifications),
                new SqlParameter("@CostPrice", TechnoProduct.CostPrice),
                new SqlParameter("@MarkUp", company.MarkUp),
                new SqlParameter("@SellingPrice", TechnoProduct.SellingPrice),
                new SqlParameter("@SupplierID", TechnoProduct.SupplierID),
                new SqlParameter("@DateAdded", TechnoProduct.DateAdded),
                new SqlParameter("@KeyWords", keys.KeywordID),
            };
            return DataProvider.ExecuteNonQuery("sp_InsertProduct", CommandType.StoredProcedure,
                Params);
        }

        public bool UpdateTechnology(Technology TechnoProduct)
        {

            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@ProductID", TechnoProduct.ProductID),
                new SqlParameter("@TechnologyID", TechnoProduct.TechID),
                new SqlParameter("@Manufacturer", TechnoProduct.Manufacturer),
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

        public Technology GetTechnologyDetails(int ProductID)
        {
            Technology Techno = null;

            SqlParameter[] Params = { new SqlParameter("@_ProductID", ProductID) };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_ViewSpecificTechnology",
                CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    Techno = new Technology();
                    Techno.ProductID = Convert.ToInt32(row["ProductID"]);
                    Techno.Specifications = row["Description"].ToString();
                    Techno.CostPrice = Convert.ToDouble(row["CostPrice"]);
                    company.MarkUp = Convert.ToDouble(row["MarkUp"]);
                    Techno.SellingPrice = Convert.ToDouble(row["SellingPrice"]);
                    Techno.TechID = Convert.ToInt32(row["TechnologyID"]);
                    Techno.Manufacturer = row["Manufacturer"].ToString();
                    Techno.ModelNumber = row["ModelNumber"].ToString();
                    if (row["ProductImageFront"] != DBNull.Value)
                    { Techno.ImageFront = (byte)row["ProductImageFront"]; }
                    if (row["ProductImageLeft"] != DBNull.Value)
                    { Techno.ImageTop = (byte)row["ProductImageLeft"]; }
                    if (row["ProductImageRight"] != DBNull.Value)
                    { Techno.ImageSide = (byte)row["ProductImageRight"]; }
                }
            }
            return Techno;
        }//STORED PROCEDURE

        #endregion
    }
}
