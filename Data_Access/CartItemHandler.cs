using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace urbanbooks
{
    public class CartItemHandler
    {
        public List<CartItem> GetCartItemList(int CartID)
        {
            List<CartItem> cartItems = null;
            SqlParameter[] Params = new SqlParameter[] { new SqlParameter("@CartID", CartID) };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_ViewAllCartItems",
                CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count > 0)
                {
                    cartItems = new List<CartItem>();
                    foreach (DataRow row in table.Rows)
                    {
                        CartItem cartItem = new CartItem();
                        cartItem.CartItemID = Convert.ToInt32(row["CartItemID"]);
                        cartItem.ProductID = Convert.ToInt32(row["ProductID"]);
                        cartItem.DateAdded = Convert.ToDateTime(row["DateAdded"]);
                        cartItem.Quantity = Convert.ToInt32(row["Quantity"]);
                        cartItem.CartID = Convert.ToInt32(row["CartID"]);
                        cartItems.Add(cartItem);
                    }
                }
            }
            return cartItems;
        }

        public bool InsertCartItem(CartItem cartItem)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@CartID", cartItem.CartID),
                new SqlParameter("@ProductID", cartItem.ProductID),
                new SqlParameter("@Quantity", cartItem.Quantity),
                new SqlParameter("@DateAdded", cartItem.DateAdded)
            };
            return DataProvider.ExecuteNonQuery("sp_InsertCartItem", CommandType.StoredProcedure,
                Params);
        }

        public bool UpdateCartItem(CartItem cartItem)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@CartItemID", cartItem.CartItemID),
                new SqlParameter("@CartID", cartItem.CartID),
                new SqlParameter("@Quantity", cartItem.Quantity )
            };
            return DataProvider.ExecuteNonQuery("sp_UpdateCartItem", CommandType.StoredProcedure,
                Params);
        }

        public bool DeleteCartItem(int CartItemID)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@CartItemID", CartItemID)
            };
            return DataProvider.ExecuteNonQuery("sp_DeleteCartItem", CommandType.StoredProcedure,
                Params);
        }
    }
}
