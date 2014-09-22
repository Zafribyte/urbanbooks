using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace urbanbooks.Models
{
    public class AddNewBookViewModel
    {
        [Key]
        public int ID { get; set; }
        [NotMapped]
        public List<SelectListItem> bookCategories { get; set; }
        [NotMapped]
        public List<SelectListItem> suppliers { get; set; }
        [NotMapped]
        public List<SelectListItem> authors { get; set; }
        [NotMapped]
        public List<SelectListItem> publishers { get; set; }
        [NotMapped]
        public List<Author> Author { get; set; }
        [NotMapped]
        public List<BookCategory> bookCategory { get; set; }
        [NotMapped]
        public List<Book> book { get; set; }
        [NotMapped]
        public List<Publisher> Publishers { get; set; }
        public BookCategory bc
        { get; set; }
        public Book books { get; set; }
        public Author author { get; set; }
        public Publisher publisher { get; set; }
    }
}