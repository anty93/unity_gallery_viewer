namespace GalleryTest.Config
{
    public enum WebAPI
    {
        FLICKR
    }
    
    public interface IAppConfig
    {
        WebAPI SelectedWebAPI { get; set; }
        string APIKey { get; set; }
        string APIAddress { get; set; }
    }
}