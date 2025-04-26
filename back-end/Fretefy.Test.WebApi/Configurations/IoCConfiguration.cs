using System;
using Fretefy.Test.Domain.Interfaces;
using Fretefy.Test.Domain.Interfaces.Repositories;
using Fretefy.Test.Domain.Services;
using Fretefy.Test.Infra.EntityFramework.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Fretefy.Test.WebApi.Configurations
{
    
    public static class IoCConfiguration
    {
        public static void ConfigureDependencyInjection(this IServiceCollection services){

            //Services
            services.AddScoped<ICidadeService, CidadeService>();
            services.AddScoped<IRegiaoService, RegiaoService>();


            //Repositories
            services.AddScoped<ICidadeRepository, CidadeRepository>();
            services.AddScoped<IRegiaoRepository, RegiaoRepository>();
            services.AddScoped<IRegiaoCidadeRepository, RegiaoCidadeRepository>();
        }

    }
}
