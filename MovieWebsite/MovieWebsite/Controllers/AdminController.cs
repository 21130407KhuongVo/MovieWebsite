using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieWebsite.Model.DomainModel;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text.Json;

namespace MovieWebsite.Controllers
{
    public class AdminController : Controller
    {

        // GET: /Admin/Index
        public async Task<IActionResult> Index()
        {
            // get Movies
            // https://localhost:<port>/api/movie/
            var model = new AdminViewModel();

            // show properties
            model.AgeSelectListItems = new List<SelectListItem>{
                new SelectListItem{Text="p", Value="P"},
                new SelectListItem{Text="c", Value="C"},
                new SelectListItem{Text="k", Value="K"},
                new SelectListItem{Text="t13", Value="T13"},
                new SelectListItem{Text="t16", Value="T16"},
                new SelectListItem{Text="t18", Value="T18"}
            };

            // visible
            model.VisibleListItem = new List<SelectListItem>{
                new SelectListItem{Text="Ẩn", Value="hide"},
                new SelectListItem{Text="Hiển thị", Value="unhide"}
            };
            List<Movies> movies = new List<Movies>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{model.baseURL}/api/movie"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    movies = JsonConvert.DeserializeObject<List<Movies>>(apiResponse);
                }
            }

            model.MovieList = movies;

            return View(model);
        }
    }

    public class AdminViewModel
    {
        public List<Movies> MovieList { get; set; }
        public List<SelectListItem> AgeSelectListItems { get; set; }
        public List<SelectListItem> VisibleListItem { get; set; }
        public string SelectedAge { get; set; }
        public string baseURL = "https://localhost:7271";
    }
}
