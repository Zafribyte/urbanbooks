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
        [ScaffoldColumn(false)]
        public int CompanyRegistration { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name="Tax Reference Number")]
        public string TaxRefferenceNumber { get; set; }
        [Required]
        [Display(Name="VAT Registration Number")]
        public string VATRegistrationNumber { get; set; }
        [Required]
        [Display(Name="VAT")]
        public double VATPercentage { get; set; }
        [Required]
        [Display(Name="Book Mark Up")]
        public double BookMarkUp { get; set; }
        [Required]
        [Display(Name = "Technology Mark Up")]
        public double TechMarkUp { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }
        [Required]
        public string Telephone { get; set; }
        [Required]
        public string Fax { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
