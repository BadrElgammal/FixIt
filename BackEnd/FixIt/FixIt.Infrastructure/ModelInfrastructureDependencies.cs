using FixIt.Infrastructure.Abstracts;
using FixIt.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FixIt.Infrastructure
{
    public static class ModelInfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
            //config
            services.AddScoped(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
           services.AddScoped<IClientRepository, ClientRepository>();
            //DI => For WorkerRepo
            services.AddTransient<IWorkerRepository, WorkerRepository>();

            return services;
        }
    }
}
