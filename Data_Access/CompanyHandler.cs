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
                        company.TechMarkUp = Convert.ToDouble(row["TechnologyMarkUp"].ToString());
                        //company.VATRegistrationNumber = Convert.ToInt32(row["VATRegistrationNumber"]);
                        company.CompanyLogo = row["CompanyLogo"].ToString();
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
                    company.CompanyRegistration = row["CompanyRegistration"].ToString();
                    company.Address = row["PhysicalAddress"].ToString();
                    company.VATRegistrationNumber = row["VatRegistrationNumber"].ToString();
                    company.TaxRefferenceNumber = row["TaxReferenceNumber"].ToString();
                    company.Email = row["Email"].ToString();
                    company.Name = row["Name"].ToString();
                    company.TechMarkUp = Convert.ToDouble(row["TechnologyMarkup"].ToString());
                    company.BookMarkUp = Convert.ToDouble(row["BookMarkup"].ToString());
                    company.VATPercentage = Convert.ToDouble(row["VatPercentage"].ToString());
                    company.Telephone = row["Telephone"].ToString();
                    company.Fax = row["Fax"].ToString();
                    company.CompanyLogo = row["CompanyLogo"].ToString();
                }
            }
            return company;
        }

        public bool UpdateCompany(Company company)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@Name", company.Name),
                new SqlParameter("@CompanyRegistration", company.CompanyRegistration),
                new SqlParameter("@TaxReferenceNumber",company.TaxRefferenceNumber),
                new SqlParameter("@PhysicalAddress",company.Address),
                new SqlParameter("@Telephone",company.Telephone),
                new SqlParameter("@Fax", company.Fax),
                new SqlParameter("@Email",company.Email),
                new SqlParameter("@VatRegistrationNumber",company.VATRegistrationNumber),
                new SqlParameter("@VATPercentage",company.VATPercentage),
                new SqlParameter("@BookMarkUp", company.BookMarkUp),
                new SqlParameter("@TechnologyMarkUp", company.TechMarkUp),
                new SqlParameter("@CompanyLogo", company.CompanyLogo)

            };
            return DataProvider.ExecuteNonQuery("sp_UpdateCompany", CommandType.StoredProcedure,
                Params);
        }
    }
}
