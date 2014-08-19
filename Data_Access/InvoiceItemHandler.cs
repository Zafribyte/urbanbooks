﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace urbanbooks
{
    public class InvoiceItemHandler
    {

        public List<InvoiceItem> GetInvoiceItemList()
        {
            List<InvoiceItem> invoiceItems = null;

            using (DataTable table = DataProvider.ExecuteSelectCommand("uspViewInvoiceItems",
                CommandType.StoredProcedure))
            {
                if (table.Rows.Count > 0)
                {
                    invoiceItems = new List<InvoiceItem>();
                    foreach (DataRow row in table.Rows)
                    {
                        InvoiceItem invoiceItem = new InvoiceItem();
                        invoiceItem.InvoiceLineID = Convert.ToInt32(row["InvoiceLineID"]);
                        invoiceItem.ProductID = Convert.ToInt32(row["ProductID"]);
                        invoiceItem.InvoiceID = Convert.ToInt32(row["InvoiceID"]);
                        invoiceItem.CartItemID = Convert.ToInt32(row["CartItemID"]);
                        invoiceItems.Add(invoiceItem);
                    }
                }
            }
            return invoiceItems;
        }

        public bool InsertInvoiceItem(InvoiceItem item)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@Quantity", item.Quantity),
                new SqlParameter("@ProductID", item.ProductID),
                new SqlParameter("@InvoiceLineID", item.InvoiceLineID),
                new SqlParameter("@CartItemID", item.CartItemID),
                new SqlParameter("@InvoiceID", item.InvoiceID)
            };
            return DataProvider.ExecuteNonQuery("sp_InsertInvoiceItems", CommandType.StoredProcedure,
                Params);
        }

        public bool DeleteInvoiceItem(int InvoiceItemID)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@KeywordID", InvoiceItemID)
            };
            return DataProvider.ExecuteNonQuery("sp_DeleteInvoiceItems", CommandType.StoredProcedure,
                Params);
        }
    }
}