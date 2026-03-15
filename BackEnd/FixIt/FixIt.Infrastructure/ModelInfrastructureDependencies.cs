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
            services.AddScoped<IServiceRequestRepository, ServiceRequestRepository>();
            services.AddScoped<IFavoritesRepository, FavoritesRepository>();
            services.AddScoped<IReviewsRepository, ReviewsRepository>();

            //DI => For WorkerRepo
            services.AddTransient<IWorkerRepository, WorkerRepository>();

            return services;
        }
    }
}
