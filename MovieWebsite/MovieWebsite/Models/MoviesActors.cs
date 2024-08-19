namespace MovieWebsite.Models
{
    public class MoviesActors
    {
        public Guid MoviesId { get; set; }
        public Movie Movie { get; set; }

        public Guid ActorsId { get; set; }
        public Actor Actor { get; set; }
    }
}
