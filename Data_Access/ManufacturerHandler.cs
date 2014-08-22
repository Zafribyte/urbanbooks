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
                        manu.ManufacturerID = Convert.ToInt32(row["KeywordID"]);
                        manu.Name = row["Keywords"].ToString();                       
                       // manu.ProductID = Convert.ToInt32(row["ProductID"]);
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
                new SqlParameter("@ManufactureID", manu.ManufacturerID),
                new SqlParameter("@Name", manu.Name),
              //  new SqlParameter("@ProductID", manu.ProductID),
            };
            return DataProvider.ExecuteNonQuery("sp_InsertManufacturer", CommandType.StoredProcedure,
                Params);
        }
    }
}
