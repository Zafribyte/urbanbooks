using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace urbanbooks.Models
{
    public class OrderModels
    {
        [Key]
        public int TheKey 
        { get; set; }
        [NotMapped]
        public IEnumerable<Order> Pending
        { get; set; }
        [NotMapped]
        public IEnumerable<Order> Completed 
        { get; set; }
        [NotMapped]
        public IEnumerable<Order> AllOrders 
        { get; set; }
        public Order Order 
        { get; set; }
    }

}