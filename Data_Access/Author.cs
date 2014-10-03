using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace urbanbooks
{
    public class Author
    {
        [Key]
        [ScaffoldColumn(false)]
        public int AuthorID
        { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Please enter a Valid Name")]
        public string Name
        { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Please enter a Valid Surname")]
        public string Surname
        { get; set; }

    }
}
