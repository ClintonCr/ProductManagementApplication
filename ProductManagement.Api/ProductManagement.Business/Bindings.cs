using Microsoft.Extensions.DependencyInjection;
using ProductManagement.Business.Services;
using ProductManagement.Business.Services.Implementation;

namespace ProductManagement.Business
{
    public static class Bindings
    {
        public static void AddBusiness(this IServiceCollection services)
        {
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProductOptionService, ProductOptionService>();
        }
    }
}
