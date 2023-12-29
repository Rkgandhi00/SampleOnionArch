using Application.DataTransformers;
using Application.DataTransformers.Interface;
using Application.Dtos.Outbound;
using Application.Retriever;
using Application.Retriever.Interface;
using AutoMapper;
using Common;
using Common.Config;
using Domain.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public class ApplicationModule : Profile
    {
        public ApplicationModule()
        {
            Mappings();
        }

        public static void Registration(IServiceCollection services)
        {
            RegisterTransformer(services);
            RegisterHttpClient(services);
            RegisterEnricher(services);
            RegisterRetriever(services);

        }

        public static void RegisterRetriever(IServiceCollection services)
        {
            services.AddTransient<ICountryRetriever, CountryRetriever>();
            services.AddTransient<ICityRetriever, CityRetriever>();
        }

        public static void RegisterTransformer(IServiceCollection services)
        {
            services.AddTransient<IGeneralValueDtoTransformer, GeneralValueDtoTransformer>();
        }

        private static void RegisterHttpClient(IServiceCollection services)
        {
            services.AddHttpClient(Constants.MASTER_SERVICE, config =>
            {
                config.BaseAddress = new Uri(AppSettings.CommonSettings.Services.BASE_ADDRESS_OF_MASTER_SERVICE);
            });
        }

        private static void RegisterEnricher(IServiceCollection services)
        {
           
        }

        private void Mappings()
        {
            CreateMap<GeneralValues, GeneralValuesDto>();
            
            CreateMap<User, UserShortDetailDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName))
            ;
        }
    }
}
