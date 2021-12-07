using ConstructionCalculator.Api.Helpers;
using ConstructionCalculator.Api.Helpers.DatabaseHelpers;
using ConstructionCalculator.Api.Mapping;
using ConstructionCalculator.Api.Models;
using ConstructionCalculator.Api.Repositories;
using ConstructionCalculator.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

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

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // указывает, будет ли валидироваться издатель при проверке токена
                        ValidateIssuer = true,
                        // строка, представляющая издателя
                        ValidIssuer = AuthOptions.ISSUER,

                        // будет ли валидироваться потребитель токена
                        ValidateAudience = true,
                        // установка потребителя токена
                        ValidAudience = AuthOptions.AUDIENCE,
                        // будет ли валидироваться время существования
                        ValidateLifetime = true,

                        // установка ключа безопасности
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        // валидация ключа безопасности
                        ValidateIssuerSigningKey = true,
                    };
                });


            services.AddCors(options =>
            {
                // Определение политики CORS как "default"
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins("http://localhost")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin()
                        .SetIsOriginAllowed(x => true);
                });
            });

            services.AddSingleton<AutomapperConfigurationStorage>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddTransient<IAutomapperHelper, AutomapperHelper>();
            services.AddTransient<IRepository<User>, EntityRepository<User>>();
            services.AddTransient<IRepository<CalculateEntity>, EntityRepository<CalculateEntity>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication(); //аутентификация
            app.UseAuthorization();     // авторизация
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
