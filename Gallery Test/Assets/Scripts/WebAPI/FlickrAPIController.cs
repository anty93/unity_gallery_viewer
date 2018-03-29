using UnityEngine;
using System.Collections.Generic;
using GalleryTest.WebAPI.Models;
using RSG;
using System;
using System.Collections;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;

namespace GalleryTest.WebAPI
{
    /// <summary>
    /// Implementation of Flickr API for downloading photos from a gallery and constructing their direct urls.
    /// </summary>
    public class FlickrAPIController : MonoBehaviour, IWebAPIController
    {
        private string apiKey;
        private string apiAddress;

        /// <summary>
        /// Sets the basic info for the API to work.
        /// </summary>
        /// <param name="key">API key</param>
        /// <param name="address">API base address</param>
        public void Configure (string key, string address)
        {
            apiKey = key;
            apiAddress = address;
        }

        /// <summary>
        /// Starts a coroutine that will collect photo data from a given gallery.
        /// </summary>
        /// <param name="galleryId">Id of the chosen gallery on the hosting service</param>
        /// <returns></returns>
        public IPromise<IEnumerable<PhotoMetaData>> CollectPhotosFromAGallery(string galleryId)
        {
            var promise = new Promise<IEnumerable<PhotoMetaData>>();
            StartCoroutine(_CollectPhotosFromAGallery(promise, galleryId));
            return promise;
        }

        /// <summary>
        /// Starts a coroutine that will download all images based on given photo meta data.
        /// </summary>
        /// <param name="photoMetaDatas">Meta Data needed to construct a direct url</param>
        /// <param name="maxPhotos">How many photos to download</param>
        /// <returns></returns>
        public IPromise<IEnumerable<PhotoDownloaded>> DownloadPhotosFromGallery(IEnumerable<PhotoMetaData> photoMetaDatas, int maxPhotos)
        {
            var promise = new Promise<IEnumerable<PhotoDownloaded>>();
            StartCoroutine(_DownloadPhotosFromGallery(promise, photoMetaDatas, maxPhotos));
            return promise;
        }

        private IEnumerator _CollectPhotosFromAGallery(Promise<IEnumerable<PhotoMetaData>> promise, string galleryId)
        {
            var webRequest = UnityWebRequest.Get(string.Format(
                "{0}?method=flickr.galleries.getPhotos&api_key={1}&gallery_id={2}&format=json&nojsoncallback=1",
                apiAddress, apiKey, galleryId));

            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                promise.Reject(new Exception(webRequest.error));
            }
            else if (webRequest.responseCode != 200)
            {
                promise.Reject(new Exception(webRequest.downloadHandler.text));
            }
            else
            {
                var jsonResult = webRequest.downloadHandler.text;
                var jsonParsed = JObject.Parse(jsonResult);
                var photoMetaDataList = ((JArray)jsonParsed["photos"]["photo"]).ToObject<List<PhotoMetaData>>();
                promise.Resolve(photoMetaDataList);
            }
        }

        private IEnumerator _DownloadPhotosFromGallery(Promise<IEnumerable<PhotoDownloaded>> promise, IEnumerable<PhotoMetaData> photoMetaDatas, int maxPhotos)
        {
            var photos = new List<PhotoDownloaded>();
            UnityWebRequest webRequest;

            foreach (var meta in photoMetaDatas)
            {
                webRequest = UnityWebRequestTexture.GetTexture(ConstructPhotoUrl(meta));
                yield return webRequest.SendWebRequest();

                if (webRequest.isNetworkError || webRequest.isHttpError)
                {
                    promise.Reject(new Exception(webRequest.error));
                }
                else
                {
                    Texture photoTexture = DownloadHandlerTexture.GetContent(webRequest);
                    photos.Add(new PhotoDownloaded { Texture = photoTexture, MetaData = meta });

                    if (photos.Count >= maxPhotos)
                    {
                        break;
                    }
                }
            }

            promise.Resolve(photos);
        }

        private string ConstructPhotoUrl(PhotoMetaData metaData)
        { 
            return string.Format("https://farm{0}.staticflickr.com/{1}/{2}_{3}.jpg", metaData.Farm, metaData.ServerId, metaData.Id, metaData.Secret);
        }
    }
}
