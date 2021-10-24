using Microsoft.Extensions.DependencyInjection;
using ProductManagement.Data.Repositories;
using ProductManagement.Data.Repositories.Implementation;

namespace ProductManagement.Data
{
    public static class Bindings
    {
        public static void AddData(this IServiceCollection services)
        {
            services.AddSingleton<IProductRepository, ProductRepository>();
        }
    }
}
