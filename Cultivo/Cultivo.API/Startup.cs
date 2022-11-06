using Cultivo.Domain.Interfaces.Repositories;
using Cultivo.Domain.Interfaces.Services;
using Cultivo.Persistence.Context;
using Cultivo.Persistence.Repositories;
using Cultivo.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

            var secretKey = "ZWRpw6fDo28gZW0gY29tcHV0YWRvcmE";

            serviceCollection.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = true;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }
    }
}
