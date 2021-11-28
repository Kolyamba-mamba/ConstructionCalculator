using ConstructionCalculator.Api.Helpers;
using ConstructionCalculator.Api.Helpers.DatabaseHelpers;
using ConstructionCalculator.Api.Mapping;
using ConstructionCalculator.Api.Models;
using ConstructionCalculator.Api.Repositories;
using ConstructionCalculator.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConstructionCalculator.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));
            services.AddControllers();
            services.AddSingleton<AutomapperConfigurationStorage>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddTransient<IAutomapperHelper, AutomapperHelper>();
            services.AddTransient<IRepository<User>, EntityRepository<User>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
