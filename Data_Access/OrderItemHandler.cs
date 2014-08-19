using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace urbanbooks
{
    public class OrderItemHandler
    {
        public List<OrderItem> GetOrderItemList()
        {
            List<OrderItem> orderItems = null;

            using (DataTable table = DataProvider.ExecuteSelectCommand("sp_ViewOrderItems",
                CommandType.StoredProcedure))
            {
                if (table.Rows.Count > 0)
                {
                    orderItems = new List<OrderItem>();
                    foreach (DataRow row in table.Rows)
                    {
                        OrderItem orderItem = new OrderItem();
                        orderItem.OrderItemNumber = Convert.ToInt32(row["OrderItemNumber"]);
                        orderItem.ProductID = Convert.ToInt32(row["ProductID"]);
                        orderItem.Quantity = Convert.ToInt32(row["Quantity"]);
                        orderItem.OrderNo = Convert.ToInt32(row["OrderNo"]);
                        orderItems.Add(orderItem);
                    }
                }
            }
            return orderItems;
        }

        public bool InsertOrderItem(OrderItem orderItem)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@OrderItemNumber", orderItem.OrderItemNumber),
                new SqlParameter("@ProductID", orderItem.ProductID),
                new SqlParameter("@Quantity", orderItem.Quantity),
                new SqlParameter("@OrderNo", orderItem.OrderNo)
            };
            return DataProvider.ExecuteNonQuery("sp_InsertOrderItems", CommandType.StoredProcedure,
                Params);
        }

        public bool DeleteOrderItem(int OrderItemID)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@OrderItemID", OrderItemID)
            };
            return DataProvider.ExecuteNonQuery("sp_DeleteBookType", CommandType.StoredProcedure,
                Params);
        }

        public bool UpdateOrderItem(OrderItem item)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@ProductID", item.ProductID),
                new SqlParameter("@Quantity", item.Quantity )
            };
            return DataProvider.ExecuteNonQuery("sp_UpdateOrderItems", CommandType.StoredProcedure,
                Params);
        }
    }
}
