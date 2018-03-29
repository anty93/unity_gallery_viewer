using UnityEngine;

namespace GalleryTest.WebAPI.Models
{
    public class PhotoDownloaded
    {
        public Texture Texture { get; set; }
        public PhotoMetaData MetaData { get; set; }
    }
}