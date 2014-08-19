using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace urbanbooks
{
    public class Delivery
    {
        [ScaffoldColumn(false)]
        public int DeliveryServiceID 
        { get; set; }
        [Display(Name="Name")]
        public string ServiceName 
        { get; set; }
        [Display(Name="Type")]
        public string ServiceType 
        { get; set; }
        [DataType(DataType.Currency)]
        public double Price 
        { get; set; }
    }
}
