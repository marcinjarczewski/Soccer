using Brilliancy.Soccer.WebApi.Translations;
using System.ComponentModel.DataAnnotations;

namespace Brilliancy.Soccer.WebApi.Models.Login.Write
{
    public class NewPasswordModel
    {
        [Required]
        [Display(Name = "LoginController_Password", ResourceType = typeof(WebApiTranslations))]
        [StringLength(100, ErrorMessageResourceName = "BaseController_StringLength", MinimumLength = 5, ErrorMessageResourceType = typeof(WebApiTranslations))]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public int AuthId { get; set; }
    }
}
