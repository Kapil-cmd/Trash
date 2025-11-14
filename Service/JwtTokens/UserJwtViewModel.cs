
namespace Application
{
    public class UserJwtViewModel
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
