using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AuthResourceEX.Enums;

namespace AuthResourceEX.Bootstrapper
{
    public static class JWTAuthenticationServices
    {
        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration config)
        {
            // Add JWT Authentication services
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var secretKey = config["Jwt:SecretKey"];
                var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = config["Jwt:Issuer"],    // Ensure this is set in your configuration
                    ValidAudience = config["Jwt:Audience"], // Ensure this is set in your configuration
                    IssuerSigningKey = signingKey
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole(Role.ADMIN.ToString()));
                options.AddPolicy("SuperAdminOnly", policy => policy.RequireRole(Role.SUPERADMIN.ToString()));
            });
        }
    }
}
