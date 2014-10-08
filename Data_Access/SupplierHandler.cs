using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace urbanbooks
{
    public class SupplierHandler
    {
        #region Admin
        public List<Supplier> GetBookSupplierList()
        {
            List<Supplier> SupplierList = null;

            using (DataTable table = DataProvider.ExecuteSelectCommand("sp_ViewAllBookSuppliers",
                CommandType.StoredProcedure))
            {
                if (table.Rows.Count > 0)
                {
                    SupplierList = new List<Supplier>();
                    foreach (DataRow row in table.Rows)
                    {
                        Supplier supplier = new Supplier();
                        supplier.SupplierID = Convert.ToInt32(row["SupplierID"]);
                        supplier.Name = row["Name"].ToString();
                        supplier.LastName = row["LastName"].ToString();
                        supplier.Fax = row["Fax"].ToString();
                        supplier.ContactPerson = row["ContactPerson"].ToString();
                        supplier.ContactPersonNumber = row["ContactPersonNumber"].ToString();
                        SupplierList.Add(supplier);
                    }
                }
            }
            return SupplierList;
        }
        public List<Supplier> GetTechSupplierList()
        {
            List<Supplier> SupplierList = null;

            using (DataTable table = DataProvider.ExecuteSelectCommand("sp_ViewAllTechSuppliers",
                CommandType.StoredProcedure))
            {
                if (table.Rows.Count > 0)
                {
                    SupplierList = new List<Supplier>();
                    foreach (DataRow row in table.Rows)
                    {
                        Supplier supplier = new Supplier();
                        supplier.SupplierID = Convert.ToInt32(row["SupplierID"]);
                        supplier.Name = row["Name"].ToString();
                        supplier.LastName = row["LastName"].ToString();
                        supplier.Fax = row["Fax"].ToString();
                        supplier.ContactPerson = row["ContactPerson"].ToString();
                        supplier.ContactPersonNumber = row["ContactPersonNumber"].ToString();
                        SupplierList.Add(supplier);
                    }
                }
            }
            return SupplierList;
        }
        public Supplier GetSupplierDetails(int SupplierID)
        {
            Supplier supplier = null;

            SqlParameter[] Params = { new SqlParameter("@SupplierID", SupplierID) };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_ViewSpecificSupplier",
                CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    supplier = new Supplier();
                    supplier.SupplierID = Convert.ToInt32(row["SupplierID"]);
                    supplier.Name = row["Name"].ToString();
                    supplier.LastName = row["LastName"].ToString();
                    supplier.Fax = row["Fax"].ToString();
                    supplier.ContactPerson = row["ContactPerson"].ToString();
                    supplier.ContactPersonNumber = row["ContactPersonNumber"].ToString();
                    supplier.IsBookSupplier = Convert.ToBoolean(row["IsBookSupplier"]);
                }
            }
            return supplier;
        }


        public Supplier GetSupplier(string User_Id)
        {
            Supplier supplier = null;

            SqlParameter[] Params = { new SqlParameter("@User_Id", User_Id) };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_ViewSpecificUserSupplier",
                CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    supplier = new Supplier();
                    supplier.SupplierID = Convert.ToInt32(row["SupplierID"]);
                    supplier.Name = row["Name"].ToString();
                    supplier.LastName = row["LastName"].ToString();
                    supplier.Fax = row["Fax"].ToString();
                    supplier.ContactPerson = row["ContactPerson"].ToString();
                    supplier.ContactPersonNumber = row["ContactPersonNumber"].ToString();
                }
            }
            return supplier;
        }

        public bool DeleteSupplierProduct(int SupplierID)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@SupplierID", SupplierID)
            };
            return DataProvider.ExecuteNonQuery("sp_DeleteSupplier", CommandType.StoredProcedure,
                Params);
        }

        public bool InsertSupplier(Supplier supplier)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@Name", supplier.Name ),
                new SqlParameter("@Telephone", supplier.LastName),
                new SqlParameter("@Fax", supplier.Fax),
                new SqlParameter("@ContactPerson", supplier.ContactPerson),
                new SqlParameter("@ContactPersonTel", supplier.ContactPersonNumber),
            };
            return DataProvider.ExecuteNonQuery("sp_InsertSupplier", CommandType.StoredProcedure,
                Params);
        }

        public bool UpdateSupplier(Supplier supplier)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter ("@SupplierID", supplier.SupplierID),
                new SqlParameter("@Name", supplier.Name ),
                new SqlParameter("@Telephone", supplier.LastName),
                new SqlParameter("@Fax", supplier.Fax),
                new SqlParameter("@ContactPerson", supplier.ContactPerson),
                new SqlParameter("@ContactPersonTel", supplier.ContactPersonNumber),
            };
            return DataProvider.ExecuteNonQuery("sp_UpdateSupplier", CommandType.StoredProcedure,
                Params);
        }

        public Supplier SearchSupplier(string Query)
        {
            Supplier supplier = null;
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@Name", Query) ///SEARCH
            };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_SearchProduct",
                CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    supplier = new Supplier();
                    supplier.SupplierID = Convert.ToInt32(row["SupplierID"]);
                    supplier.Name = row["Name"].ToString();
                    supplier.LastName = row["LastName"].ToString();
                    supplier.Fax = row["Fax"].ToString();
                    supplier.ContactPerson = row["ContactPerson"].ToString();
                    supplier.ContactPersonNumber = row["ContactPersonNumber"].ToString();

                }
            }
            return supplier;
        }
        #endregion
    }
}
