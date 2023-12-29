using System.Net;

namespace Common.CustomErrorResponses
{
    public class ApiResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public List<string>? Message { get; set;  }
        public object? Result { get; set; }
        public IEnumerable<string>? Errors { get; set; }
    }
}
