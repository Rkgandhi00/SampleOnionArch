using System.Net;

namespace Common.CustomErrorResponses
{
    public class ValidationModel
    {
        public object? Result { get; set; }
        public List<string> Errors { get; set; }
        public List<string>? Messages { get; set; }
        public HttpStatusCode? statusCode { get; set; }
        public bool hasErrors => Errors != null && Errors.Count > 0;
    }
}
