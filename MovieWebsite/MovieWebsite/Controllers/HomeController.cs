using Microsoft.AspNetCore.Mvc;
using MovieWebsite.Data;
using MovieWebsite.Models;
using System.Diagnostics;

namespace MovieApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public Movies Movie { get; private set; }
		public Episode Episode { get; private set; }

		public HomeController(ILogger<HomeController> logger)
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
			var comments = await GetCommentsFromApiAsync(moviesId);
			var episodes = await GetEpisodesFromApiAsync(moviesId); // Fetch the list of episodes

			// Get the source of the first episode, if it exists
			var firstEpisodeSource = episodes?.FirstOrDefault()?.Source ?? string.Empty;

			var model = new HomeViewModel
			{
				Id = moviesId,
				baseURL = "https://localhost:7271",
				Movie = movie ?? new Movies(),
				Comments = comments ?? new List<Comment>(),
				EpisodeSource = firstEpisodeSource, // Use the source from the first episode
				Episodes = episodes // Pass the list of episodes to the view model
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
						_logger.LogInformation("Movie Url: {Url}", movieData.Url);
						return movieData;
					}
					else
					{
						return null;
					}
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error fetching movie");
				return null;
			}
		}

		private async Task<List<Comment>> GetCommentsFromApiAsync(Guid moviesId)
		{
			try
			{
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri("https://localhost:7271/api/");
					var response = await client.GetAsync($"{moviesId}/comment");

					if (response.IsSuccessStatusCode)
					{
						var commentsData = await response.Content.ReadFromJsonAsync<List<Comment>>();
						_logger.LogInformation("Fetched comments: {Comments}", commentsData); // Log fetched comments
						return commentsData ?? new List<Comment>(); // Return the comments
					}
					else
					{
						_logger.LogWarning("Failed to fetch comments: {StatusCode}", response.StatusCode);
						return new List<Comment>(); // Return an empty list if failed
					}
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error fetching comments");
				return new List<Comment>(); // Return an empty list in case of error
			}
		}


		private async Task<List<Episode>> GetEpisodesFromApiAsync(Guid moviesId)
		{
			try
			{
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri("https://localhost:7271/api/");
					var response = await client.GetAsync($"{moviesId}/episode");

					if (response.IsSuccessStatusCode)
					{
						var episodesData = await response.Content.ReadFromJsonAsync<List<Episode>>();
						return episodesData ?? new List<Episode>();
					}
					else
					{
						_logger.LogWarning("Failed to fetch episodes: {StatusCode}", response.StatusCode);
						return new List<Episode>();
					}
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error fetching episodes");
				return new List<Episode>();
			}
		}
	}

	public class HomeViewModel
	{
		public string baseURL { get; set; } = "https://localhost:7271";
		public Guid Id { get; set; }
		public Movies Movie { get; set; } = new Movies();
		public List<Comment> Comments { get; set; } = new List<Comment>();
		public string EpisodeSource { get; set; } // This will hold the source of the first episode for the iframe
		public List<Episode> Episodes { get; set; } = new List<Episode>(); // This will hold the list of episodes
	}
}
