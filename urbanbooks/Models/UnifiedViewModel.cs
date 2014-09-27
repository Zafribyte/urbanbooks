using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace urbanbooks.Models
{
    public class UnifiedViewModel
    {
        public bool iSupplier { get; set; }
        public Book Book { get; set; }
        public BookCategory BookCategory { get; set; }
        public Publisher Publisher { get; set; }
        public List<Author> Authors { get; set; }
        public Technology Device { get; set; }
        public TechCategory Category { get; set; }
        public Manufacturer Manufacturer { get; set; }
    }
}