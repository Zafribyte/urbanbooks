using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace urbanbooks
{
    public class Manufacturer 
    {
        [Key]
        public int ManufacturerID { get; set; }
        [Display(Name="Manufacturer")]
        [Required(ErrorMessage = "Required")]
        public string Name { get; set; }
        //public List<Technology> TechnologyList { get; set; }
    }
}
