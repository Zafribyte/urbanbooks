using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace urbanbooks
{
    public class Book : Product
    {
        [ScaffoldColumn(false)]
        public int BookID
        { get; set; }
        public string ISBN
        { get; set; }
        public string BookTitle 
        { get; set; }
        public string Synopsis 
        { get; set; }
        [ScaffoldColumn(false)]
        public int PublisherID 
        { get; set; }
        [ScaffoldColumn(false)]
        public int BookCategoryID
        { get; set; }
        [ScaffoldColumn(false)]
        public int AuthorID
        { get; set; }
        public byte CoverImage
        { get; set; }

        //OVERRIDE

        [ScaffoldColumn(false)]
        public override int ProductID
        { get; set; }
        [ScaffoldColumn(false)]
        public override double CostPrice
        { get; set; }       
        [DataType(DataType.Currency)]
        [Display(Name="Selling Price")]
        public override double SellingPrice
        { get; set; }
        [ScaffoldColumn(false)]
        public override DateTime DateAdded
        { get; set; }        
        [ScaffoldColumn(false)]
        public override bool IsBook
        { get; set; }
        [ScaffoldColumn(true)]
        public override int SupplierID 
        { get; set; }

        //BOOKTYPE

        public List<BookCategory> BookCategory
        { get; set; }

        //EMPLOYEE

        //public List<Employee> Employee
        //{ get; set; }

        //SUPPLIER

        public List<Supplier> Supplier
        { get; set; }

        //AUTHOR

        public List<Author> Author
        { get; set; }

    }
}
