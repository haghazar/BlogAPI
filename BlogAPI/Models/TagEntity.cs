namespace BlogAPI.Models
{
    public class TagEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<PostTagEntity> PostTags { get; set; } = [];
    }
}

