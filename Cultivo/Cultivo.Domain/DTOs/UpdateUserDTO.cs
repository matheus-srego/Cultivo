using Cultivo.Domain.Models;

namespace Cultivo.Domain.DTOs
{
    public class UpdateUserDTO : BaseEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhotoURL { get; set; }

        public UpdateUserDTO(string name, string lastName, string email, string password, string photoURL)
        {
            Name = name;
            LastName = lastName;
            Email = email;
            Password = password;
            PhotoURL = photoURL;
        }
    }
}
