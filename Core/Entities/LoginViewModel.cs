using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

    }
    public class LoginResponseViewModel
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string Status { get; set; }
        public string PhoneNumber { get; set; }
        public string IsVerified { get; set; }
        public string ImageUrl { get; set; }
        public string Token { get; set; }
    }
}
