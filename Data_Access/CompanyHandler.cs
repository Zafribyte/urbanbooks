using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace urbanbooks
{
    public class CompanyHandler
    {
        public List<Company> CompanyDetails()
        {
            List<Company> AuthorList = null;

            using (DataTable table = DataProvider.ExecuteSelectCommand("sp_ViewCompanyDetails",
                CommandType.StoredProcedure))
            {
                if (table.Rows.Count > 0)
                {
                    AuthorList = new List<Company>();
                    foreach (DataRow row in table.Rows)
                    {
                        Company company = new Company();
                        //company.TaxRefferenceNumber = row["TaxRefferenceNumber"].ToString();
                        company.Name = row["Name"].ToString();
                        company.Telephone = row["Telephone"].ToString();
                        company.VATPercentage = Convert.ToDouble(row["VATPercentage"].ToString());
                        company.Email = row["Email"].ToString();
                        company.BookMarkUp = Convert.ToDouble(row["BookMarkUp"].ToString());
                        //company.TechMarkUp = Convert.ToDouble(row["TechMarkUp"].ToString());
                        //company.VATRegistrationNumber = Convert.ToInt32(row["VATRegistrationNumber"]);
                        AuthorList.Add(company);
                    }
                }
            }
            return AuthorList;
        }

        public Company GetCompanyDetail()
        {
            Company company = null;

            using (DataTable table = DataProvider.ExecuteSelectCommand("sp_ViewCompanyDetail",
                CommandType.StoredProcedure))
            {
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    company = new Company();
                    company.Address = row["Address"].ToString();
                    company.Email = row["E-Mail"].ToString();
                    company.Name = row["Name"].ToString();
                    company.Telephone = row["Telephone"].ToString();
                }
            }
            return company;
        }
    }
}
