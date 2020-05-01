using System;

using Autofac;
using Autofac.Configuration;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Dnd.Ddd.CharacterCreation.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        ///     This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options => options.Filters.Add(new ProducesAttribute("application/json")))
                .AddJsonOptions(jsonOptions => jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = null);
        }

        /// <summary>
        ///     This method gets called after ConfigureServices.
        ///     It should be used to configure modules containing Autofac mappings.
        /// </summary>
        /// <param name="builder"></param>
        public virtual void ConfigureContainer(ContainerBuilder builder)
        {
            ConfigurationBuilder domainModulesConfigurationBuilder = new ConfigurationBuilder();
            domainModulesConfigurationBuilder.AddJsonFile("autofacDomainModules.json");

            builder.RegisterModule(new ConfigurationModule(domainModulesConfigurationBuilder.Build()));

            ConfigurationBuilder infrastructureModulesConfigurationBuilder = new ConfigurationBuilder();
            infrastructureModulesConfigurationBuilder.AddJsonFile("autofacInfrastructureModules.json");
            builder.RegisterModule(new ConfigurationModule(infrastructureModulesConfigurationBuilder.Build()));
        }

        /// <summary>
        ///     This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            _ = env ?? throw new ArgumentNullException(nameof(env));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            if (!env.IsDevelopment())
            {
                return;
            }

            app.UseSwagger();
            app.UseSwaggerUI(
                c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "D&D CharacterCreation API V1");
                    c.RoutePrefix = string.Empty;
                });
        }
    }
}
