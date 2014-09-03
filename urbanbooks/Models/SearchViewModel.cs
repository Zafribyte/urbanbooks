using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace urbanbooks.Models
{
    public class SearchViewModel
    {
        [Key]
        public int ID { get; set; }
        public string Query { get; set; }
        [NotMapped]
        public IEnumerable<Book> BookResults { get; set; }
        [NotMapped]
        public IEnumerable<Technology> GadgetResults { get; set; }
        [NotMapped]
        public IEnumerable<BookCategory> BookCategoryResults { get; set; }
        [NotMapped]
        public IEnumerable<TechCategory> GadgetCategoryResults { get; set; }
    }
}