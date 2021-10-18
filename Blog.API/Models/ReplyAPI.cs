namespace Blog.API.Models
{
    public class ReplyAPI
    {
        public string Author { get; set; }
        public string Content { get; set; }
        public int Score { get; set; }
        public long ParentId { get; set; }
    }
}