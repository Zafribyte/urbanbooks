using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace urbanbooks
{
    public class TechCategory
    {
        [Key]
        [ScaffoldColumn(false)]
        public int TechCategoryID 
        { get; set; }
        [Display(Name="Category")]
        public string CategoryName 
        { get; set; }
        public string CategoryDescription 
        { get; set; }
    }
}
