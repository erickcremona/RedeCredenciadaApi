using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RedeCredenciadaDomain.Interfaces.Repositoty;
using RedeCredenciadaDomain.Interfaces.Service;
using RedeCredenciadaDomain.Notifications;
using RedeCredenciadaDomain.Services;
using RedeCredenciadaInfraData.Repository;
using TopDown.Core.Data;
using TopDown.Core.Data.Encrypt;
using TopDown.Core.Data.Oracle;
using TopDown.Core.Domain.Extensions;
using TopDown.Core.Facade.Rest.Request.Web.Cliente;
using TopDown.Core.Infra.MemoryBus;
using TopDown.Core.Infra.MemoryBus.Notifications;

namespace RedeCredenciadaApi.IoC
{
    public static class Register
    {
        public static void RegisterIoC(this IServiceCollection services,
                                           IConfiguration configuration)
        {
            var ip = Rede.GetIpLocal;
            //Carregando configuração com banco
            services.Configure<ProviderConnector>(options =>
            {
                configuration.GetSection("Oracle").Bind(options);
                options.Ip = ip;
            });

            //Injeção de Dependencia
            services.AddAutoMapper(typeof(Startup));

            //Mediator
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            services.AddScoped<INotification, Notifier>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<BaseData>((provider) =>
            {
                return new OracleData(
                    provider.GetService<IOptions<ProviderConnector>>(),
                    provider.GetService<ILogger<BaseData>>(),
                    provider.GetService<IHttpContextAccessor>(),
                    provider.GetService<IEncryptConnectionString>(),
                    true, true, false, true);
            });

            services.AddScoped<IServiceProduto, ServiceDomainProduto>();
            services.AddScoped<IRepositoryProduto, RepositoryProduto>();

            services.AddScoped<IServiceRecursos, ServiceDomainRecursos>();
            services.AddScoped<IRepositoryRecursos, RepositoryRecursos>();

            services.AddScoped<IServicePlano, ServiceDomainPlano>();
            services.AddScoped<IRepositoryPlano, RepositoryPlano>();

            // ASP.NET HttpContext dependency
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IRestClientFactory, RestClientFactory>();
        }

    }
}
