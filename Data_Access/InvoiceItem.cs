using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace urbanbooks
{
    public class InvoiceItem
    {
        [Key]
        [ScaffoldColumn(false)]
        public int InvoiceID 
        { get; set; }
        [ScaffoldColumn(false)]
        public int InvoiceLineID 
        { get; set; }
        [ScaffoldColumn(false)]
        public int ProductID 
        { get; set; }
        [ScaffoldColumn(false)]
        public int CartItemID 
        { get; set; }
        public int Quantity 
        { get; set; }
        [DataType(DataType.Currency)]
        public double Price 
        { get; set; }

    }
}
