using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace urbanbooks
{
    public class BookAuthor
    {
        [Key]
        public int BookID
        { get; set; }
        public int AuthorID
        { get; set; }
    }
}
