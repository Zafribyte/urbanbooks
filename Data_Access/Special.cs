using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace urbanbooks
{
    public class Special
    {
        [Key]
        [ScaffoldColumn(false)]
        public int SpecialID 
        { get; set; }
        public DateTime StartDate 
        { get; set; }
        public DateTime EndDate 
        { get; set; }
        public string Description 
        { get; set; }
        [Display(Name="Cut Down %")]
        public int CutDownPercentage 
        { get; set; }
        public double SpecialPrice //?
        { get; set; }
    }
}
