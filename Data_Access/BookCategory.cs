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
        public string CategoryDescription
        { get; set; }
        [Display(Name="Book Category")]
        public string CategoryName 
        { get; set; }
    }
}
