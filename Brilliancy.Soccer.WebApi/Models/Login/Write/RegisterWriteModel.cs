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
        [StringLength(100, ErrorMessage = "{0} musi mieć conajmniej {2} znaków.", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} musi mieć conajmniej {2} znaków.", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Hasła muszą być identyczne")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} musi mieć conajmniej {2} znaków.", MinimumLength = 5)]
        [RegularExpression("^(?!.*[^a-zA-Z0-9_.@]).*$",
        ErrorMessage = "Login może zawierać jedynie znaki a-z A-Z 0-9 _.@")]
        public string Login { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
