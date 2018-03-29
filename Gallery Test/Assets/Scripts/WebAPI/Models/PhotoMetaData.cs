using Newtonsoft.Json;

namespace GalleryTest.WebAPI.Models
{
    /// <summary>
    /// Contains basic photo data necessary to construct its direct url
    /// </summary>
    public class PhotoMetaData
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("server")]
        public int ServerId { get; set; }

        [JsonProperty("farm")]
        public int Farm { get; set; }

        [JsonProperty("secret")]
        public string Secret { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }
}