using AuthResourceEX.Repository.Classes;
using AuthResourceEX.Repository.Interfaces;
using AuthResourceEX.Services.Classes;
using AuthResourceEX.Services.Interfaces;

namespace AuthResourceEX.Bootstrapper
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddScopes(this IServiceCollection service)
        {
            service.AddScoped<IResourceRepository, ResourceRepository>();
            service.AddScoped<IJwtTokenService, JwtTokenService>();
            return service;
        }
    }
}
