using Common.Dto.Contracts;
using Newtonsoft.Json;

namespace Application.Enricher
{
    public abstract class AbstractDtoEnricher
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AbstractDtoEnricher(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        protected async Task<List<T>?> GetAll<T>(string client, string apiName)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient(client);
                var response = await httpClient.GetAsync($"api/{apiName}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var responseData = JsonConvert.DeserializeObject<HttpApiResponse<List<T>>>(content);
                    return responseData.result;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        protected async Task<T> GetById<T>(string client, string apiName, int id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient(client);
                var response = await httpClient.GetAsync($"api/{apiName}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var responseData = JsonConvert.DeserializeObject<HttpApiResponse<T>>(content);
                    return responseData.result;
                }

                return default(T);
            }
            catch (Exception ex)
            {
                throw ex;
            }     
        }

        protected async Task<T> GetById_QueryString<T>(string client, string apiName, string id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient(client);
                var response = await httpClient.GetAsync($"api/{apiName}?{id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var responseData = JsonConvert.DeserializeObject<HttpApiResponse<T>>(content);
                    return responseData.result;
                }

                return default(T);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected async Task<List<T>> GetAllById<T>(string client, string apiName, string param)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient(client);

                var response = await httpClient.GetAsync($"api/{apiName}?{param}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var responseData = JsonConvert.DeserializeObject<HttpApiResponse<List<T>>>(content);
                    return responseData.result;
                }

                return default(List<T>);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected async Task<List<T>> GetAllPartialValue<T>(string client, string apiName, string paramString)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient(client);

                var response = await httpClient.GetAsync($"api/{apiName}?{paramString}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var responseData = JsonConvert.DeserializeObject<HttpApiResponse<List<T>>>(content);
                    return responseData.result;
                }

                return default(List<T>);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
