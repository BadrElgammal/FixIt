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
            services.AddScoped<IServiceRequestService, ServiceRequestService>();
            services.AddScoped<IWorkerService, WorkerService>();
            services.AddScoped<IFavoritesService, FavoritesService>();
            services.AddScoped<IReviewsService, ReviewsService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IPortfolioService, PortfoliosService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IChatService, ChatService>();

            return services;
        }
    }
}
