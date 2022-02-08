using Brilliancy.Soccer.WebApi.Translations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Brilliancy.Soccer.WebApi.Models.Login.Write
{
    public class RegisterWriteModel
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password", ResourceType = typeof(WebApiTranslations))]
        [StringLength(100, ErrorMessageResourceName = "StringLength", MinimumLength = 5, ErrorMessageResourceType = typeof(WebApiTranslations))]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Password", ResourceType = typeof(WebApiTranslations))]
        [StringLength(100, ErrorMessageResourceName = "StringLength", MinimumLength = 5, ErrorMessageResourceType = typeof(WebApiTranslations))]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessageResourceName = "StringLength", ErrorMessage = "Hasła muszą być identyczne")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Login", ResourceType = typeof(WebApiTranslations))]
        [StringLength(100, ErrorMessageResourceName = "StringLength", MinimumLength = 5, ErrorMessageResourceType = typeof(WebApiTranslations))]
        [RegularExpression("^(?!.*[^a-zA-Z0-9_.@]).*$", ErrorMessageResourceName = "LoginCharacters", ErrorMessageResourceType = typeof(WebApiTranslations))]
        public string Login { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
