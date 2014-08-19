using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace urbanbooks
{
    public class Customer
    {
        [ScaffoldColumn(false)]
        [Key]
        public int CustomerID
        { get; set; }
        [Display(Name="Name")]
        public string FirstName
        { get; set; }
        [Display(Name = "Surname")]
        public string LastName
        { get; set; }
        [Display(Name="Address")]
        public string PhysicalAddress
        { get; set; }
        [Display(Name = "Cell Phone")]
        [DataType(DataType.PhoneNumber)]
        public string CellPhone
        { get; set; }
        [StringLength(10)]
        [DataType(DataType.PhoneNumber)]
        public string Telephone
        { get; set; }
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "email address is not valid.")]
        [Display(Name="e-mail Address")]
        public string Email
        { get; set; }
        [ScaffoldColumn(false)]
        public string Password
        { get; set; }
        [ScaffoldColumn(false)]
        public bool Status
        { get; set; }
    }
}
