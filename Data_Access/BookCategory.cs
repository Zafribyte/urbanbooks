using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace urbanbooks
{
    public class BookCategory
    {
        [Key]
        [ScaffoldColumn(false)]
        public int BookCategoryID
        { get; set; }
        [Display(Name="Book Category Description")]
        [DataType(DataType.MultilineText)]
        [Required]
        public string CategoryDescription
        { get; set; }
        [Required]
        [Display(Name="Book Category")]
        public string CategoryName 
        { get; set; }
    }
}
