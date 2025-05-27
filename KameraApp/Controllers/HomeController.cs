
using KameraApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace CameraAutomation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public async Task<IActionResult> Index()
        {
            var httpClient = new HttpClient();

            // 1. Organizasyonları al
            var orgResponse = await httpClient.GetStringAsync("https://localhost:7242/api/auth/get-device-org-tree");
            var orgTree = JsonConvert.DeserializeObject<OrganizationTreeResponse>(orgResponse);
            var flatOrgs = orgTree.Data.Departments;

            // 2. Cihazları al
            var deviceResponse = await httpClient.GetStringAsync("https://localhost:7242/api/auth/devices");
            var devicePage = JsonConvert.DeserializeObject<DeviceResponse>(deviceResponse);
            var allDevices = devicePage.Data.PageData;

            // 3. Organizasyonlara cihaz isimlerini eşleştir
            var tree = BuildTreeWithMatchedDevices(flatOrgs, "", allDevices);

            ViewBag.OrganizationTree = tree;

            return View();
        }

        private List<Organization> BuildTreeWithMatchedDevices(List<Organization> items, string parentCode, List<Device> allDevices)
        {
            return items
                .Where(x => x.ParentCode == parentCode)
                .Select(x =>
                {
                    x.Children = BuildTreeWithMatchedDevices(items, x.Code, allDevices);
                    x.MatchedDevices = new List<Device>();

                    if (x.Device != null)
                    {
                        foreach (var dev in x.Device)
                        {
                            var matched = allDevices.FirstOrDefault(d => d.DeviceCode == dev.Id); // ✅ Eşleştirme burada
                            if (matched != null)
                                x.MatchedDevices.Add(matched);
                        }
                    }

                    return x;
                })
                .ToList();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
