using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace urbanbooks
{
    public class InvoiceHandler
    {
        public bool CreateInvoice(Invoice invoice)
        {

            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@CustomerID", invoice.User_Id),
                new SqlParameter("@DeliveryServiceID", invoice.DeliveryServiceID),
                new SqlParameter("@DateCreated", invoice.DateCreated),
                new SqlParameter("@DeliveryAddress",invoice.DeliveryAddress),
            };
            return DataProvider.ExecuteNonQuery("sp_InsertInvoice", CommandType.StoredProcedure,
                Params);
        }





        public Invoice GetInvoiceNumber(Invoice invoice)
        {
            Invoice reciept;
            SqlParameter[] Params = { new SqlParameter("@CostPrice", invoice.DateCreated),
                                      new SqlParameter("@DeliveryAddress", invoice.DeliveryAddress),
                                      new SqlParameter("@Status", invoice.Status),
                                      new SqlParameter("@User_Id", invoice.User_Id),
                                      new SqlParameter("@DeliveryServiceID", invoice.DeliveryServiceID)
                                    };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_ManhattanProject", CommandType.StoredProcedure, Params))
            {
                reciept = new Invoice();
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    reciept.InvoiceID = Convert.ToInt32(row["InvoiceID"]);
                }

            }
            return reciept;
        }







        public Invoice GetInvoice(int InvoiceID)
        {
            Invoice invoice = null;

            SqlParameter[] Params = { new SqlParameter("@InvoiceID", InvoiceID) };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_InsertInvoice",
                CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    invoice = new Invoice();
                    invoice.InvoiceID = Convert.ToInt32(row["InvoiceID"]);
                    invoice.DateCreated = Convert.ToDateTime(row["DateCreated"]);
                    invoice.User_Id = Convert.ToInt32(row["CustomerID"]);
                    invoice.DeliveryServiceID = Convert.ToInt32(row["DeliveryServiceID"]);
                    invoice.DeliveryAddress = row["DeliveryAddress"].ToString();
                }
            }
            return invoice;
        }

        public bool DeleteInvoice(int InvoiceID)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@InvoiceID", InvoiceID)
            };
            return DataProvider.ExecuteNonQuery("sp_DeleteInvoice", CommandType.StoredProcedure,
                Params);
        }//STORED PROCEDURE
    }
}
