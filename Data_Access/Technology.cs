using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace urbanbooks
{
    public class Technology : Product
    {
        [Key]
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
        [DataType(DataType.MultilineText)]
        public string Specs
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
        [DataType(DataType.Currency)]
        public override double CostPrice
        { get; set; }

        [Display(Name = "Selling Price")]
        [DataType(DataType.Currency)]
        public override double SellingPrice
        { get; set; }

        [ScaffoldColumn(false)]
        public override DateTime DateAdded
        { get; set; }

        [ScaffoldColumn(false)]
        public override int SupplierID
        { get; set; }


        public List<SelectListItem> techCategories { get; set; }
        public List<SelectListItem> suppliers { get; set; }
        public List<SelectListItem> manufacturers { get; set; }
        public IEnumerable<TechCategory> techCategory { get; set; }
        public IEnumerable<Manufacturer> manufacturer { get; set; }
        public IEnumerable<Technology> tech { get; set; }
        public Technology techs { get; set; }
        public Manufacturer mans { get; set; }

    }
}
