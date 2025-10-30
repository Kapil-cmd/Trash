using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class ItemTypesViewModel
    {
    }
    public class AddItemTypeViewModel
    {
        [Required]
        [MinLength(4)]
        public string ItemTypeName { get; set; }
        [Required]
        [MinLength(7)]
        public string Description { get; set; }

        public string? CreatedBy { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public string? Status { get; set; }

    }
}
