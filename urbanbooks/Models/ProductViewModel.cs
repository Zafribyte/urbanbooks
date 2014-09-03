using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace urbanbooks.Models
{
    public class ProductViewModel
    {
        [Key]
        public int theKey { get; set; }
        [NotMapped]
        public IEnumerable<CartItem> allCartItem { get; set; }
        [NotMapped]
        public IEnumerable<Book> allBook { get; set; }
        [NotMapped]
        public IEnumerable<Technology> allTechnology { get; set; }
        [NotMapped]
        public IEnumerable<WishlistItem> allWishlistItems { get; set; }
        [NotMapped]
        public IEnumerable<CartHelper> secureCart { get; set; }
        [NotMapped]
        public List<SelectListItem> I_DeliveryList { get; set; }
        [NotMapped]
        public List<CartConclude> ItsA_wrap { get; set; }
        public ProvideUser UserDetails { get; set; }
        public Billing Bill { get; set; }
        public Invoice recieptData { get; set; }
        public Company company { get; set; }
        public DeliveryHelper deliveryHelper { get; set; }

        public class CartHelper
        {
            [Key]
            public int ProductID { get; set; }
            public int CartItemID { get; set; }
            [DataType(DataType.Currency)]
            public double TotalPerItem { get; set; }

        }

        public class CartConclude
        {
            [DataType(DataType.Currency)]
            [Key]
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
        [Key]
        [Display(Name = "Delivery Address")]
        [DataType(DataType.MultilineText)]
        public string DeliveryAddress
        { get; set; }
        public string DeliveryServiceName 
        { get; set; }
        public string DeliveryServiceType 
        { get; set; }
        [DataType(DataType.Currency)]
        [UIHint("Currency")]
        [Required]
        public decimal DeliveryServicePrice 
        { get; set; }
    }
    public class ProvideUser
    {
        [Key]
        public string Name 
        { get; set; }
        public string LName 
        { get; set; }
        public string Address 
        { get; set; }
        public string PhoneNumber 
        { get; set; }
        public string email 
        { get; set; }
    }
}