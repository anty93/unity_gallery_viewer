using System.Collections.Generic;
using GalleryTest.WebAPI.Models;
using RSG;

namespace GalleryTest.WebAPI
{
    /// <summary>
    /// Interface base for web apis. Declares collecting photo meta data from a given gallery and downloading images
    /// </summary>
    public interface IWebAPIController
    {
        /// <summary>
        /// Downloads json data for a selected amount of photos for a given gallery ID
        /// </summary>
        /// <param name="galleryId">ID string of the gallery</param>
        /// <param name="maxPhotosToCollect">Maximum total amount of photos to get</param>
        /// <returns>A collection of Photo class objects</returns>
        IPromise<IEnumerable<PhotoMetaData>> CollectPhotosFromAGallery(string galleryId);

        /// <summary>
        /// Downloads photo images based on provided photo meta data
        /// </summary>
        /// <param name="photoMetaDatas">Meta data of photos</param>
        /// <param name="maxPhotos">How many photos to download</param>
        /// <returns></returns>
        IPromise<IEnumerable<PhotoDownloaded>> DownloadPhotosFromGallery(IEnumerable<PhotoMetaData> photoMetaDatas, int maxPhotos);
    }
}