using Cultivo.Domain.DTOs;
using Cultivo.Domain.Interfaces.Repositories;
using Cultivo.Domain.Interfaces.Services;
using Cultivo.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cultivo.Service.Services
{
    public sealed class UserService : BaseService<User>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPostRepository _postRepository;
        public UserService(IUserRepository userRepository, IPostRepository postRepository) : base(userRepository)
        {
            _userRepository = userRepository;
            _postRepository = postRepository;
        }


        public async Task<User> GetUserWithPosts(string email)
        {
            var user = await _userRepository.GetOneByCriteriaAsync(model => model.Email == email);
            var posts = await _postRepository.ListUsers();
            
            /*foreach (var post in posts)
            {
                if (post.UserId == user.Id)
                    user.posts.Add(post);
            }*/

            return user;
        }

        public async Task<User> DeleteUserWithPosts(int id)
        {
            return await _userRepository.DeleteUserWithPosts(id);
        }
    }
}
