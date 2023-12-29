using Domain.DAL;
using Domain.DAL.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Domain
{
    public class DomainModule
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IEmailTemplateDAL, EmailTemplateDAL>();
            services.AddTransient<IGeneralValueDAL, GeneralValueDAL>();
        }
    }
}
