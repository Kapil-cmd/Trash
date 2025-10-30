
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [NotMapped]
        public string? Status { get;set; }
        [NotMapped]
        public string? UserType { get; set; }    
    }
}
