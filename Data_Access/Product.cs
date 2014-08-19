using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace urbanbooks
{
    public abstract class Product
    {
        public virtual int ProductID
        { get; set; }
        public virtual double CostPrice
        { get; set; }
        public virtual double SellingPrice
        { get; set; }
        public virtual DateTime DateAdded
        { get; set; }
        public virtual bool IsBook
        { get; set; }
        public virtual int SupplierID
        { get; set; }
    }
}
