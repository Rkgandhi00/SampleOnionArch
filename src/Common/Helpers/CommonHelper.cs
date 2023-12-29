using Common.Config;
using Microsoft.AspNetCore.Http;

namespace Common.Helpers
{
    public static class CommonHelper
    {
        private static readonly string[] _extensions = new string[] { Constants.ALLOWED_EXTENTION_JPEG, Constants.ALLOWED_EXTENTION_JPG, Constants.ALLOWED_EXTENTION_PNG, Constants.ALLOWED_EXTENTION_GIF };

        public static string GetURL(string extention, string blobContainerName, string mediaFileName)
        {
            try
            {
                return _extensions.Contains(extention.ToLower())
                    ? $"YOUR_CDN_BASE_URL'/{blobContainerName}/{mediaFileName}"
                   : $"'YOUR_BLOB_BASE_URL'/api/YOUR_CDN_BASE_URL/{blobContainerName}/{mediaFileName}";
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static string GetImageNameFromURL(string mediaFileName)
        {
            try
            {
                return mediaFileName.Substring(mediaFileName.LastIndexOf('/') + 1);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static string GetUserIP(HttpRequest req)
        {
            var ip = req.Headers["X-Forwarded-For"].FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(ip)) ip = ip.Split(',')[0];

            if (string.IsNullOrWhiteSpace(ip)) ip = Convert.ToString(req.HttpContext.Connection.RemoteIpAddress);

            if (string.IsNullOrWhiteSpace(ip)) ip = req.Headers["REMOTE_ADDR"].FirstOrDefault();

            if (ip.Contains(':'))
            {
                ip = ip.Substring(0, ip.IndexOf(':'));
            }
            return ip;
        }
    }
}
