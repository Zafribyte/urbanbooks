using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace urbanbooks
{
    public class Invoice
    {
        [ScaffoldColumn(false)]
        public int InvoiceID 
        { get; set; }
        [ScaffoldColumn(false)]
        public string User_Id 
        { get; set; }
        [ScaffoldColumn(false)]
        public int DeliveryServiceID 
        { get; set; }
        public string DeliveryAddress 
        { get; set; }
        [ScaffoldColumn(false)]
        public DateTime DateCreated { get; set; }
        [ScaffoldColumn(false)]
        public bool Status 
        { get; set; }
        public int CompanyReg
        { get; set; }
    }
}
