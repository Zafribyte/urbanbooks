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

        public List<Technology> TechnologyByCategory(int CategoryID)
        {
            List<Technology> TechnologyList = null;
            SqlParameter[] Params = { new SqlParameter("@CategoryID", CategoryID) };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_TechnologyByCategory", CommandType.StoredProcedure, Params))
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
                        Techno.ManufacturerID = (int)row["ManufacturerID"];
                        Techno.TechCategoryID = (int)row["TechCategoryID"];
                        Techno.SellingPrice = Convert.ToDouble(row["SellingPrice"]);
                        Techno.ImageFront = row["ImageFront"].ToString();
                        TechnologyList.Add(Techno);
                    }
                }
            }
            return TechnologyList;
        }

        #region Search
        public List<Technology> TechnologyGlobalSearch(string query)
        {
            List<Technology> TechnologyList = null;
            SqlParameter[] Params = { new SqlParameter("@Search", query) };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_TechnologyGlobalSearch", CommandType.StoredProcedure, Params))
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
                        Techno.ManufacturerID = (int)row["ManufacturerID"];
                        Techno.TechCategoryID = (int)row["TechCategoryID"];
                        Techno.SellingPrice = Convert.ToDouble(row["SellingPrice"]);
                        Techno.ImageFront = row["ImageFront"].ToString();
                        TechnologyList.Add(Techno);
                    }
                }
            }
            return TechnologyList;
        }

        public List<Technology> ModelNameDeviceSearch(string query)
        {
            List<Technology> TechnologyList = null;
            SqlParameter[] Params = { new SqlParameter("@Search", query) };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_DeviceModelNameSearch", CommandType.StoredProcedure, Params))
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
                        Techno.ModelNumber = row["ModelNumber"].ToString();
                        Techno.ManufacturerID = (int)row["ManufacturerID"];
                        Techno.TechCategoryID = (int)row["TechCategoryID"];
                        Techno.SellingPrice = Convert.ToDouble(row["SellingPrice"]);
                        Techno.ImageFront = row["ImageFront"].ToString();
                        TechnologyList.Add(Techno);
                    }
                }
            }
            return TechnologyList;
        }

        public List<Technology> ModelNumberDeviceSearch(string query)
        {
            List<Technology> TechnologyList = null;
            SqlParameter[] Params = { new SqlParameter("@Search", query) };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_DeviceModelNumberSearch", CommandType.StoredProcedure, Params))
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
                        Techno.ModelNumber = row["ModelNumber"].ToString();
                        Techno.ManufacturerID = (int)row["ManufacturerID"];
                        Techno.TechCategoryID = (int)row["TechCategoryID"];
                        Techno.SellingPrice = Convert.ToDouble(row["SellingPrice"]);
                        Techno.ImageFront = row["ImageFront"].ToString();
                        TechnologyList.Add(Techno);
                    }
                }
            }
            return TechnologyList;
        }

        public List<Technology> ManufacturerDeviceSearch(string query)
        {
            List<Technology> TechnologyList = null;
            SqlParameter[] Params = { new SqlParameter("@Search", query) };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_DeviceManufacturerSearch", CommandType.StoredProcedure, Params))
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
                        Techno.ModelNumber = row["ModelNumber"].ToString();
                        Techno.ManufacturerID = (int)row["ManufacturerID"];
                        Techno.TechCategoryID = (int)row["TechCategoryID"];
                        Techno.SellingPrice = Convert.ToDouble(row["SellingPrice"]);
                        Techno.ImageFront = row["ImageFront"].ToString();
                        TechnologyList.Add(Techno);
                    }
                }
            }
            return TechnologyList;
        }

        public List<Technology> DeviceCategorySearch(string query)
        {
            List<Technology> TechnologyList = null;
            SqlParameter[] Params = { new SqlParameter("@Search", query) };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_DeviceCategorySearch", CommandType.StoredProcedure, Params))
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
                        Techno.ModelNumber = row["ModelNumber"].ToString();
                        Techno.ManufacturerID = (int)row["ManufacturerID"];
                        Techno.TechCategoryID = (int)row["TechCategoryID"];
                        Techno.SellingPrice = Convert.ToDouble(row["SellingPrice"]);
                        Techno.ImageFront = row["ImageFront"].ToString();
                        TechnologyList.Add(Techno);
                    }
                }
            }
            return TechnologyList;
        }

        public List<Technology> DeviceCategoryFromQuerySearch(string query, double FromPrice)
        {
            List<Technology> TechnologyList = null;
            SqlParameter[] Params = { 
                                        new SqlParameter("@Search", query),
                                        new SqlParameter("@FromPrice", FromPrice)
                                    };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_DeviceCategoryFromQuerySearch", CommandType.StoredProcedure, Params))
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
                        Techno.ModelNumber = row["ModelNumber"].ToString();
                        Techno.ManufacturerID = (int)row["ManufacturerID"];
                        Techno.TechCategoryID = (int)row["TechCategoryID"];
                        Techno.SellingPrice = Convert.ToDouble(row["SellingPrice"]);
                        Techno.ImageFront = row["ImageFront"].ToString();
                        TechnologyList.Add(Techno);
                    }
                }
            }
            return TechnologyList;
        }

        public List<Technology> DeviceManufacturerFromQuerySearch(string query, double FromPrice)
        {
            List<Technology> TechnologyList = null;
            SqlParameter[] Params = { 
                                        new SqlParameter("@Search", query),
                                        new SqlParameter("@FromPrice", FromPrice)
                                    };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_DeviceManufacturerFromQuerySearch", CommandType.StoredProcedure, Params))
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
                        Techno.ModelNumber = row["ModelNumber"].ToString();
                        Techno.ManufacturerID = (int)row["ManufacturerID"];
                        Techno.TechCategoryID = (int)row["TechCategoryID"];
                        Techno.SellingPrice = Convert.ToDouble(row["SellingPrice"]);
                        Techno.ImageFront = row["ImageFront"].ToString();
                        TechnologyList.Add(Techno);
                    }
                }
            }
            return TechnologyList;
        }

        public List<Technology> DeviceModelNumberFromQuerySearch(string query, double FromPrice)
        {
            List<Technology> TechnologyList = null;
            SqlParameter[] Params = { 
                                        new SqlParameter("@Search", query),
                                        new SqlParameter("@FromPrice", FromPrice)
                                    };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_DeviceModelNumberFromQuerySearch", CommandType.StoredProcedure, Params))
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
                        Techno.ModelNumber = row["ModelNumber"].ToString();
                        Techno.ManufacturerID = (int)row["ManufacturerID"];
                        Techno.TechCategoryID = (int)row["TechCategoryID"];
                        Techno.SellingPrice = Convert.ToDouble(row["SellingPrice"]);
                        Techno.ImageFront = row["ImageFront"].ToString();
                        TechnologyList.Add(Techno);
                    }
                }
            }
            return TechnologyList;
        }

        public List<Technology> DeviceModelNameFromQuerySearch(string query, double FromPrice)
        {
            List<Technology> TechnologyList = null;
            SqlParameter[] Params = { 
                                        new SqlParameter("@Search", query),
                                        new SqlParameter("@FromPrice", FromPrice)
                                    };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_DeviceModelNameFromQuerySearch", CommandType.StoredProcedure, Params))
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
                        Techno.ModelNumber = row["ModelNumber"].ToString();
                        Techno.ManufacturerID = (int)row["ManufacturerID"];
                        Techno.TechCategoryID = (int)row["TechCategoryID"];
                        Techno.SellingPrice = Convert.ToDouble(row["SellingPrice"]);
                        Techno.ImageFront = row["ImageFront"].ToString();
                        TechnologyList.Add(Techno);
                    }
                }
            }
            return TechnologyList;
        }

        public List<Technology> DeviceModelNameToQuerySearch(string query, double ToPrice)
        {
            List<Technology> TechnologyList = null;
            SqlParameter[] Params = { 
                                        new SqlParameter("@Search", query),
                                        new SqlParameter("@ToPrice", ToPrice)
                                    };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_DeviceModelNameToQuerySearch", CommandType.StoredProcedure, Params))
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
                        Techno.ModelNumber = row["ModelNumber"].ToString();
                        Techno.ManufacturerID = (int)row["ManufacturerID"];
                        Techno.TechCategoryID = (int)row["TechCategoryID"];
                        Techno.SellingPrice = Convert.ToDouble(row["SellingPrice"]);
                        Techno.ImageFront = row["ImageFront"].ToString();
                        TechnologyList.Add(Techno);
                    }
                }
            }
            return TechnologyList;
        }

        public List<Technology> DeviceModelNumberToQuerySearch(string query, double ToPrice)
        {
            List<Technology> TechnologyList = null;
            SqlParameter[] Params = { 
                                        new SqlParameter("@Search", query),
                                        new SqlParameter("@ToPrice", ToPrice)
                                    };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_DeviceModelNumberToQuerySearch", CommandType.StoredProcedure, Params))
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
                        Techno.ModelNumber = row["ModelNumber"].ToString();
                        Techno.ManufacturerID = (int)row["ManufacturerID"];
                        Techno.TechCategoryID = (int)row["TechCategoryID"];
                        Techno.SellingPrice = Convert.ToDouble(row["SellingPrice"]);
                        Techno.ImageFront = row["ImageFront"].ToString();
                        TechnologyList.Add(Techno);
                    }
                }
            }
            return TechnologyList;
        }

        public List<Technology> DeviceManufacturerToQuerySearch(string query, double ToPrice)
        {
            List<Technology> TechnologyList = null;
            SqlParameter[] Params = { 
                                        new SqlParameter("@Search", query),
                                        new SqlParameter("@ToPrice", ToPrice)
                                    };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_DeviceManufacturerToQuerySearch", CommandType.StoredProcedure, Params))
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
                        Techno.ModelNumber = row["ModelNumber"].ToString();
                        Techno.ManufacturerID = (int)row["ManufacturerID"];
                        Techno.TechCategoryID = (int)row["TechCategoryID"];
                        Techno.SellingPrice = Convert.ToDouble(row["SellingPrice"]);
                        Techno.ImageFront = row["ImageFront"].ToString();
                        TechnologyList.Add(Techno);
                    }
                }
            }
            return TechnologyList;
        }

        public List<Technology> DeviceCategoryToQuerySearch(string query, double ToPrice)
        {
            List<Technology> TechnologyList = null;
            SqlParameter[] Params = { 
                                        new SqlParameter("@Search", query),
                                        new SqlParameter("@ToPrice", ToPrice)
                                    };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_DeviceCategoryToQuerySearch", CommandType.StoredProcedure, Params))
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
                        Techno.ModelNumber = row["ModelNumber"].ToString();
                        Techno.ManufacturerID = (int)row["ManufacturerID"];
                        Techno.TechCategoryID = (int)row["TechCategoryID"];
                        Techno.SellingPrice = Convert.ToDouble(row["SellingPrice"]);
                        Techno.ImageFront = row["ImageFront"].ToString();
                        TechnologyList.Add(Techno);
                    }
                }
            }
            return TechnologyList;
        }

        public List<Technology> DeviceCategoryBETWEENQuerySearch(string query,double FromPrice, double ToPrice)
        {
            List<Technology> TechnologyList = null;
            SqlParameter[] Params = { 
                                        new SqlParameter("@Search", query),
                                        new SqlParameter("@FromPrice", FromPrice),
                                        new SqlParameter("@ToPrice", ToPrice)
                                    };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_DeviceCategoryBETWEENQuerySearch", CommandType.StoredProcedure, Params))
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
                        Techno.ModelNumber = row["ModelNumber"].ToString();
                        Techno.ManufacturerID = (int)row["ManufacturerID"];
                        Techno.TechCategoryID = (int)row["TechCategoryID"];
                        Techno.SellingPrice = Convert.ToDouble(row["SellingPrice"]);
                        Techno.ImageFront = row["ImageFront"].ToString();
                        TechnologyList.Add(Techno);
                    }
                }
            }
            return TechnologyList;
        }

        public List<Technology> DeviceManufacturerBETWEENQuerySearch(string query, double FromPrice, double ToPrice)
        {
            List<Technology> TechnologyList = null;
            SqlParameter[] Params = { 
                                        new SqlParameter("@Search", query),
                                        new SqlParameter("@FromPrice", FromPrice),
                                        new SqlParameter("@ToPrice", ToPrice)
                                    };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_DeviceManufacturerBETWEENQuerySearch", CommandType.StoredProcedure, Params))
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
                        Techno.ModelNumber = row["ModelNumber"].ToString();
                        Techno.ManufacturerID = (int)row["ManufacturerID"];
                        Techno.TechCategoryID = (int)row["TechCategoryID"];
                        Techno.SellingPrice = Convert.ToDouble(row["SellingPrice"]);
                        Techno.ImageFront = row["ImageFront"].ToString();
                        TechnologyList.Add(Techno);
                    }
                }
            }
            return TechnologyList;
        }

        public List<Technology> DeviceModelNumberBETWEENQuerySearch(string query, double FromPrice, double ToPrice)
        {
            List<Technology> TechnologyList = null;
            SqlParameter[] Params = { 
                                        new SqlParameter("@Search", query),
                                        new SqlParameter("@FromPrice", FromPrice),
                                        new SqlParameter("@ToPrice", ToPrice)
                                    };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_DeviceModelNumberBETWEENQuerySearch", CommandType.StoredProcedure, Params))
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
                        Techno.ModelNumber = row["ModelNumber"].ToString();
                        Techno.ManufacturerID = (int)row["ManufacturerID"];
                        Techno.TechCategoryID = (int)row["TechCategoryID"];
                        Techno.SellingPrice = Convert.ToDouble(row["SellingPrice"]);
                        Techno.ImageFront = row["ImageFront"].ToString();
                        TechnologyList.Add(Techno);
                    }
                }
            }
            return TechnologyList;
        }

        public List<Technology> DeviceModelNameBETWEENQuerySearch(string query, double FromPrice, double ToPrice)
        {
            List<Technology> TechnologyList = null;
            SqlParameter[] Params = { 
                                        new SqlParameter("@Search", query),
                                        new SqlParameter("@FromPrice", FromPrice),
                                        new SqlParameter("@ToPrice", ToPrice)
                                    };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_DeviceModelNameBETWEENQuerySearch", CommandType.StoredProcedure, Params))
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
                        Techno.ModelNumber = row["ModelNumber"].ToString();
                        Techno.ManufacturerID = (int)row["ManufacturerID"];
                        Techno.TechCategoryID = (int)row["TechCategoryID"];
                        Techno.SellingPrice = Convert.ToDouble(row["SellingPrice"]);
                        Techno.ImageFront = row["ImageFront"].ToString();
                        TechnologyList.Add(Techno);
                    }
                }
            }
            return TechnologyList;
        }


        #endregion

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


        public Technology experimentalUpdate(Technology tech)
        {
            Technology tec;
            SqlParameter[] Params = {
                                        new SqlParameter("@CostPrice", tech.CostPrice),
                                        new SqlParameter("@SellingPrice", tech.SellingPrice),
                                        new SqlParameter("@IsBook", tech.IsBook = false),
                                        new SqlParameter("@DateAdded", tech.DateAdded)
                                    };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_NewManhattanProject", CommandType.StoredProcedure, Params))
            {
                tec = new Technology();
                if(table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    tec.ProductID = Convert.ToInt32(row["ProductID"]);
                }
            }

            return tec;
        }
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

        public Technology GetTechnologyDetails(int ProductID)
        {
            Technology Techno = null;

            SqlParameter[] Params = { new SqlParameter("@ProductID", ProductID) };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_ViewSpecificTechAdmin",
                CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    Techno = new Technology();
                    Techno.TechID = Convert.ToInt32(row["TechID"]);
                    Techno.ProductID = Convert.ToInt32(row["ProductID"]);
                    Techno.ModelName = row["ModelName"].ToString();
                    Techno.Specs = row["Specs"].ToString();
                    Techno.ModelNumber = row["ModelNumber"].ToString();
                    Techno.CostPrice = Convert.ToDouble(row["CostPrice"]);
                    Techno.SellingPrice = Convert.ToDouble(row["SellingPrice"]);
                    Techno.ManufacturerID = (int)row["ManufacturerID"];
                    Techno.TechCategoryID = (int)row["TechCategoryID"];
                    Techno.ImageFront = row["ImageFront"].ToString();
                    Techno.ImageTop = row["ImageTop"].ToString();
                    Techno.ImageSide = row["ImageSide"].ToString();
                }
            }
            return Techno;
        }
        public bool UpdateTechnologyProduct(Technology TechnoProduct)
        {
            Technology tc;
            SqlParameter[] Params = {
                                        new SqlParameter("@CostPrice", TechnoProduct.CostPrice),
                                        new SqlParameter("@SellingPrice", TechnoProduct.SellingPrice),
                                        new SqlParameter("@DateAdded", TechnoProduct.DateAdded),
                                        new SqlParameter("@IsBook", false)
                                    };
            return DataProvider.ExecuteNonQuery("sp_UpdateProduct", CommandType.StoredProcedure, Params);
        }
        public bool UpdateTechnology(Technology TechnoProduct)
        {

            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@ProductID", TechnoProduct.ProductID),
                new SqlParameter("@ModelName", TechnoProduct.ModelName),
                new SqlParameter("@ModelNumber", TechnoProduct.ModelNumber),
                new SqlParameter("@Specs", TechnoProduct.Specs),
                new SqlParameter("@TechID", TechnoProduct.TechID),
                new SqlParameter("@ManufacturerID", TechnoProduct.ManufacturerID),
                new SqlParameter("@TechCategoryID", TechnoProduct.TechCategoryID),
                new SqlParameter("@SupplierID", TechnoProduct.SupplierID),
                new SqlParameter("@ImageFront", TechnoProduct.ImageFront),
                new SqlParameter("@ImageTop", TechnoProduct.ImageTop),
                new SqlParameter("@ImageSide", TechnoProduct.ImageSide),
            };
            return DataProvider.ExecuteNonQuery("sp_UpdateTechnology", CommandType.StoredProcedure,
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
                new SqlParameter("@ImageSide", TechnoProduct.ImageSide),
                new SqlParameter("@IsBook", false)
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

        public bool DeleteTechnologyProduct(int ProductID)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@_ProductID", ProductID)
            };
            return DataProvider.ExecuteNonQuery("sp_DeleteProduct", CommandType.StoredProcedure,
                Params);
        }

        public List<Technology> GetNewDeviceList()
        {
            List<Technology> TechnologyList = null;

            using (DataTable table = DataProvider.ExecuteSelectCommand("sp_ViewAllNewDevices",
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
                        Techno.SellingPrice = Convert.ToDouble(row["SellingPrice"]);
                        Techno.ImageFront = row["ImageFront"].ToString();
                        TechnologyList.Add(Techno);
                    }
                }
            }
            return TechnologyList;
        }

        #endregion
    }
}