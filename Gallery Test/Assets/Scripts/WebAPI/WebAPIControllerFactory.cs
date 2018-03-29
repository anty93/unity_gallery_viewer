using UnityEngine;
using GalleryTest.Config;

namespace GalleryTest.WebAPI
{
    public class WebAPIControllerFactory
    {
        public static IWebAPIController CreateWebAPIController(IAppConfig appConfig)
        {
            switch (appConfig.SelectedWebAPI)
            {
                case Config.WebAPI.FLICKR:
                default:
                    var flickrApiGameObject = new GameObject("Flickr API Controller");
                    var flickrAPI = flickrApiGameObject.AddComponent<FlickrAPIController>();
                    flickrAPI.Configure(appConfig.APIKey, appConfig.APIAddress);
                    return flickrAPI;
            }
        }
    }
}