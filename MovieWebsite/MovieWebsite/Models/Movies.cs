using System;
using System.Collections.Generic;

namespace MovieWebsite.Models
{
    public class Movies
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AgeRating { get; set; }
        public string Poster { get; set; }
        public int Status { get; set; }
        public string Url { get; set; }

        // Navigation Property
        public ICollection<MoviesActors> MoviesActors { get; set; }
    }
}
