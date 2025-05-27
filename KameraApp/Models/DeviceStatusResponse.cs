using Newtonsoft.Json;

namespace KameraApp.Models
{
    public class DeviceStatusResponse
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("desc")]
        public string Desc { get; set; }

        [JsonProperty("data")]
        public DeviceStatusData Data { get; set; }
    }

    public class DeviceStatusData
    {
        [JsonProperty("results")]
        public List<DeviceStatusResultItem> Results { get; set; }
    }

    public class DeviceStatusResultItem
    {
        [JsonProperty("deviceCode")]
        public string DeviceCode { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("sleepStatus")]
        public int SleepStatus { get; set; }

        [JsonProperty("channels")]
        public List<ChannelItem> Channels { get; set; }
    }

    public class ChannelItem
    {
        [JsonProperty("channelId")]
        public string ChannelId { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }
    }



}
