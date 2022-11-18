using Cultivo.Domain.Interfaces.Repositories;
using Cultivo.Domain.Models;
using Cultivo.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Cultivo.Persistence.Repositories
{
    public sealed class UserRepository : BaseRepository<User>, IUserRepository
    {
        protected readonly DbSet<User> _queryUser;
        protected readonly DbSet<Post> _queryPost;
        protected readonly CultivoContext _context;

        public UserRepository(CultivoContext context) : base(context)
        {
            _context = context;
            _queryUser = _context.Set<User>();
            _queryPost = _context.Set<Post>();
        }

        public async Task<User> DeleteUserWithPosts(int id)
        {
            var user = await _queryUser.FindAsync(id);
            var posts = await _queryPost.ToListAsync();

            if (user.posts.Count() != 0)
            {
                foreach (var post in user.posts)
                {
                    _queryPost.Remove(post);
                }
            }

            _queryUser.Remove(user);

            await _context.SaveChangesAsync();

            return user;
        }
    }
}
