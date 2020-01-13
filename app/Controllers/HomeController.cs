using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using app.Models;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace app.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public async Task<IActionResult> Add()
        {
            var instance = GetIPAddress();

            var response = await GetSessionValues();

            ViewBag.Message = "Add a message in the form";
            ViewBag.CurrentValues = response;
            ViewBag.Instance = $"{instance}";

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(FormModel model)
        {
            var instance = GetIPAddress();

            HttpContext.Session.SetString(Guid.NewGuid().ToString(), $"Some nice value added in the instance {instance}: {model.SessionValue}");

            await HttpContext.Session.CommitAsync();

            var response = await GetSessionValues();

            ViewBag.Message = "Add a message in the form";
            ViewBag.CurrentValues = response;
            ViewBag.Instance = $"{instance}";

            System.Threading.Thread.Sleep(10000);

            return View();
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

        private string GetIPAddress()
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName()); // `Dns.Resolve()` method is deprecated.
            IPAddress ipAddress = ipHostInfo.AddressList[0];

            return ipAddress.ToString();
        }

        private async Task<List<string>> GetSessionValues()
        {
            await HttpContext.Session.LoadAsync();

            var response = new List<string>();

            foreach (var item in HttpContext.Session.Keys)
            {
                response.Add(HttpContext.Session.GetString(item));
            }

            return response;
        }
    }
}
