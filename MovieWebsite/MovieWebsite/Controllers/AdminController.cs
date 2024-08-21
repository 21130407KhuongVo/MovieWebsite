using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieWebsite.Model.DomainModel;

namespace MovieWebsite.Controllers
{
    public class AdminController : Controller
    {
        // GET: /Admin/Index
        public IActionResult Index()
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

            // insert Movies. Get from API
            model.MovieList = new List<Movies>{
                new Movies{
                    Id= Guid.NewGuid(),
                    Title = "Doraemon Movie 2024: Nobita Và Bản Giao Hưởng Địa Cầu",
                    Description = "this is Doraemon film",
                    Status="Hiển thị",
                    AgeRating = "P",
                    Poster = "https://vnw-img-cdn.popsww.com/api/v2/containers/file2/cms_topic/horizontal_poster__1_-72c22681f3d2-1715248196841-TXI02OfZ.png?v=0&maxW=600"
                },
                new Movies{
                    Id=Guid.NewGuid(),
                    Title = "The Boys",
                    Description = "This is The boys film",
                    Status="Ẩn",
                    AgeRating = "T16",
                    Poster = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSsn2C9wOt_qlYjzFN5oLuwxoAdBhs3cTnUMQ&s"
                }
            };


            return View(model);
        }
    }

    public class AdminViewModel
    {
        public List<Movies> MovieList { get; set; }
        public List<SelectListItem> AgeSelectListItems { get; set; }
        public List<SelectListItem> VisibleListItem { get; set; }
        public string SelectedAge { get; set; }
    }
}
