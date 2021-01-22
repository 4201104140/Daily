using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneBoxDeployment.Api
{
    /// <summary>
    /// An API startup class with modifications
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// This is the root to Swagger documentation URL and to the
        /// generated content.
        /// </summary>
        private string SwaggerRoot { get; } = "api-docs";

        /// <summary>
        /// The root for Swagger documentation URL.
        /// </summary>
        private string SwaggerDocumentationBasePath { get; } = "OneBoxDeployment";

        /// <summary>
        /// The environment specific configuration object.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// The environment specific logger.
        /// </summary>
        public Microsoft.Extensions.Logging.ILogger Logger { get; }

        /// <summary>
        /// The hosting environment.
        /// </summary>
        public IWebHostEnvironment Environment { get; set; }


        /// <summary>
        /// A default constructor.
        /// </summary>
        /// <param name="logger">The environment specific logger.</param>
        /// <param name="configuration">The environment specific configuration object.</param>
        /// <param name="env">The environment information to use in checking per deployment type configuration value validity.</param>
        public Startup(ILogger<Startup> logger, IConfiguration configuration, IWebHostEnvironment env)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            Environment = env ?? throw new ArgumentNullException(nameof(env));

        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">The ASP.NET services collection.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            if (Environment.IsProduction())
            {
                
            }
            else
            {
                services.AddCors(options =>
                {
                    options.AddPolicy("CorsPolicy",
                        builder => builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());
                });
            }

            services

                .AddMvcCore(options =>
                {
                    if (!Environment.IsDevelopment())
                    {

                    }


                })
                .AddApiExplorer()

                .AddDataAnnotations()
                .AddFormatterMappings();



            services.AddSingleton(Environment);
            services.AddSingleton(Configuration);


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsProduction())
            {
                //This test middleware needs to be before developer exception pages.
                
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
