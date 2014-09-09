using System.ComponentModel.DataAnnotations;

namespace urbanbooks.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string Action { get; set; }
        public string ReturnUrl { get; set; }
    }

    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }





    public class FullRegisterViewModel
    {
        [Key]
        [ScaffoldColumn(false)]
        public int CustomerID { get; set; }
        [StringLength(150, MinimumLength = 1)]
        [Display(Name = "First Name")]
        public string FirstName
        { get; set; }
        [Display(Name = "Last Name")]
        [StringLength(150, MinimumLength = 2)]
        public string LastName
        { get; set; }
        [Display(Name = "Address")]
        [DataType(DataType.MultilineText)]
        [StringLength(150, MinimumLength=10)]
        public string Address
        { get; set; }
        [Required]
        [Display(Name = "Cell Phone")]
        [DataType(DataType.PhoneNumber)]
        public string CellPhone
        { get; set; }
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }




    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class RegisterSupplier
    {
        [Key]
        [ScaffoldColumn(false)]
        public int SupplierID
        { get; set; }
        [Required]
        public string Name
        { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [ScaffoldColumn(false)]
        public string User_Id
        { get; set; }
        [Display(Name = "Address")]
        [DataType(DataType.MultilineText)]
        [StringLength(150, MinimumLength = 10)]
        public string Address
        { get; set; }
        [Required]
        [Display(Name = "Contact Person Name")]
        public string ContactPerson
        { get; set; }
        [Required]
        [Display(Name = "Contact Person Last Name")]
        public string LastName
        { get; set; }
        [Required]
        [StringLength(10)]
        public string Fax
        { get; set; }
        [Display(Name = "Telephone")]
        [StringLength(10)]
        [Required]
        public string ContactPersonNumber
        { get; set; }
        [ScaffoldColumn(false)]

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
