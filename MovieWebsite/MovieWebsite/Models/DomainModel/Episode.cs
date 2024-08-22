using System.ComponentModel.DataAnnotations;

namespace MovieWebsite.Model.DomainModel
{
    public class Episode
    {
        public Guid Id { get; set; }  // Khóa chính

        public int EpisodeNumber { get; set; }  // Số tập trong mùa

        public DateTime? ReleaseDate { get; set; }  // Ngày phát hành của tập phim

        public int? Duration { get; set; }  // Thời lượng của tập phim

        public string Description { get; set; }  // Mô tả ngắn về tập phim

        public string Source { get; set; }  // Đường dẫn hoặc URL đến nguồn của tập phim

        public Guid MovieId { get; set; }  // Khóa ngoại liên kết với Movie

        public Movies Movie { get; set; }  // Thuộc tính điều hướng liên kết với Movie
    }
}
