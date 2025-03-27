
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
            var response = await httpClient.GetStringAsync("https://testdashboard.suricifatih.com/api/auth/get-device-org-tree");

            var parsed = JsonConvert.DeserializeObject<ApiResponse>(response);
            var flatList = parsed.Data.Departments;

            var tree = BuildTree(flatList, "");

            ViewBag.OrganizationTree = tree;

            return View();
        }

        private List<Organization> BuildTree(List<Organization> items, string parentCode)
        {
            return items
                .Where(x => x.ParentCode == parentCode)
                .Select(x =>
                {
                    x.Children = BuildTree(items, x.Code);
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
