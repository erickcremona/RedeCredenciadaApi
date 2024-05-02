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
                //Habilita autoriza��o para todas as rotas
                //setupAction.Filters.Add(new AuthorizeFilter());

                setupAction.ReturnHttpNotAcceptable = true;

                setupAction.OutputFormatters.Add(new XmlSerializerOutputFormatter());

                setupAction.EnableEndpointRouting = false;
            }).AddJsonOptions(options => options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull);


            services//Adiciona CORS liberando para todas chamadas
                .AddCorsApiCustom()

                //Adiciona vers�o padr�o � Api
                .AddApiVersioningCustom(new ApiVersion(1, 0))

                //Adiciona comportamento de Mvs e adiciona filtro de autoriza��o para todas as rotas da Api
                .AddAddMvcCustom()

                //Adiciona comportamento de valida��o de modelstate � Api
                .AddApiBehaviorOptionsCustom()

                .AddCacheCustom(Configuration)

                //Adiciona Swagger � Api
                .AddSwaggerGenCustom("Top Down Servi�o IN 56", "Exclus�o/Substitui��o de rede - Servi�o para listar informa��es de exclus�o/altera��o de rede.")

                //Adiciona no pipeline de execu��o do aplicativo a utiliza��o de HealthCheck
                .AddHealthChecksCustom(Convert.ToInt64(Configuration.GetSection("MemoryCheckOptions:Threshold").Value))

                .AddHealthChecksApiCustom();

            //end Load configuration

            //services.AddAuthentication("Basic")
            //.AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic", null);

            //Autenticar com Basic authentication
            //services.AddAuthenticationCustom(TipoAutenticacao.BasicAuth);


            ///Configura��o de Authentication
            //services.Configure<BasicAuthenticationConfig>(options => Configuration.GetSection("AuthenticationConfiguration").Bind(options));

            // Adding MediatR for Domain Events and Notifications
            services.AddMediatR(typeof(Startup));

            //Adicionando Middleware para registrar a inje��o de depend�ncia
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
                //Configura a utiliza��o do CorrelationId padr�o da Top Down
                .UseLogComCorrelationIdCustom()

                //Configura a utiliza��o do Handler de exception padr�o da Top Down
                .UseExceptionHandlerCustom()

                //Carregar configura��es default para a aplica��o
                .UseSwaggerCustom(apiVersionDescriptionProvider.ApiVersionDescriptions, "Unimed Seguros listar informa��es de exclus�o/altera��o de rede")

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
