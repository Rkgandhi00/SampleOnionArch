using System.Net;

namespace Common.Dto.Contracts
{
    public class HttpApiResponse<T>
    {
        public HttpStatusCode statusCode { get; set; }
        public List<string>? message { get; set; }
        public T result { get; set; }
        public IEnumerable<string>? errors { get; set; }
    }
}
