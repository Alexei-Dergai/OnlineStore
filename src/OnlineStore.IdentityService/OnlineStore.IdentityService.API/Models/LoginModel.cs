using System.ComponentModel.DataAnnotations;

namespace OnlineStore.IdentityService.API.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
