
using Microsoft.AspNetCore.Http;

namespace Core
{
    public class ItemViewModel
    {
    }
    public class AddItemViewModel
    {
        public string ItemName { get; set; }
        public string Description { get; set; }
        public long ItemTypeId { get; set; }
        public List<IFormFile> Images {get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public string? Status { get; set; }
    }
}
