using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RedeCredenciadaApi.IoC;
using System;
using System.Diagnostics;
using System.Net;
using TopDown.Core.Cache;
using TopDown.Core.HealthChecks;
using TopDown.Core.HealthChecks.Api;
using TopDown.Core.Log.Serilog;
using TopDown.Core.Service.Discovery.Extension;
using TopDown.Core.WebApi.Extensions;

namespace RedeCredenciadaApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(setupAction =>
            {
                //Habilita autorização para todas as rotas
                //setupAction.Filters.Add(new AuthorizeFilter());

                setupAction.ReturnHttpNotAcceptable = true;

                setupAction.OutputFormatters.Add(new XmlSerializerOutputFormatter());

                setupAction.EnableEndpointRouting = false;
            }).AddJsonOptions(options => options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull);


            services//Adiciona CORS liberando para todas chamadas
                .AddCorsApiCustom()

                //Adiciona versão padrão à Api
                .AddApiVersioningCustom(new ApiVersion(1, 0))

                //Adiciona comportamento de Mvs e adiciona filtro de autorização para todas as rotas da Api
                .AddAddMvcCustom()

                //Adiciona comportamento de validação de modelstate à Api
                .AddApiBehaviorOptionsCustom()

                .AddCacheCustom(Configuration)

                //Adiciona Swagger à Api
                .AddSwaggerGenCustom("Top Down Serviço IN 56", "Exclusão/Substituição de rede - Serviço para listar informações de exclusão/alteração de rede.")

                //Adiciona no pipeline de execução do aplicativo a utilização de HealthCheck
                .AddHealthChecksCustom(Convert.ToInt64(Configuration.GetSection("MemoryCheckOptions:Threshold").Value))

                .AddHealthChecksApiCustom();

            //end Load configuration

            //services.AddAuthentication("Basic")
            //.AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic", null);

            //Autenticar com Basic authentication
            //services.AddAuthenticationCustom(TipoAutenticacao.BasicAuth);


            ///Configuração de Authentication
            //services.Configure<BasicAuthenticationConfig>(options => Configuration.GetSection("AuthenticationConfiguration").Bind(options));

            // Adding MediatR for Domain Events and Notifications
            services.AddMediatR(typeof(Startup));

            //Adicionando Middleware para registrar a injeção de dependência
            services.RegisterIoC(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
                              IWebHostEnvironment env,
                              IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseExceptionHandlerCustom();
                //app.UseDatabaseErrorPage();
            }

            app
                //Configura a utilização do CorrelationId padrão da Top Down
                .UseLogComCorrelationIdCustom()

                //Configura a utilização do Handler de exception padrão da Top Down
                .UseExceptionHandlerCustom()

                //Carregar configurações default para a aplicação
                .UseSwaggerCustom(apiVersionDescriptionProvider.ApiVersionDescriptions, "Unimed Seguros listar informações de exclusão/alteração de rede")

                //.UseHttpsRedirection()

                .UseStaticFiles()

                .UseRouting()

                //.UseAuthentication()

                //.UseAuthorization()

                //Usa o CORS na Api
                .UseCorsCustom()

                //Configura o pipeline para usar o HealthChecks e determina a URL de acesso "/health" e seu tipo de saida
                .UseHealthChecksCustom("/health", HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError)

                .UseEndpoints(endpoints =>
                {
                    endpoints.MapDefaultControllerRoute();
                    endpoints.MapControllers();
                });
        }
    }
}
