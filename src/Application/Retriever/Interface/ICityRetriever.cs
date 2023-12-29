using Common.Dto.Contracts;

namespace Application.Retriever.Interface
{
    public interface ICityRetriever
    {
        Task<List<CityDto>?> GetCityByIds(string CityIds);
    }
}
