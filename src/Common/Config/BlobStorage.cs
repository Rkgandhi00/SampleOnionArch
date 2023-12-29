namespace Common.Config
{
    public class BlobStorage
    {
        public TestMedia TestMedia { get; set; }
    }

    public class TestMedia : BaseBlobStorage
    {
    }

    public abstract class BaseBlobStorage
    {
        public string BlobConnectionString { get; set; }
        public string BlobContainerName { get; set; }
    }
}
