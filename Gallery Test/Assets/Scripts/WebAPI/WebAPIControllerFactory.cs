using UnityEngine;
using GalleryTest.Config;

namespace GalleryTest.WebAPI
{
    /// <summary>
    /// Creates API controller
    /// </summary>
    public class WebAPIControllerFactory
    {
        /// <summary>
        /// Initializes chosen API controller
        /// </summary>
        /// <param name="appConfig">Basic app info</param>
        /// <returns>API controller</returns>
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