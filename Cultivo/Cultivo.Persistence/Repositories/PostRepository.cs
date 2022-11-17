using Cultivo.Domain.Interfaces.Repositories;
using Cultivo.Domain.Models;
using Cultivo.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cultivo.Persistence.Repositories
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(CultivoContext context) : base(context)
        {
            // ---
        }
    }
}
