using System.ComponentModel.DataAnnotations;
using System.ServiceModel.Security;
using DAS.Domain.GoDaddy.Users;

namespace MVC.Models.Pages
{
    public class SettingsModel : BaseModel
    {
        [Required(ErrorMessage = "Your username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "A password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool Verified { get; set; }

        public bool EmailAlerts { get; set; }

        public bool UseAccountInSearch { get; set; }
    }
}
