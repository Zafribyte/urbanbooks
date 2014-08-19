using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace urbanbooks
{
    public class WishlistItemHandler
    {

        public List<WishlistItem> GetWishlistItemList(int wishlistId)
        {
            List<WishlistItem> wishlistItems = null;

            SqlParameter[] Params = { new SqlParameter("@WishlistID", wishlistId) };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_ViewWishlist",
                CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count > 0)
                {
                    wishlistItems = new List<WishlistItem>();
                    foreach (DataRow row in table.Rows)
                    {
                        WishlistItem wishlistItem = new WishlistItem();
                        wishlistItem.WishlistItemID = Convert.ToInt32(row["WishlistItemID"]);
                        wishlistItem.ProductID = Convert.ToInt32(row["ProductID"]);
                        wishlistItem.WishlistID = Convert.ToInt32(row["WishlistID"]);
                        wishlistItem.DateAdded = Convert.ToDateTime(row["DateAdded"]);
                        wishlistItems.Add(wishlistItem);
                    }
                }
            }
            return wishlistItems;
        }

        public bool InsertWishlistItem(WishlistItem wishlistItem)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@WishlistID", wishlistItem.WishlistID),
                new SqlParameter("@ProductID", wishlistItem.ProductID),
                new SqlParameter("@DateAdded", wishlistItem.DateAdded)
            };
            return DataProvider.ExecuteNonQuery("sp_InsertWishlistItem", CommandType.StoredProcedure,
                Params);
        }

        public bool DeleteWishlistItem(int WishlistItemID)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@WishlistItemID", WishlistItemID)
            };
            return DataProvider.ExecuteNonQuery("sp_DeleteWishlistItem", CommandType.StoredProcedure,
                Params);
        }

    }
}
