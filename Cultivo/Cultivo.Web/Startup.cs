using Cultivo.Domain.Constants;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Cultivo.Web
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
            serviceCollection.AddControllersWithViews();
            serviceCollection.AddMvc(otpions => otpions.EnableEndpointRouting = true);

            serviceCollection.AddSession(options =>
            {
                
                options.IdleTimeout = TimeSpan.FromMinutes(30);

            });

            serviceCollection.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Endpoints.API_URL,
                        ValidAudience = Endpoints.API_URL,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Endpoints.SECRET_KEY))
                    };
                });
        }
    }
}
