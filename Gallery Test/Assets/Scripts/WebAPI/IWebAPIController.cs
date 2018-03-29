using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GalleryTest.WebAPI.Models;
using RSG;

namespace GalleryTest.WebAPI
{
    public interface IWebAPIController
    {
        /// <summary>
        /// Downloads json data for a selected amount of photos for a given gallery ID
        /// </summary>
        /// <param name="galleryId">ID string of the gallery</param>
        /// <param name="maxPhotosToCollect">Maximum total amount of photos to get</param>
        /// <returns>A collection of Photo class objects</returns>
        IPromise<IEnumerable<PhotoMetaData>> CollectPhotosFromAGallery(string galleryId);

        IPromise<IEnumerable<PhotoDownloaded>> DownloadPhotosFromGallery(IEnumerable<PhotoMetaData> photoMetaDatas, int maxPhotos);
    }
}