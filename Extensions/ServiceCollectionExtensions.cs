using dashboardManger.Interfaces;
using dashboardManger.Services;

namespace dashboardManger.Extensions
{
    public static  class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IPropertyService, PropertyService>();

            return services;
        }
    }
}
