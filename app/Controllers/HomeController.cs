using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using app.Models;
using Microsoft.AspNetCore.Http;

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
            var instance = Environment.GetEnvironmentVariable("ASPNETCORE_INSTANCE_NAME");

            HttpContext.Session.SetString("KeyOfSessionValue", $"Some nice value added in the instance {instance}, maybe an object serialized to JSON?");

            await HttpContext.Session.CommitAsync();

            ViewData["Message"] = "If you see this, I guess the data was stored ";

            return View();
        }

        public async Task<IActionResult> Retrieve()
        {

            await HttpContext.Session.LoadAsync();

            string storedValue = HttpContext.Session.GetString("KeyOfSessionValue");
            
            if (String.IsNullOrEmpty(storedValue))
            {
                storedValue = "What a shame, no session value for you.";
            }

            var instance = Environment.GetEnvironmentVariable("ASPNETCORE_INSTANCE_NAME");

            ViewData["Message"] = $"{storedValue} And you are in instance: {instance}";

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
    }
}
