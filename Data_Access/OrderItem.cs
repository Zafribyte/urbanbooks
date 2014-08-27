using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace urbanbooks
{
    public class OrderItem : Order
    {
        [ScaffoldColumn(true)]
        [Key]public int OrderItemNumber 
        { get; set; }
        [ScaffoldColumn(true)]
        public int ProductID 
        { get; set; }
        public int Quantity 
        { get; set; }
        [ScaffoldColumn(false)]
        public int OrderNo
        { get; set; }

        //Order Details

        public int orderNo{get; set;}
        public DateTime dateCreated { get; set; }


    }


}
