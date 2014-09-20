using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace urbanbooks.Models
{
    public class InvoiceModel
    {
        [Key]
        public int TheKey { get; set; }

        public Invoice myInvoice { get; set; }
        [NotMapped]
        public List<InvoiceItem> InvoiceLine { get; set; }

        public Order Order { get; set; }
        [NotMapped]
        public List<OrderItem> OrderLine { get; set; }

        public int MyProperty { get; set; }
    }
}