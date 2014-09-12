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
        [Display(Name="Name")]
        public string ModelName
        { get; set; }
        [ScaffoldColumn(true)]
        [DataType(DataType.MultilineText)]
        [Display(Name="Specifications")]
        public string Specs
        { get; set; }
        [Display(Name="Model Number")]
        public string ModelNumber
        { get; set; }
        public int ManufacturerID
        { get; set; }
        [Display(Name="Front")]
        public string ImageFront
        { get; set; }
        [Display(Name="Top")]
        public string ImageTop
        { get; set; }
        [Display(Name="Side")]
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
