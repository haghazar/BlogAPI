using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BlogApi.Models
{
    public class Post 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Author { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();

        public bool IsDeleted { get; set; }
        //public DateTime? DeletedOnUtc { get ; set ; }
    }
}

