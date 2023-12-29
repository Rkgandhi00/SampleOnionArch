using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Common.Dto;
using Common.Helpers;
using Infrastructure.Helper.Interface;
using Microsoft.AspNetCore.Http;
//using Microsoft.WindowsAzure.Storage;
//using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;

namespace Infrastructure.Helper
{
    public class AzureBlobHelper : IAzureBlobHelper
    {

        public async Task<AzureBlobResponse> DeleteFile(string mediaName, string blobConnectionString, string blobContainerName)
        {
            try
            {
                var validation = new AzureBlobResponse();

                var container = GetClient(mediaName, blobConnectionString, blobContainerName);

                var result = container.DeleteIfExistsAsync();
                result.Wait();
                if (result.IsCompletedSuccessfully)
                {
                    return validation;
                }

                validation.Errors = new List<string> { "Internal Error occured" };
                return validation;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<AzureBlobResponse> DeleteBlob(string mediaName, string blobConnectionString, string blobContainerName)
        {
            try
            {
                var validation = new AzureBlobResponse();
                bool isDeleted = false;

                var blobServiceClient = new BlobServiceClient(blobConnectionString);
                var blobContainerClient = blobServiceClient.GetBlobContainerClient(blobContainerName);
                var blobItems = blobContainerClient.GetBlobsAsync(prefix: mediaName);
                await foreach (BlobItem blobItem in blobItems)
                {
                    BlobClient blobClient = blobContainerClient.GetBlobClient(blobItem.Name);
                    isDeleted = await blobClient.DeleteIfExistsAsync();
                    if (!isDeleted)
                    {
                        validation.Errors = new List<string> { "Internal Error occured" };
                        return validation;
                    }
                }

                return validation;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private CloudBlockBlob GetClient(string systemFileName, string connectionString, string containerName)
        {
            var cloudStorageAccount = CloudStorageAccount.Parse(connectionString);

            var blobClient = cloudStorageAccount.CreateCloudBlobClient();

            var container = blobClient.GetContainerReference(containerName);

            return container.GetBlockBlobReference(systemFileName);
        }

        public async Task<AzureBlobResponse> UploadFile(IFormFile file, string blobConnectionString, string blobContainerName)
        {

            try
            {
                var validation = new AzureBlobResponse();
                var success = false;
                var extension = Path.GetExtension(file.FileName);
                var systemFileName = string.Concat(Guid.NewGuid().ToString().Replace("-", string.Empty), extension);

                var blockBlob = GetClient(systemFileName, blobConnectionString, blobContainerName);

                await using (var data = file.OpenReadStream())
                {
                    var result = blockBlob.UploadFromStreamAsync(data);
                    result.Wait();
                    success = result.IsCompletedSuccessfully;
                }

                if (success)
                {
                    validation.FileName = systemFileName;
                    validation.FileUrl = CommonHelper.GetURL(extension, blobContainerName, systemFileName);
                    return validation;
                }

                validation.Errors = new List<string> { "Internal Error occured" };
                return validation;
            }
            catch (Exception)
            {
                throw;

            }
        }
    }
}
