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
        [ScaffoldColumn(false)]
        [Key]
        public int TechCategoryID 
        { get; set; }

        public string CategoryName 
        { get; set; }
        public string CategoryDescription 
        { get; set; }
    }
}
