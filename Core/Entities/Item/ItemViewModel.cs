
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class ItemViewModel
    {
    }
    public class AddItemViewModel
    {
        public AddItemViewModel()
        {
            Image = new List<ItemImageViewModel>();
        }
        [Required]
        public long UserId { get; set; }
        public List<ItemImageViewModel>? Image { get; set;  }
        [Required(ErrorMessage ="PLEASE ENTER THE IMAGENAME!!!")]
        [MinLength(3)]
        public string ItemName { get; set; }

        [Required(ErrorMessage = "PLEASE PROVIDE THE DESCRIPTION!!!")]
        [MinLength(5)] 
        public string Description { get; set; }
        [Required(ErrorMessage = "PLEASE SELECT THE ITEM TYPES!!!")]
        public long ItemTypeId { get; set; }
        [Required(ErrorMessage = "PLEASE UPLOAD THE ITEM IMAGE!!!")]

        public List<IFormFile> Images {get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public string? Status { get; set; }
    }
    public class ItemImageViewModel
    {
        public string ImageName { get; set; }
        public string ImageSource { get; set; }
    }
    public class ImageDetailViewModel
    {
        public ImageDetailViewModel()
        {
            Images = new List<string>();
        }
        public long ItemId { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string ImageName { get; set; }
        public string Status { get; set; }
        public string CityName{get;set;}
        public string CountyName{get;set;}
        public string StreetName{get;set;}
        public string PostalCode{get;set;}
        public string HouseName { get; set; }

        public List<string> Images { get; set; }
    }
    public class UpdateItemStatus
    {
        [Required(ErrorMessage = "USER ID IS REQUIRED!!!")]
        public long UserId { get; set; }
        [Required(ErrorMessage ="ITEM ID IS REQUIRED!!!")]
        public long ItemId { get; set; }
        [Required(ErrorMessage ="STATUS IS REQUIRED!!!")]
        public string Status { get; set; }
    }
}
