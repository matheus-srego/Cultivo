using Cultivo.Domain.Interfaces.Repositories;
using Cultivo.Domain.Models;
using Cultivo.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cultivo.Persistence.Repositories
{
    public sealed class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(CultivoContext context) : base(context)
        {
            // ---
        }
    }
}
