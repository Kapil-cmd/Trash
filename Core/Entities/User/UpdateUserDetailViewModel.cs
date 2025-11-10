
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class UpdateUserDetailViewModel
    {
        [Required]
        public long UserId { get; set; }

        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [PhoneNumberValidation]
        public string PhoneNumber { get; set; }
        [Required]
        public string FullName { get; set; }
        public string? ImageName { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        public string HouseName { get; set; }
        [Required]
        public string StreetName { get; set; }
        [Required]
        public string PostalCode {  get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public long? CountryId { get; set; }
        [Required]
        public long? CountyId { get; set; }

        public IFormFile? profileImage {  get; set; }
        public long? AddressId {  get; set; }
    }
}
