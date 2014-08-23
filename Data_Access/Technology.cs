using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace urbanbooks
{
    public class Technology : Product
    {
        [ScaffoldColumn(false)]
        public int TechID
        { get; set; }
        [ScaffoldColumn(false)]
        public int TechCategoryID
        { get; set; }
        [ScaffoldColumn(true)]
        public string ModelName
        { get; set; }
        [ScaffoldColumn(true)]
        public string Specs
        { get; set; }
        public string Manufacturer
        { get; set; }
        public string ModelNumber
        { get; set; }
        public int ManufacturerID
        { get; set; }
        public string ImageFront
        { get; set; }
        public string ImageTop
        { get; set; }
        public string ImageSide
        { get; set; }



        //OVERRIDE
        [ScaffoldColumn(false)]
        public override int ProductID
        { get; set; }

        [Display(Name = "Cost Price")]
        public override double CostPrice
        { get; set; }

        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public override double SellingPrice
        { get; set; }

        [ScaffoldColumn(false)]
        public override DateTime DateAdded
        { get; set; }

        [ScaffoldColumn(false)]
        public override int SupplierID
        { get; set; }


    }
}
