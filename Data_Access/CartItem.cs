using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace urbanbooks
{
    public class CartItem
    {
        [ScaffoldColumn(false)]
        public int CartID 
        { get; set; }
        [ScaffoldColumn(false)]
        public int ProductID 
        { get; set; }
        [ScaffoldColumn(false)]
        public int CartItemID 
        { get; set; }
        [Display(Name="Quantity")]
        public int Quantity 
        { get; set; }
        [ScaffoldColumn(false)]
        public DateTime DateAdded 
        { get; set; }
    }
}
