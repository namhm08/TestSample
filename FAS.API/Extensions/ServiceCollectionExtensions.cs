
using FAS.BLL.BusinessInterfaces;
using FAS.BLL.BusinessService;
using FAS.DAL.Interfaces;
using FAS.DAL.Repositories;
using FAS.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FAS.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Service
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // and a lot more Services
            services.AddSingleton(configuration);
            //services.AddScoped<ICategoryRepository, CategoryRepository>(); // Sửa từ AddSingleton thành AddScoped
            services.AddDbContext<EcommerceDbContext>(options =>
                options.UseSqlServer());


            //BLL
            services.AddScoped<IProductService, ProductService>();


            //DAL
            services.AddScoped<IProductDataAccess, ProductDataAccess>();

            //services.AddScoped<ICategoryService, CategoryService>();

            return services;
        }
    }
}