using Newtonsoft.Json;

namespace KameraApp.Models
{
    public class DeviceResponse
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("desc")]
        public string Desc { get; set; }

        [JsonProperty("data")]
        public DevicePageData Data { get; set; }
    }

    public class DevicePageData
    {
        [JsonProperty("pageData")]
        public List<Device> PageData { get; set; }
    }

    public class DeviceInTree
    {
        [JsonProperty("deviceCode")]
        public string DeviceCode { get; set; }

        [JsonProperty("deviceName")]
        public string DeviceName { get; set; }

        // Varsa diğer alanlar da buraya eklenebilir
    }
}
