using Cultivo.Domain.Interfaces.Repositories;
using Cultivo.Domain.Interfaces.Services;
using Cultivo.Persistence.Context;
using Cultivo.Persistence.Repositories;
using Cultivo.Service.Services;
using Microsoft.EntityFrameworkCore;

namespace Cultivo.API
{
    public class Startup
    {
        protected readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public virtual void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IAuthService, AuthService>();

            serviceCollection.AddScoped<IUserService, UserService>();
            serviceCollection.AddScoped<IUserRepository, UserRepository>();

            var connection = _configuration["ConnectionString:cultivo-db"];

            serviceCollection.AddDbContext<CultivoContext>(options =>
            {

                options.UseSqlServer(connection);

            });
        }
    }
}
