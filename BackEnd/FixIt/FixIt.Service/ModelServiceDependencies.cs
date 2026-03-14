using FixIt.Service.Abstracts;
using FixIt.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FixIt.Service
{
    public static class ModelServiceDependencies
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            services.AddScoped(typeof(IService<>), typeof(GenericService<>));
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IWorkerService, WorkerService>();

            return services;
        }
    }
}
