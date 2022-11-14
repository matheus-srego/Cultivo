using Cultivo.Domain.Constants;
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
            var connection = _configuration["ConnectionString:cultivo-db"];

            serviceCollection.AddScoped<ITokenService, TokenService>();

            serviceCollection.AddScoped<IUserService, UserService>();
            serviceCollection.AddScoped<IUserRepository, UserRepository>();

            serviceCollection.AddMvc(otpions => otpions.EnableEndpointRouting = true);

            serviceCollection.AddDbContext<CultivoContext>(options =>
            {

                options.UseSqlServer(connection);

            });

            serviceCollection.AddAuthentication(optinos =>
            {
                optinos.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                optinos.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Endpoints.SECRET_KEY))
                    };
                });

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls(Endpoints.MVC_URL);
                });
    }
}
