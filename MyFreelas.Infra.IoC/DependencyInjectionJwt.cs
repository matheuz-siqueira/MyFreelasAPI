using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace MyFreelas.Infra.IoC;

public static class DependencyInjectionJwt
{
    public static void AddInfrastructureJwt(this IServiceCollection services, 
        IConfiguration configuration)
    {
        AddJwt(services, configuration);
    }

    private static void AddJwt(this IServiceCollection services, IConfiguration configuration)
    {
        var JWTKey = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(JWTKey),
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });
    }
}
