using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace urbanbooks
{
    public class WishlistHandler
    {

        public Wishlist GetWishlist(string UserID)
        {
            Wishlist wish = null;

            SqlParameter[] Params = { new SqlParameter("@UserID", UserID) };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_ReturnWishlistProduct",
                CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    wish = new Wishlist();
                    wish.WishlistID = Convert.ToInt32(row["WishlistID"]);
                    wish.CustomerID = Convert.ToInt32(row["CustomerIS"]);

                }
            }
            return wish;
        }

        public bool CreateWishlist(string UserID, Wishlist wish)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@WishlistID", UserID),
                 new SqlParameter("@CustomerID", wish.CustomerID)
                //new SqlParameter("@Title", book.ProductTitle),
            };
            return DataProvider.ExecuteNonQuery("sp_CreateWishlist", CommandType.StoredProcedure,
                Params);
        }

        public bool UpdateWishlist(Wishlist list)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter ("@WishlistID", list.WishlistID),
                new SqlParameter("@DateModified", list.DateModified ),

            };
            return DataProvider.ExecuteNonQuery("sp_UpdateWishlist", CommandType.StoredProcedure,
                Params);
        }
    }
}
