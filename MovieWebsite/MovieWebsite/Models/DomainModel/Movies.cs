namespace MovieWebsite.Model.DomainModel
{
    public class Movies
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string AgeRating{get; set;}
        public string Status { get; set; } = "Hiển thị";
        public string Poster { get; set; }
        public int ExpectedEpisode {  get; set; }
        public bool Type { get; set;}
        //true : phim bo, false : phim le 
        public bool SetType { set => Type = ExpectedEpisode > 1; }
    }
}