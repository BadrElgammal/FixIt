using FixIt.Infrastructure.Abstracts;
using FixIt.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FixIt.Infrastructure
{
    public static class ModelInfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
           services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IServiceRequestRepository, ServiceRequestRepository>();

            return services;
        }
    }
}
