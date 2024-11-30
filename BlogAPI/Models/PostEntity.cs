namespace BlogAPI.Models
{
    public class PostEntity
    {
        public Guid Id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public ICollection<PostTagEntity> PostTags { get; set; } = [];
    }
}

