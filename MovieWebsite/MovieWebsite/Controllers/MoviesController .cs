using Microsoft.AspNetCore.Mvc;
using MovieWebsite.Model.DomainModel;
using Newtonsoft.Json;

namespace MovieWebsite.Controllers
{
    public class MoviesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ListMovies(string search = null)
        {
            var model = new MoviesViewModel();
            List<Movies> movies = new List<Movies>();

            using (var httpClient = new HttpClient())
            {
                string url = string.IsNullOrEmpty(search)
                    ? $"{model.baseURL}/api/movie"
                    : $"{model.baseURL}/api/movie/search?name={Uri.EscapeDataString(search)}";

                using (var response = await httpClient.GetAsync(url))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    movies = JsonConvert.DeserializeObject<List<Movies>>(apiResponse);
                }
            }

            model.MovieList = movies;

            return View(model);
        }

    }

    public class MoviesViewModel
    {
        public List<Movies> MovieList { get; set; }
        public Movies Movies { get; set; }
        public string baseURL = "https://localhost:7271";
    }
}
