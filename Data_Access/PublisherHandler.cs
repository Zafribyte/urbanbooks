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

        public List<Publisher> PublisherGlobalSearch(string query)
        {
            List<Publisher> publisherList = null;

            SqlParameter[] Params = { new SqlParameter("@Search", query) };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_PublisherGlobalSearch",
                CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count > 0)
                {
                    publisherList = new List<Publisher>();
                    foreach (DataRow row in table.Rows)
                    {
                        Publisher publisher = new Publisher();
                        publisher.PublisherID = Convert.ToInt32(row["PublisherID"]);
                        publisher.Name = row["Name"].ToString();
                        publisherList.Add(publisher);
                    }
                }
            }
            return publisherList;
        }

        public Publisher GetPublisherDeatils(int SpecialID)
        {
            Publisher publisher = null;

            SqlParameter[] Params = { new SqlParameter("@PublisherID", SpecialID) };
            using (DataTable table = DataProvider.ExecuteParamatizedSelectCommand("sp_ViewPublisher",
                CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    publisher = new Publisher();
                    publisher.PublisherID = Convert.ToInt32(row["PublisherID"]);
                    publisher.Name = row["Name"].ToString();

                }
            }
            return publisher;
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
                new SqlParameter("@Name", publisher.Name)
            };
            return DataProvider.ExecuteNonQuery("sp_InsertPublisher", CommandType.StoredProcedure,
                Params);
        }

        public bool UpdatePublisher(Publisher publisher)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@PublisherID", publisher.PublisherID ),
                new SqlParameter("@Name", publisher.Name),
            };
            return DataProvider.ExecuteNonQuery("sp_UpdatePublisher", CommandType.StoredProcedure,
                Params);
        }
    }
}
