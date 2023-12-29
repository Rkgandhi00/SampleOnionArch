using Common.Dto;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Helper.Interface
{
    public interface IAzureBlobHelper
    {
        Task<AzureBlobResponse> UploadFile(IFormFile file, string blobConnectionString, string containerName);
        Task<AzureBlobResponse> DeleteFile(string mediaName, string blobConnectionString, string containerName);
        Task<AzureBlobResponse> DeleteBlob(string mediaName, string blobConnectionString, string blobContainerName);
    }
}
