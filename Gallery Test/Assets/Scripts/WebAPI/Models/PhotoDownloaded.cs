using UnityEngine;

namespace GalleryTest.WebAPI.Models
{
    /// <summary>
    /// Contains photo meta data and its downloaded texture
    /// </summary>
    public class PhotoDownloaded
    {
        public Texture Texture { get; set; }
        public PhotoMetaData MetaData { get; set; }
    }
}