
using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class RegisterUserViewModel
    {
        [Required]
        [MinLength(5)]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        [MinLength(10)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        [MinLength(5)]
        public string FullName { get; set; }
        public string Status { get;set; }
        public string UserType { get; set; }    
    }
}
