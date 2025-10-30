
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
}
