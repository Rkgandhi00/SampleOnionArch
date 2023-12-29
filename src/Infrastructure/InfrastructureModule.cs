using Infrastructure.Helper;
using Infrastructure.Helper.Interface;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureModule
    {
        public static void RegisterServices(IServiceCollection services)
        {  
            services.AddTransient<IEmailRepository, EmailRepository>();
            services.AddTransient<IAzureBlobHelper, AzureBlobHelper>();            
        }
    }
}
