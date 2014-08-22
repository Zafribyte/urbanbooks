using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace urbanbooks.Models
{
    public class AddNewBookViewModel
    {
        public List<SelectListItem> booktype { get; set; }
        public List<SelectListItem> suppliers { get; set; }

        public IEnumerable<BookCategory> bookTypes { get; set; } 

        public Book books { get; set; }
        

    }
}