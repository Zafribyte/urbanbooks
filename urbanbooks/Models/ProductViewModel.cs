using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace urbanbooks.Models
{
    public class ProductViewModel
    {

        public IEnumerable<CartItem> allCartItem { get; set; }
        public IEnumerable<Book> allBook { get; set; }
        public IEnumerable<Technology> allTechnology { get; set; }
        public IEnumerable<WishlistItem> allWishlistItems { get; set; }
        public IEnumerable<CartHelper> secureCart { get; set; }
        public List<SelectListItem> I_DeliveryList { get; set; }
        public List<CartConclude> ItsA_wrap { get; set; }

        public Billing Bill { get; set; }
        public DeliveryHelper deliveryHelper { get; set; }

        public class CartHelper
        {
            public int ProductID { get; set; }
            [DataType(DataType.Currency)]
            public double TotalPerItem { get; set; }

        }

        public class CartConclude
        {
            [DataType(DataType.Currency)]
            public double CartTotal { get; set; }
            [DataType(DataType.Currency)]
            public double VatAddedTotal { get; set; }
            [DataType(DataType.Currency)]
            public double SubTotal { get; set; }

        }

    }
    public class Billing
    {
        [StringLength(16, ErrorMessage = "Invalid Credit Card Number")]
        [RegularExpression(@"^.{16,}$", ErrorMessage = "Invalid Credit Card Number")]
        [DataType(DataType.CreditCard)]
        [Key]
        [Required]
        [Display(Name = "Credit Card Number")]
        public string CreditCardNumber
        { get; set; }
        [Required]
        [StringLength(4, ErrorMessage = "Invalid CVC")]
        [RegularExpression(@"^.{3,}$", ErrorMessage = "Invalid CVC")]
        public string CVC
        { get; set; }
        [Display(Name = "Expiry Date")]
        [Required]
        public DateTime ExpiryDate
        { get; set; }

    }
    public class DeliveryHelper
    {
        [Required]
        [Display(Name = "Delivery Address")]
        [RegularExpression(@"^.{16,}$", ErrorMessage = "Please provide full address")]
        [DataType(DataType.MultilineText)]
        public string DeliveryAddress
        { get; set; }
    }
}