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
                new SqlParameter("@User_Id", invoice.User_Id),
                new SqlParameter("@DeliveryServiceID", invoice.DeliveryServiceID),
                new SqlParameter("@DateCreated", invoice.DateCreated),
                new SqlParameter("@DeliveryAddress",invoice.DeliveryAddress),
            };
            return DataProvider.ExecuteNonQuery("sp_InsertInvoice", CommandType.StoredProcedure,
                Params);
        }


        public List<Invoice> GetInvoicesInRange(string startDate, string endDate)
        {
            List<Invoice> invoice = null;

            SqlParameter[] Params = { new SqlParameter("@StartDate", startDate), new SqlParameter("@EndDate", endDate) };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_ViewInvoiceByDate", CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count > 0)
                {
                    invoice = new List<Invoice>();
                    foreach (DataRow row in table.Rows)
                    {
                        Invoice invoiceItem = new Invoice();
                        invoiceItem.InvoiceID = Convert.ToInt32(row["InvoiceID"]);
                        invoiceItem.DateCreated = Convert.ToDateTime(row["DateIssued"]);
                        invoice.Add(invoiceItem);
                    }
                }
            }
            return invoice;
        }


        public InvoiceItem GetInvoiceNumber(Invoice invoice)
        {
            InvoiceItem reciept;
            SqlParameter[] Params = { new SqlParameter("@Date", invoice.DateCreated),
                                      new SqlParameter("@DeliveryAddress", invoice.DeliveryAddress),
                                      new SqlParameter("@Status", invoice.Status),
                                      new SqlParameter("@User_Id", invoice.User_Id),
                                      new SqlParameter("@DeliveryServiceID", invoice.DeliveryServiceID)
                                    };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_InsertInvoice", CommandType.StoredProcedure, Params))
            {
                reciept = new InvoiceItem();
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
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_ViewSpecificInvoice",
                CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    invoice = new Invoice();
                    invoice.InvoiceID = Convert.ToInt32(row["InvoiceID"]);
                    invoice.DateCreated = Convert.ToDateTime(row["DateIssued"]);
                    invoice.User_Id = row["User_Id"].ToString();
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
