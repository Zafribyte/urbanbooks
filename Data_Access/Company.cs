using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace urbanbooks
{
    public class Company
    {
        [Key]
        public int CompanyRegistration { get; set; }
        public string Name { get; set; }
        public string TaxRefferenceNumber { get; set; }
        public int VATRegistrationNumber { get; set; }
        [Display(Name="VAT")]
        public double VATPercentage { get; set; }
        public double BookMarkUp { get; set; }
        public double TechMarkUp { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
    }
}
