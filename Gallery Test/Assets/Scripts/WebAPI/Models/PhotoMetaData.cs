using Newtonsoft.Json;

namespace GalleryTest.WebAPI.Models
{
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