using Cultivo.Domain.Constants;
using Cultivo.Web.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Cultivo.Web.DTOs
{
    public class UpdateUserViewDTO : BaseViewModel
    {
        [StringLength(50)]
        [Display(Name = ViewModels.DISPLAY_FIELD_NAME)]
        [Required(ErrorMessage = ViewModels.REQUIRED_MESSAGE_NAME)]
        public string Name { get; set; }

        [StringLength(50)]
        [Display(Name = ViewModels.DISPLAY_FIELD_LASTNAME)]
        [Required(ErrorMessage = ViewModels.REQUIRED_MESSAGE_LASTNAME)]
        public string LastName { get; set; }

        [EmailAddress]
        [StringLength(100)]
        [Display(Name = ViewModels.DISPLAY_FIELD_EMAIL)]
        [Required(ErrorMessage = ViewModels.REQUIRED_MESSAGE_EMAIL)]
        public string Email { get; set; }

        [StringLength(256)]
        [DataType(DataType.Password)]
        [Display(Name = ViewModels.DISPLAY_FIELD_PASSWORD)]
        [Required(ErrorMessage = ViewModels.REQUIRED_MESSAGE_PASSWORD)]
        public string Password { get; set; }

        [DataType(DataType.Url)]
        [Display(Name = ViewModels.DISPLAY_FIELD_PHOTO)]
        public string PhotoURL = "";

        public UpdateUserViewDTO()
        {
            // ---
        }

        public UpdateUserViewDTO(string name, string lastName, string email, string password, string photoURL)
        {
            Name = name;
            LastName = lastName;
            Email = email;
            Password = password;
            PhotoURL = photoURL;
        }
    }
}
