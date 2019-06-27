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

        public IActionResult Add()
        {
            var instance = Environment.GetEnvironmentVariable("ASPNETCORE_INSTANCE_NAME");

            ViewBag.Message = "Add a message in the form";
            ViewBag.CurrentValues = new List<string>();
            ViewBag.Instance = $"{instance}";

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(FormModel model)
        {
            var instance = Environment.GetEnvironmentVariable("ASPNETCORE_INSTANCE_NAME");

            HttpContext.Session.SetString(Guid.NewGuid().ToString(), $"Some nice value added in the instance {instance}: {model.SessionValue}");

            await HttpContext.Session.CommitAsync();

            await HttpContext.Session.LoadAsync();

            var response = new List<string>();

            foreach (var item in HttpContext.Session.Keys)
            {
                response.Add(HttpContext.Session.GetString(item));
            }

            ViewBag.Message = "Add a message in the form";
            ViewBag.CurrentValues = response;
            ViewBag.Instance = $"{instance}";

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
