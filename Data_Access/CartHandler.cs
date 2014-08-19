using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace urbanbooks
{
    public class CartHandler
    {
        public Cart GetCart(int CustomerID)
        {
            Cart cart = null;

            SqlParameter[] Params = { new SqlParameter("@CustomerID", CustomerID) };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_ViewSpecificCart",
                CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    cart = new Cart();
                    cart.CartID = Convert.ToInt32(row["CartID"]);
                    cart.DateLastModified = Convert.ToDateTime(row["DateLastModified"]);

                }
            }
            return cart;
        }

        public bool CreateCart(Cart cart)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@DateLastModified", cart.DateLastModified),
                new SqlParameter("@CustomerID",cart.CustomerID )
            };
            return DataProvider.ExecuteNonQuery("sp_CreateCart", CommandType.StoredProcedure,
                Params);
        }

        public bool UpdateCart(Cart cart)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@CartID", cart.CartID),
                new SqlParameter("@DateLastModified", cart.DateLastModified )
            };
            return DataProvider.ExecuteNonQuery("sp_UpdateCart", CommandType.StoredProcedure,
                Params);
        }
    }
}
