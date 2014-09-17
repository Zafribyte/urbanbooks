using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace urbanbooks
{
    public class ManufacturerHandler
    {
        public List<Manufacturer> GetManufacturerList()
        {
            List<Manufacturer> ManufacturerList = null;

            using (DataTable table = DataProvider.ExecuteSelectCommand("sp_ViewAllManufacturers", //*Note
                CommandType.StoredProcedure))
            {
                if (table.Rows.Count > 0)
                {
                    ManufacturerList = new List<Manufacturer>();
                    foreach (DataRow row in table.Rows)
                    {
                        Manufacturer manu = new Manufacturer();
                        manu.ManufacturerID = Convert.ToInt32(row["ManufacturerID"]);
                        manu.Name = row["Manufacturer"].ToString();
                        ManufacturerList.Add(manu);
                    }
                }
            }
            return ManufacturerList;
        }

        public bool DeleteManufacturer(int ManufacturerID)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@ManufacturerID", ManufacturerID)
            };
            return DataProvider.ExecuteNonQuery("sp_DeleteManufacturer", CommandType.StoredProcedure,
                Params);
        }

        public bool InsertManufacturer(Manufacturer manu)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@Name", manu.Name)
            };
            return DataProvider.ExecuteNonQuery("sp_InsertManufacturer", CommandType.StoredProcedure,
                Params);
        }
        public bool UpdateManufacturer(Manufacturer manufacturer)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@PublisherID", manufacturer.ManufacturerID ),
                new SqlParameter("@Name", manufacturer.Name)
            };
            return DataProvider.ExecuteNonQuery("sp_UpdateManufacturer", CommandType.StoredProcedure,
                Params);
        }

        public Manufacturer GetManufacturerDetails(int ManufacturerID)
        {
            Manufacturer manufacturer = null;

            SqlParameter[] Params = { new SqlParameter("@ManufacturerID", ManufacturerID) };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_ViewSpecificManufacturer",
                CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    manufacturer = new Manufacturer();
                    manufacturer.ManufacturerID = Convert.ToInt32(row["ManufacturerID"]);
                    manufacturer.Name = row["Name"].ToString();
                }
            }
            return manufacturer;
        }
    }
}
