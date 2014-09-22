using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace urbanbooks
{
    public class Publisher 
    {
        [Key]
        [ScaffoldColumn(false)]
        public int PublisherID { get; set; }
        [Display(Name="Publisher")]
        [Required]
        public string Name { get; set; }

    }
}
