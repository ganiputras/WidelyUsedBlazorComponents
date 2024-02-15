using Blazored.Toast;
using Microsoft.Extensions.DependencyInjection;

namespace WebApp.Shared
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSharedServices(this IServiceCollection services)
        {

            services.AddBlazoredToast();
            return services;
        }


    }

}

