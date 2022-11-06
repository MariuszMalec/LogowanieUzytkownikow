using LoginUser.WebApi.Entities;
using LoginUser.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace LoginUser.WebApp.Controllers
{
    public class AuthenticationController : Controller
    {
        private const string AppiUrl = "https://localhost:44352/api";
        IHttpClientFactory _httpClientFactory;

        // GET: AuthenticationController

        public AuthenticationController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                HttpClient client = _httpClientFactory.CreateClient();

                var request = new HttpRequestMessage(HttpMethod.Post, $"{AppiUrl}/Account/Register");

                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                request.Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                var result = await client.SendAsync(request);

                //var content = await result.Content.ReadAsStringAsync();

                //Serilog.Log.Information("User {userName} create new trainer at {date}", userEmail, DateTime.Now);

                if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    //Serilog.Log.Warning($"Trainer can't be created, email exist yet!");
                    return Content("You are not registered!");
                }

                if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    //Serilog.Log.Warning($"Trainer can't be created, email exist yet!");
                    return Content("You are not registered!");
                }

                return RedirectToAction(nameof(YouAreRegister));
            }
            catch
            {
                return View();
            }      

        }

        [HttpGet("YouAreRegister")]
        public ActionResult YouAreRegister(int id)
        {
            return View();
        }

        // GET: AuthenticationController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AuthenticationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuthenticationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthenticationController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AuthenticationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthenticationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AuthenticationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
