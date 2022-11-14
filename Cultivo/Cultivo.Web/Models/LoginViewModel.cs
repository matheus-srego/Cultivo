using Cultivo.Domain.Constants;
using System.ComponentModel.DataAnnotations;

namespace Cultivo.Web.Models
{
    public class LoginViewModel
    {
        [EmailAddress]
        [Display(Name = ViewModels.DISPLAY_FIELD_EMAIL)]
        [Required(ErrorMessage = ViewModels.REQUIRED_MESSAGE_EMAIL)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = ViewModels.DISPLAY_FIELD_PASSWORD)]
        [Required(ErrorMessage = ViewModels.REQUIRED_MESSAGE_PASSWORD)]
        public string Password { get; set; }

        public LoginViewModel()
        {
            // ---
        }

        public LoginViewModel(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
