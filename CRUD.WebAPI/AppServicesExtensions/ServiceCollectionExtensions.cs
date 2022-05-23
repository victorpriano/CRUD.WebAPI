using CRUD.WebAPI.DataAccess;
using CRUD.WebAPI.DataAccess.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace CRUD.WebAPI.AppServicesExtensions
{
    public static class ServiceCollectionExtensions
    {
        public static WebApplicationBuilder AddAuthenticationJwt(this WebApplicationBuilder builder)
        {
            var key = Encoding.ASCII.GetBytes(Settings.Secret);

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });

            builder.Services.AddAuthorization();
            return builder;
        }
        public static WebApplicationBuilder AddPersistence(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddTransient<IUser, UserRepository>();

            return builder;
        }

    }
}
