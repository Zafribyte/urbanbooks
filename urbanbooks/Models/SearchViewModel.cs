using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace urbanbooks.Models
{
    public class SearchViewModel
    {
        [Key]
        public int ID { get; set; }
       public IEnumerable<Book> BookResults { get; set; }
       public IEnumerable<Technology> GadgetResults { get; set; }
       public IEnumerable<BookCategory> BookCategoryResults { get; set; }
       public IEnumerable<TechCategory> GadgetCategoryResults { get; set; }
    }
}