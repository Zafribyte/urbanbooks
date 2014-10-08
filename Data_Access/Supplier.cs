using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace urbanbooks
{
    public class Supplier
    {
        [Key]
        [ScaffoldColumn(false)]
        public int SupplierID
        { get; set; }
        [Required]
        public string Name
        { get; set; }
        [ScaffoldColumn(false)]
        public string User_Id 
        { get; set; }
        [Required]
        [Display(Name = "Contact Person Name")]
        public string ContactPerson
        { get; set; }
        [Required]
        [Display(Name="Contact Person Last Name")]
        public string LastName
        { get; set; }
        [Required]
        [StringLength(10)]
        public string Fax
        { get; set; }
        [Display(Name="Telephone")]
        [RegularExpression(@"^[0-9].{9,9}", ErrorMessage = "Invalid Contact number")]
        [StringLength(10)]
        [Required]
        public string ContactPersonNumber
        { get; set; }
        [ScaffoldColumn(false)]
        public bool Status
        { get; set; }
        public bool IsBookSupplier { get; set; }
    }
}
