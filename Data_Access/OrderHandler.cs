using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace urbanbooks
{
    public class OrderHandler
    {
        public List<Order> GetOrdersList()
        {
            List<Order> OrdersList = null;

            using (DataTable table = DataProvider.ExecuteSelectCommand("sp_ViewAllOrders", //*Note
                CommandType.StoredProcedure))
            {
                if (table.Rows.Count > 0)
                {
                    OrdersList = new List<Order>();
                    foreach (DataRow row in table.Rows)
                    {
                        Order order = new Order();
                        order.OrderNo = Convert.ToInt32(row["OrderNo"]);
                        order.DateCreated = Convert.ToDateTime(row["DateCreated"]);
                        order.DateLastModified = Convert.ToDateTime(row["DateLastModified"]);
                        order.DateSent = Convert.ToDateTime(row["DateSent"]);
                        order.EmployeeID = Convert.ToInt32(row["EmployeeID"]);
                        OrdersList.Add(order);
                    }
                }
            }
            return OrdersList;
        }

        public Order GetOrder(int OrderID)
        {
            Order order = null;

            SqlParameter[] Params = { new SqlParameter("@OrderID", OrderID) };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_ViewSpecificOrder",
                CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    order = new Order();
                    order.OrderNo = Convert.ToInt32(row["OrderNumber"]);
                    order.DateCreated = Convert.ToDateTime(row["DateCreated"]);
                    order.DateSent = Convert.ToDateTime(row["DateSent"]);
                    order.DateLastModified = Convert.ToDateTime(row["DateLastModified"]);
                    order.SupplierID = Convert.ToInt32(row["SupplierID"]);
                    order.EmployeeID = Convert.ToInt32(row["EmployeeID"]);
                    order.Status = Convert.ToBoolean(row["Status"]);

                }
            }
            return order;
        }

        public bool CreateOrder(Order order)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@OrderNumber", order.OrderNo),
                new SqlParameter("@DateCreated", order.DateCreated),
                new SqlParameter("@DateSent", order.DateSent),
                new SqlParameter("@DateLastModified", order.DateLastModified),
                new SqlParameter("@SupplierID", order.SupplierID),
                new SqlParameter("@Employee", order.EmployeeID)
            };
            return DataProvider.ExecuteNonQuery("sp_CreateOrder", CommandType.StoredProcedure,
                Params);
        }

        public bool DeleteOrder(int OrderNumber)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@OrderNumber", OrderNumber)
            };
            return DataProvider.ExecuteNonQuery("sp_DeleteOrder", CommandType.StoredProcedure,
                Params);
        }

        public bool UpdateOrder(Order order)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@DateModified", order.DateLastModified),
                new SqlParameter("@EmployeeID", order.EmployeeID )
            };
            return DataProvider.ExecuteNonQuery("sp_UpdateAuthor", CommandType.StoredProcedure,
                Params);
        }

    }
}
