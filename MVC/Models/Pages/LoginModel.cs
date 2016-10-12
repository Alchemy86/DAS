using System.ComponentModel.DataAnnotations;

namespace MVC.Models.Pages
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Your username is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "A password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
