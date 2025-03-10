using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Producto.Application.Configurations;
using Producto.Application.Mappers;
using Producto.Infrastructura.Context;

namespace Producto.Services.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection Configure(this IServiceCollection services, WebApplicationBuilder webApplicationBuilder, IConfiguration configuration)
        {
            ConfigHelper.ConfigSqlServer = configuration.GetSection(nameof(ConfigSqlServer)).Get<ConfigSqlServer>();
            ConfigHelper.ConfiLoggerFile = configuration.GetSection(nameof(ConfiLoggerFile)).Get<ConfiLoggerFile>();
            ConfigHelper.ConfigFormatos = configuration.GetSection(nameof(ConfigFormatos)).Get<ConfigFormatos>();

            services.AddSwaggerGen();
            services.AddEndpointsApiExplorer();
            
            AddConfigureSwagger(services);
            services.AddMvc();
            services.AddInjections();
            services.AddAutoMapper();
            AddUseSqlServer(services, configuration);

            return services;
        }

        public static IServiceCollection AddConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                { Title = "Servicio Producto", Version = "v1" });
            });

            return services;
        }
        public static IServiceCollection AddUseSqlServer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options => options.UseSqlServer(configuration.GetConnectionString(ConfigHelper.ConfigSqlServer!.Connection)));

            return services;
        }

        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(typeof(MapperProfile));
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            return services;
        }
    }
}
