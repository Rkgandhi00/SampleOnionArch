using Application.Retriever.Interface;
using Common;
using Common.Dto.Contracts;
using Newtonsoft.Json;

namespace Application.Retriever
{
    public class CountryRetriever : ICountryRetriever
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CountryRetriever(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<CountryDto>?> GetCountryByIds(string countryIds)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient(Constants.MASTER_SERVICE);
                var response = await httpClient.GetAsync($"api/Master/Country/GetByIds?countryIds={countryIds}");

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var content = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<HttpApiResponse<List<CountryDto>>>(content);
                return responseData?.result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
