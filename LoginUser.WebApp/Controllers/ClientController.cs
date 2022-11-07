using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using LoginUser.WebApp.Models;

namespace LoginUser.WebApp.Controllers
{
    public class ClientController : Controller
    {
        // GET: ClientController
        private const string AppiUrl = "https://localhost:44352/api";
        IHttpClientFactory _httpClientFactory;

        public ClientController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAll()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> GetAll(AuthenticationModel model)
        {
            try
            {
                //TODO to musialem usunac aby poszli gdy model.email = null, jakos inaczej trza!
                //if (!ModelState.IsValid)
                //{
                //    return View(model);
                //}

                var client = _httpClientFactory.CreateClient();

                var request = new HttpRequestMessage(HttpMethod.Get, $"{AppiUrl}/Client");

                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //request.Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                //var result = await client.SendAsync(request);

                //var content = await result.Content.ReadAsStringAsync();

                //request.Headers.Add("Bearer", model.Token);//TODO to nie dziala

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", model.Token);

                //request.Headers.Add("Email", model.Email);

                var result = await client.SendAsync(request);

                if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    //Serilog.Log.Warning($"Trainer can't be created, email exist yet!");
                    //return Content("You are unauthorized!");
                    return RedirectToAction("Unauthorized");
                }

                if (result.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    //Serilog.Log.Warning($"Trainer can't be created, email exist yet!");
                    return Content("Access Forbidden!");
                }


                var content = await result.Content.ReadAsStringAsync();

                var viewModel = JsonConvert.DeserializeObject<List<ViewClient>>(content);

                if (viewModel != null)
                {
                    //trzeba serilize aby poszlo pomiedzy modelami albo poslac od razu content
                    TempData["ClientFromApi"] = JsonConvert.SerializeObject(viewModel);
                    return RedirectToAction(nameof(ViewClientFromApi));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ViewClientFromApi()
        {
            var viewModel = JsonConvert.DeserializeObject<List<ViewClient>>((string)TempData["ClientFromApi"]);

            ViewBag.Users = TempData["ClientFromApi"];

            return View(viewModel);
        }

        [HttpGet("Unauthorized")]
        public ActionResult Unauthorized()
        {
            return View();
        }


        // GET: ClientController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ClientController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClientController/Create
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

        // GET: ClientController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ClientController/Edit/5
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

        // GET: ClientController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ClientController/Delete/5
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
