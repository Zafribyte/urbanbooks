using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace urbanbooks.Models
{
    public class Book_Advanced
    {
        [ScaffoldColumn(false)]
        [Key]
        public int Id 
        { get; set; }
        [Display(Name = "Book Category")]
        public bool Category
        { get; set; }
        [Display(Name = "Book Title")]
        public bool BookTitle
        { get; set; }
        public bool ISBN
        { get; set; }
        public bool Author 
        { get; set; }
        public bool Publisher
        { get; set; }
        [Display(Name = "Search Term")]
        public string Query
        { get; set; }
        [Display(Name = "From")]
        public double? RangeFrom
        { get; set; }
        [Display(Name = "To")]
        public double? RangeTo
        { get; set; }
    }
}