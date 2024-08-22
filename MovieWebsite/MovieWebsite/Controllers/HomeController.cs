using Microsoft.AspNetCore.Mvc;

using MovieWebsite.Data;
using MovieWebsite.Model.DomainModel;

using MovieWebsite.Models;
using System.Diagnostics;
using System.Xml.Linq;


namespace MovieApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILogger<MovieDbContext> _context;
        public Movies Movie { get; private set; }

        public HomeController(ILogger<HomeController> logger, MovieDbContext context)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> DetailPage(Guid moviesId)
        {
            _logger.LogInformation("Received moviesId: {moviesId}", moviesId);

            var movie = await GetMovieFromApiAsync(moviesId);
            //var comments = await GetCommentsFromApiAsync(moviesId);

            //_logger.LogInformation("Fetched comments: {Comments}", comments); // Kiểm tra giá trị comments

            var model = new HomeViewModel
            {
                Id = moviesId,
                baseURL = "https://localhost:7271",
                Movie = movie ?? new Movies(),
                //Comments = comments ?? new List<Comment>() // Đảm bảo không có giá trị null
            };

            return View(model);
        }


        private async Task<Movies> GetMovieFromApiAsync(Guid moviesId)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7271/api/movie/");
                    var response = await client.GetAsync($"{moviesId}");

                    if (response.IsSuccessStatusCode)
                    {
                        var movieData = await response.Content.ReadFromJsonAsync<Movies>();

                        // Log để kiểm tra giá trị của Url
                        _logger.LogInformation("Movie Url: {Url}", movieData.Type);

                        return movieData;
                    }
                    else
                    {
                        // Xử lý khi không lấy được dữ liệu
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                return null;
            }
        }
        public async Task<IActionResult> MovieDetails(Guid moviesId)
        {
            var movie = await _context.Movies
                .Include(m => m.MoviesActors)
                .ThenInclude(ma => ma.Actor)
                .FirstOrDefaultAsync(m => m.Id == moviesId);

            if (movie == null)
            {
                return NotFound();
            }

            // Gửi dữ liệu phim đến view
            var viewModel = new MovieDetailsViewModel
            {
                Id = movie.Id,
                Title = movie.Title,
                Url = movie.Url,
                Description = movie.Description,
                AgeRating = movie.AgeRating,
                Poster = movie.Poster,
                Actors = movie.MoviesActors.Select(ma => ma.Actor.Name).ToList()
            };

            return View(viewModel);
        }

        //private async Task<List<Comment>> GetCommentsFromApiAsync(Guid moviesId)
        //{
        //    try
        //    {
        //        using (var client = new HttpClient())
        //        {
        //            client.BaseAddress = new Uri("https://localhost:7271/api/comments/");
        //            var response = await client.GetAsync($"{moviesId}");

        //            if (response.IsSuccessStatusCode)
        //            {
        //                var commentsData = await response.Content.ReadFromJsonAsync<List<Comment>>();
        //                return commentsData ?? new List<Comment>(); // Đảm bảo trả về danh sách rỗng nếu không có dữ liệu
        //            }
        //            else
        //            {
        //                _logger.LogWarning("Failed to fetch comments: {StatusCode}", response.StatusCode);
        //                return new List<Comment>(); // Trả về danh sách rỗng nếu không thành công
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error fetching comments");
        //        return new List<Comment>(); // Trả về danh sách rỗng nếu có lỗi
        //    }
        //}

        public class HomeViewModel
        {
            public string baseURL { get; set; } = "https://localhost:7271";
            public Guid Id { get; set; }
            public Movies Movie { get; set; } = new Movies();
            //public List<Comment> Comments { get; set; } = new List<Comment>();
        }

    }
}