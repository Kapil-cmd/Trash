
using System.ComponentModel.DataAnnotations;
using System.Web.Razor.Text;

namespace Core
{
    public class NotificationViewModel
    {
    }
    public class AddNotificationViewModel
    {
        [Required]
        public  long UserId { get; set; }
        public string Message { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public string CreatedDateTime { get; set; }
        public string ViewedDateTime { get;set; }
    }
}
