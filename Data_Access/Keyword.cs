using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace urbanbooks
{
    public class Keyword
    {
        [Key]
        [ScaffoldColumn(false)]
        public int KeywordID 
        { get; set; }
        [Display(Name="Keyword")]
        public string Keywords 
        { get; set; }
        public string KeywordType
        { get; set; }

        /*  is this the accessor for keyword search */

        [ScaffoldColumn(false)]
        public int ProductID 
        { get; set; }
    }
}
