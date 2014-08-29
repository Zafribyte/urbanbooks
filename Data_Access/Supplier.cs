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
        [ScaffoldColumn(false)]
        public int SupplierID
        { get; set; }
        public string Name
        { get; set; }
        public string User_Id { get; set; }
        public string LastName
        { get; set; }
        [StringLength(10)]
        public string Fax
        { get; set; }
        [Display(Name="Contact Person")]
        public string ContactPerson
        { get; set; }
        [Display(Name="Telephone")]
        [StringLength(10)]
        public string ContactPersonNumber
        { get; set; }
        [ScaffoldColumn(false)]
        public bool Status
        { get; set; }
    }
}
