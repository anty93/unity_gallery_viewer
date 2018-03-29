namespace GalleryTest.Config
{
    public class AppConfig : IAppConfig
    {
        public WebAPI SelectedWebAPI { get; set; }
        public string APIKey { get; set; }
        public string APIAddress { get; set; }
    }
}