using Fretefy.Test.Domain.Interfaces;
using Fretefy.Test.Domain.Interfaces.Repositories;
using Fretefy.Test.Domain.Services;
using Fretefy.Test.Infra.EntityFramework;
using Fretefy.Test.Infra.EntityFramework.Repositories;
using Fretefy.Test.WebApi.Configurations;
using Fretefy.Test.WebApi.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Fretefy.Test.WebApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<DbContext, TestDbContext>();
            services.AddDbContext<TestDbContext>((provider, options) =>
            {
                options.UseSqlite("Data Source=Data\\Test.db");
            });
             services.AddCors(options =>
                {
                    options.AddPolicy("AllowAll", builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
                });

            services.ConfigureDependencyInjection();
            services.AddMvc()
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Latest);
            services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
                });
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseCors("AllowAll");
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
