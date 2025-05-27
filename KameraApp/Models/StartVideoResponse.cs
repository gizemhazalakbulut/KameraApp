using Newtonsoft.Json;

namespace KameraApp.Models
{
    public class StartVideoResponse
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("desc")]
        public string Description { get; set; }

        [JsonProperty("data")]
        public StartVideoData Data { get; set; }
    }

    public class StartVideoData
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        // Diğer alanlar da eklenebilir:
        [JsonProperty("session")]
        public string Session { get; set; }

        [JsonProperty("ip")]
        public string Ip { get; set; }

        [JsonProperty("port")]
        public string Port { get; set; }
    }

}
