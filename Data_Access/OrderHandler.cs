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
                        OrdersList.Add(order);
                    }
                }
            }
            return OrdersList;
        }

        public Order GetOrder(int orderNo)
        {
            Order order = null;

            SqlParameter[] Params = { new SqlParameter("@OrderNo", orderNo) };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_ViewSpecificOrder",
                CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    order = new Order();
                    order.OrderNo = Convert.ToInt32(row["OrderNo"]);
                    order.DateCreated = Convert.ToDateTime(row["DateCreated"]);
                    order.DateSent = Convert.ToDateTime(row["DateSent"]);
                    order.DateLastModified = Convert.ToDateTime(row["DateLastModified"]);
                    order.SupplierID = Convert.ToInt32(row["SupplierID"]);
                    order.EmployeeID = Convert.ToInt32(row["EmployeeID"]);
                    order.InvoiceID = Convert.ToInt32(row["InvoiceID"]);
                    order.Status = Convert.ToBoolean(row["Status"]);

                }
            }
            return order;
        }

        public OrderItem CreateOrder(Order order)
        {
            OrderItem OrderLine;
            SqlParameter[] Params = { new SqlParameter("@DateCreated", order.DateCreated),
                                      new SqlParameter("@DateLastModified", order.DateLastModified),
                                      new SqlParameter("@InvoiceID", order.InvoiceID),
                                      new SqlParameter("@Status", order.Status),
                                      new SqlParameter("@SupplierID",order.SupplierID)
                                    };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_CreateOrder", CommandType.StoredProcedure, Params))
            {
                OrderLine = new OrderItem();
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    OrderLine.OrderNo = Convert.ToInt32(row["OrderNumber"]);
                }

            }
            return OrderLine;
        }

        public bool DeleteOrder(int orderNo)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@OrderNumber", orderNo)
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
            return DataProvider.ExecuteNonQuery("sp_UpdateOrder", CommandType.StoredProcedure,
                Params);
        }

        public bool AssignSupplierToOrder(Order order)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@OrderNo", order.OrderNo),
                new SqlParameter("@SupplierID", order.SupplierID)
            };
            return DataProvider.ExecuteNonQuery("sp_AssignSupplier", CommandType.StoredProcedure,
                Params);
        }

        public List<Order> GetAllCompletedOrders()
        {
            List<Order> OrdersList = null;

            using (DataTable table = DataProvider.ExecuteSelectCommand("sp_ViewAllCompletedOrders", //*Note
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
                        order.InvoiceID = Convert.ToInt32(row["InvoiceID"]);
                        order.Status = Convert.ToBoolean(row["Status"]);
                        OrdersList.Add(order);
                    }
                }
            }
            return OrdersList;
        }

        public List<Order> GetAllPendingOrders()
        {
            List<Order> OrdersList = null;

            using (DataTable table = DataProvider.ExecuteSelectCommand("sp_ViewAllPendingOrders", //*Note
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
                        order.InvoiceID = Convert.ToInt32(row["InvoiceID"]);
                        order.Status = Convert.ToBoolean(row["Status"]);
                        OrdersList.Add(order);
                    }
                }
            }
            return OrdersList;
        }

    }
}
