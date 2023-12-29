using Application.Retriever.Interface;
using Common;
using Common.Dto.Contracts;
using Newtonsoft.Json;

namespace Application.Retriever
{
    public class CityRetriever : ICityRetriever
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CityRetriever(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<CityDto>?> GetCityByIds(string CityIds)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient(Constants.MASTER_SERVICE);
                var response = await httpClient.GetAsync($"api/master/City/GetByIds?CityIds={CityIds}");

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var content = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<HttpApiResponse<List<CityDto>>>(content);
                return responseData?.result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
