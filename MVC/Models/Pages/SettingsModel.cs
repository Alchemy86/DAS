using DAS.Domain.GoDaddy.Users;

namespace MVC.Models.Pages
{
    public class SettingsModel : BaseModel
    {
        public GoDaddyAccount GodaddyAccount { get; set; } 
        public bool EmailAlerts { get; set; }
        public bool UseAccountInSearch { get; set; }
    }
}
