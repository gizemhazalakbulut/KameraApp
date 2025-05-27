using KameraApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;
using System.Text;

namespace KameraApp.Controllers
{
    public class CameraController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly ILogger<CameraController> _logger;

        public CameraController(IHttpClientFactory httpClientFactory, ILogger<CameraController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<IActionResult> ShowCamera(string deviceCode)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(deviceCode))
                    return BadRequest("Device code is required.");

                var httpClient = _httpClientFactory.CreateClient();

                var statusRequestBody = new { DeviceCode = deviceCode };

                var statusRequest = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7242/api/auth/device-status")
                {
                    Content = new StringContent(JsonConvert.SerializeObject(statusRequestBody), Encoding.UTF8, "application/json")
                };

                var statusResponse = await httpClient.SendAsync(statusRequest);
                var statusJson = await statusResponse.Content.ReadAsStringAsync();

                if (!statusResponse.IsSuccessStatusCode)
                    return StatusCode((int)statusResponse.StatusCode, $"Device status failed: {statusJson}");

                Console.WriteLine("Gelen JSON:");
                Console.WriteLine(statusJson); // 🖨 Tüm gelen json burada

                var statusResult = JsonConvert.DeserializeObject<DeviceStatusResponse>(statusJson);

                // 1. Genel cihaz listesi kontrolü
                if (statusResult?.Data?.Results == null || !statusResult.Data.Results.Any())
                    return StatusCode(404, "Cihaz listesi boş.");

                // 2. İlk cihaz alınır (null olabilir, dikkat!)
                var resultItem = statusResult.Data.Results[0];

                if (resultItem == null)
                {
                    _logger.LogError("resultItem null döndü.");
                    return StatusCode(500, "Cihaz bilgisi null.");
                }

                // 3. Kanal listesi kontrolü
                if (resultItem.Channels == null)
                {
                    _logger.LogError("resultItem.Channels null.");
                    return StatusCode(404, "Kanal listesi null.");
                }

                if (!resultItem.Channels.Any())
                {
                    _logger.LogWarning("Kanal listesi boş.");
                    return StatusCode(404, "Kanal listesi boş.");
                }

                // 4. Artık güvenli şekilde foreach kullanılabilir
                var videoInfos = new List<CameraStreamInfo>();

                // foreach döngüsü başlıyor
                foreach (var channel in resultItem.Channels)
                {
                    _logger.LogInformation($"channelId: {channel.ChannelId}, status: {channel.Status}");

                    //var videoRequestBody = new
                    //{
                    //    clientType = "WINPC_V2",
                    //    clientMac = "00-09-0F-FE-00-01",
                    //    clientPushId = "",
                    //    project = "PSDK",
                    //    method = "MTS.Video.StartVideo",
                    //    data = new
                    //    {
                    //        streamType = "1",
                    //        optional = "",
                    //        trackId = "",
                    //        extend = "",
                    //        channelId = channel.ChannelId,
                    //        keyCode = "",
                    //        planId = "",
                    //        dataType = "1",
                    //        enableRtsps = "0"
                    //    }
                    //};

                    //var videoRequest = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7242/api/auth/start-realtime-video")
                    //{
                    //    Content = new StringContent(JsonConvert.SerializeObject(videoRequestBody), Encoding.UTF8, "application/json")
                    //};

                    var videoRequestBody = new
                    {
                        ChannelId = channel.ChannelId
                    };

                    var videoRequest = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7242/api/auth/start-realtime-video")
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(videoRequestBody), Encoding.UTF8, "application/json")
                    };


                    var videoResponse = await httpClient.SendAsync(videoRequest);
                    var videoJson = await videoResponse.Content.ReadAsStringAsync();

                    if (!videoResponse.IsSuccessStatusCode)
                        continue;

                    var videoResult = JsonConvert.DeserializeObject<StartVideoResponse>(videoJson);

                    videoInfos.Add(new CameraStreamInfo
                    {
                        ChannelId = channel.ChannelId,
                        StreamUrl = videoResult?.Data?.Url,
                        StreamToken = videoResult?.Data?.Token
                    });
                } // 👈 foreach biter

                // ✅ BURAYA EKLE:
                ViewBag.DeviceCode = deviceCode;
                ViewBag.Streams = videoInfos;

                return View(); // ShowCamera.cshtml
            }
            catch (Exception ex)
            {
                // 🔴 Hatanın tam metnini yakala
                return StatusCode(500, $"UNHANDLED EXCEPTION: {ex.Message}\n{ex.StackTrace}");
            }
        }




    }


}
