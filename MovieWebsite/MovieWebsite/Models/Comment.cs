namespace MovieWebsite.Models
{
	public class Comment
	{
		public Guid Id { get; set; }
		public string Author { get; set; }
		public string AvatarUrl { get; set; }
		public string Content { get; set; }
		public int Rating { get; set; }
		public DateTime CreateDate { get; set; }
		public DateTime? LastUpdate { get; set; }
	}

	//public class Comment
	//{
	//	public Guid Id { get; set; }
	//	public string Content { get; set; }
	//	public DateTime CreateDate { get; set; }
	//	public DateTime? LastUpdate { get; set; }
	//}

}