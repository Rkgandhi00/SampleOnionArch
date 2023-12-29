namespace Common.Dto
{
    public class AzureBlobResponse
    {
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public List<string>? Errors { get; set; }
        public bool hasErrors => Errors != null && Errors.Count > 0;
    }
}
