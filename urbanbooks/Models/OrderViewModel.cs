using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace urbanbooks.Models
{
    public class OrderViewModel
    {
        public IEnumerable<Order> orders { get; set; }
        public IEnumerable<OrderItem> orderItems { get; set; }
        public IEnumerable<Supplier> supplier { get; set; }
        public IEnumerable<Book> books { get; set; }
        public IEnumerable<Technology> tech { get; set; }
    }
}