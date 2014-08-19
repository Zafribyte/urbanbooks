using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace urbanbooks
{
    public class Employee
    {
        [ScaffoldColumn(false)]
        [Key]
        public int EmployeeID
        { get; set; }
        [Display(Name="Name")]
        public string FirstName
        { get; set; }
        [Display(Name="Surname")]
        public string LastName
        { get; set; }
        public string Address
        { get; set; }
        [Display(Name="Cell Phone")]
        [StringLength(10)]
        [DataType(DataType.PhoneNumber)]
        public string CellPhone
        { get; set; }
        [StringLength(10)]
        [DataType(DataType.PhoneNumber)]
        public string Telephone
        { get; set; }
        [DataType(DataType.EmailAddress)]
        [Display(Name= "e-mail Address ")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "email address is not valid.")]
        public string Email
        { get; set; }
        [ScaffoldColumn(false)]
        public string Password
        { get; set; }
        [ScaffoldColumn(false)]
        public bool Status
        { get; set; }
        [ScaffoldColumn(false)]
        public int RoleID
        { get; set; }
    }
}
