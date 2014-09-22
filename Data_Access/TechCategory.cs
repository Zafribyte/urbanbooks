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
        [Required]
        [Display(Name="Category")]
        public string CategoryName 
        { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name="Category Description")]
        public string CategoryDescription 
        { get; set; }
    }
}
