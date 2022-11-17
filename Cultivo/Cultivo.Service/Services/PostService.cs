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
    public sealed class PostService : BaseService<Post>, IPostService
    {
        public PostService(IPostRepository postRepository) : base(postRepository)
        {
            // ---
        }
    }
}
