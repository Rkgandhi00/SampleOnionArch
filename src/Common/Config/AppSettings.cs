namespace Common.Config
{
    public static class AppSettings
    {
        public static CommonSettings CommonSettings { get; set; } = new CommonSettings();
    }
    public class CommonSettings
    {
        public Services Services { get; set; }
    }

    public class Services
    {
        public string BASE_ADDRESS_OF_MASTER_SERVICE { get; set; }
        public string BASE_ADDRESS_OF_USER_SERVICE { get; set; }
        public string BASE_ADDRESS_OF_PIMS_SERVICE { get; set; }
    }
}
