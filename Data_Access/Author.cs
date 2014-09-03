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

<<<<<<< HEAD
=======
        //BOOK
        public List<Book> Book
        { get; set; }
>>>>>>> d5a104f58965e981a58a48ed7311d319211495bb
    }
}
