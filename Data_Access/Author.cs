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
        public string Name
        { get; set; }
        public string Surname
        { get; set; }

    }
}
