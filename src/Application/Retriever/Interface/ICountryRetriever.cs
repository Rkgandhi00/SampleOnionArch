using Common.Dto.Contracts;

namespace Application.Retriever.Interface
{
    public interface ICountryRetriever
    {
        Task<List<CountryDto>?> GetCountryByIds(string countryIds);
    }
}
