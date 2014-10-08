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
        public string CompanyRegistration { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Please enter a Valid Name")]
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
        [RegularExpression(@"^[0-9].{9,9}", ErrorMessage = "Invalid Telephone number")]
        public string Telephone { get; set; }
        [Required]
        [RegularExpression(@"^[0-9].{9,9}", ErrorMessage = "Invalid Fax number")]
        public string Fax { get; set; }
        [Required]
        public string Email { get; set; }
        public string CompanyLogo { get; set; }
    }
}
