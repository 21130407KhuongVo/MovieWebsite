using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieWebsite.Model.DomainModel;
using System.Net.Http;
using System.Text.Json;

namespace MovieWebsite.Controllers
{
    public class MoviesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ListMovies()

        {
            var model = new AdminViewModel();
            List<Movies> movies = new List<Movies>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{model.baseURL}/api/movie"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    movies = 
                }
            }

            model.MovieList = movies;
            // Implement your logic here
            return View();
        }
    }
    public class MoviesViewModel
    {
        public Movies Movies { get; set; }
        public string baseURL = "https://localhost:7271";
    }
}
