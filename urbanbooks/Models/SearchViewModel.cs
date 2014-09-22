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
        [NotMapped]
        public IEnumerable<Author> AuthorResults { get; set; }
        [NotMapped]
        public IEnumerable<Publisher> PublisherResults { get; set; }
        [NotMapped]
        public IEnumerable<Manufacturer> ManufacturerResults { get; set; }
        [NotMapped]
        public BookCategory BCategory { get; set; }
        [NotMapped]
        public TechCategory TCategory { get; set; }
        [NotMapped]
        public Publisher Publisher { get; set; }
        [NotMapped]
        public Manufacturer Manufacturer { get; set; }
        [NotMapped]
        public Author Author { get; set; }
        [NotMapped]
        public Technology Gadget { get; set; }
        [NotMapped]
        public Book Book { get; set; }
    }
}