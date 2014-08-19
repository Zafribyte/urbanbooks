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
        [Display(Name="Model")]
        public string ModelName 
        { get; set; }
        public string Specifications 
        { get; set; }
        public string Manufacturer
        { get; set; }
        [Display(Name="Model Number")]
        public string ModelNumber
        { get; set; }
        [ScaffoldColumn(false)]
        public int ManufacturerID
        { get; set; }
        [Display(Name="Front Image")]
        public byte ImageFront 
        { get; set; }
        [Display(Name = "Top Image")]
        public byte ImageTop
        { get; set; }
        [Display(Name = "Side Image")]
        public byte ImageSide
        { get; set; }


        //OVERRIDE
        [ScaffoldColumn(false)]
        public override int ProductID
        { get; set; }

        [Display(Name="Cost Price")]
        public override double CostPrice 
        { get; set; }

        [Display(Name="Price")]
        [DataType(DataType.Currency)]
        public override double SellingPrice
        { get; set; }

        [ScaffoldColumn(false)]
        public override DateTime DateAdded 
        { get; set; }

        [ScaffoldColumn(false)]
        public override int SupplierID
        { get; set; }
   
        


        //TECHTYPE

        public List<TechCategory> TechCategoryList
        { get; set; }

        //EMPLOYEE

        //public List<Employee> Employee
        //{ get; set; }
        //SUPPLIER

        public List<Supplier> SupplierList 
        { get; set; }
    }
}
