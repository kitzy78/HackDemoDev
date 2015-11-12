using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using Hack.Ui.Models;

namespace Hack.Ui.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var vm = new HomeIndexViewModel();
            vm.UiVersion = this.GetType().Assembly.GetName().Version.ToString();
            vm.ApiVersion = await GetApiVersion();

            return View(vm);
        }

        public async Task<string> GetApiVersion()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiAddress"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync("api/version");
                if (response.IsSuccessStatusCode)
                {
                    return (await response.Content.ReadAsStringAsync());
                }
                return "Error";
            }
        }
    }
}