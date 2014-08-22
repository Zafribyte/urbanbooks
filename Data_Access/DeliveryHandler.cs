using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace urbanbooks
{
    public class DeliveryHandler
    {
        public List<Delivery> GetDeliveryList()
        {
            List<Delivery> DeliveryList = null;

            using (DataTable table = DataProvider.ExecuteSelectCommand("sp_ViewDeliveries",
                CommandType.StoredProcedure))
            {
                if (table.Rows.Count > 0)
                {
                    DeliveryList = new List<Delivery>();
                    foreach (DataRow row in table.Rows)
                    {
                        Delivery delivery = new Delivery();
                        delivery.DeliveryServiceID = Convert.ToInt32(row["DeliveryServiceID"]);
                        delivery.ServiceName = row["Service"].ToString();
                        delivery.Price =Convert.ToDouble(row["Price"]);
                        DeliveryList.Add(delivery);
                    }
                }
                return DeliveryList;
            }
        }

        public Delivery GetDeliveryDetails(int deliveryId)
        {
            Delivery delivery = null;

            SqlParameter[] Params = { new SqlParameter("@DeliveryID", deliveryId) };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_ViewSpecificDelivery",
                CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    delivery = new Delivery();
                    delivery.DeliveryServiceID = Convert.ToInt32(row["DeliveryServiceID"]);
                    delivery.ServiceName = row["ServiceName"].ToString();
                    delivery.ServiceType = row["ServiceType"].ToString();
                    delivery.Price = Convert.ToDouble(row["Price"]);
                }
            }
            return delivery;
        }

        public bool DeleteDelivery(int DeliveryId)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@DeliveryID", DeliveryId)
            };
            return DataProvider.ExecuteNonQuery("sp_DeleteDelivery", CommandType.StoredProcedure,
                Params);
        }

        public bool InsertDelivery(Delivery delivery)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@DeliveryName", delivery.ServiceName ),
                new SqlParameter("@DeliveryType", delivery.ServiceType),
                new SqlParameter("@Price", delivery.Price)
            };
            return DataProvider.ExecuteNonQuery("sp_InsertDelivery", CommandType.StoredProcedure,
                Params);
        }

        public bool UpdateDelivery(Delivery delivery)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@DeliveryID", delivery.DeliveryServiceID),
                new SqlParameter("@DeliveryName",delivery.ServiceName),
                new SqlParameter("@DeliveryType",delivery.ServiceType),
                new SqlParameter("@DeliveryPrice",delivery.Price)
            };
            return DataProvider.ExecuteNonQuery("sp_UpdateDelivery", CommandType.StoredProcedure,
                Params);
        }
    }
}
