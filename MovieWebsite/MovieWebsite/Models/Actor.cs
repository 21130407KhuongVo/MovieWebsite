namespace MovieWebsite.Models
{
    public class Actor
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public DateTime Birthdate { get; set; }
        public bool Sex { get; set; }
        public string Country { get; set; }
        public string Avatar { get; set; }
        // Navigation Property
        public ICollection<MoviesActors> MoviesActors { get; set; }
    }
}
