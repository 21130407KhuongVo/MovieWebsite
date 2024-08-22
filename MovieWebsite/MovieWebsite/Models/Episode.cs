using System.ComponentModel.DataAnnotations;

namespace MovieWebsite.Models
{
	public class Episode
	{
		[Key]
		public Guid Id { get; set; }  // Khóa chính

		[Required]
		public int EpisodeNumber { get; set; }  // Số tập trong mùa

		public DateTime? ReleaseDate { get; set; }  // Ngày phát hành của tập phim

		public int? Duration { get; set; }  // Thời lượng của tập phim

		public string Description { get; set; }  // Mô tả ngắn về tập phim

		public string Source { get; set; }  // Đường dẫn hoặc URL đến nguồn của tập phim
		[Required]
		public Guid MovieId { get; set; }  // Khóa ngoại liên kết với Movie

		public Movies Movie { get; set; }  // Thuộc tính điều hướng liên kết với Movie
	}
}
