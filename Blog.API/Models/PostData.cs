namespace Blog.API.Models
{
    public class PostData
    {
        public long Id { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public int Score { get; set; }
    }
}