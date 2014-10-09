using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace urbanbooks.Models
{
    public class OrderLineModel
    {
        [Key]
        public int TheKey 
        { get; set; }
        [DataType(DataType.Currency)]
        public double totally { get; set; }
        public Order OrderDetails 
        { get; set; }
        public IEnumerable<OrderItem> OrderLineDetails 
        { get; set; }
        public urbanbooks.Supplier SupplierDetails 
        { get; set; }
    }
}