using Cultivo.Domain.DTOs;
using Cultivo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cultivo.Domain.Factories
{
    public static class UserFactory
    {
        public static User Create(NewUserDTO newUser)
        {
            User user = new User();
            
            user.Name = newUser.Name;
            user.LastName = newUser.LastName;
            user.Email = newUser.Email;
            user.Password = newUser.Password;
            user.PhotoURL = newUser.PhotoURL;

            return user;
        }

        public static User Update(UpdateUserDTO updateUser, User user)
        {
            user.Id = updateUser.Id;
            user.Name = updateUser.Name;
            user.LastName = updateUser.LastName;
            user.Email = updateUser.Email;
            user.Password = updateUser.Password;
            user.PhotoURL = updateUser.PhotoURL.ToString();

            return user;
        }
    }
}
