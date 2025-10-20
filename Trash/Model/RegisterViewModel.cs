using System.ComponentModel.DataAnnotations;

namespace Trash
{
    public class RegisterViewModel
    {
        public string FullName {  get; set; }
        public string Email { get; set; }
        public string Password { get; set; }    
        [Compare("Password")]

        public string ConfirmPassword { get; set; }
    }
}
