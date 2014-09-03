using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace urbanbooks.Models
{
    public class AddNewBookViewModel
    {
        public List<SelectListItem> bookCategories { get; set; }
        public List<SelectListItem> suppliers { get; set; }

        public List<SelectListItem> authors { get; set; }

        public List<SelectListItem> publishers { get; set; }

        public IEnumerable<Author> Author { get; set; }

        public IEnumerable<BookCategory> bookCategory { get; set; }
        public IEnumerable<Book> book { get; set; }
        public BookCategory bc
        { get; set; }
        public Book books { get; set; }
    }
}