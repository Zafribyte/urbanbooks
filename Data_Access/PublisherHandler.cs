using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace urbanbooks
{
    public class PublisherHandler
    {
        public List<Publisher> GetPublisherList()
        {
            List<Publisher> PublisherList = null;

            using (DataTable table = DataProvider.ExecuteSelectCommand("sp_ViewAllPublishers", //*Note
                CommandType.StoredProcedure))
            {
                if (table.Rows.Count > 0)
                {
                    PublisherList = new List<Publisher>();
                    foreach (DataRow row in table.Rows)
                    {
                        Publisher publisher = new Publisher();
                        publisher.PublisherID = Convert.ToInt32(row["PublisherID"]);
                        publisher.Name = row["Publisher"].ToString();
                        PublisherList.Add(publisher);
                    }
                }
            }
            return PublisherList;
        }

        public bool DeletePublisher(int PublisherID)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@PublisherID", PublisherID)
            };
            return DataProvider.ExecuteNonQuery("sp_DeletePublisher", CommandType.StoredProcedure,
                Params);
        }

        public bool InsertPublisher(Publisher publisher)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@PublisherID", publisher.PublisherID),
                new SqlParameter("@Name", publisher.Name),
                new SqlParameter("@ProductID", publisher.ProductID),
            };
            return DataProvider.ExecuteNonQuery("sp_InsertPublisher", CommandType.StoredProcedure,
                Params);
        }
    }
}
