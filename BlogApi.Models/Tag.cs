using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogApi.Models
{
    public class Tag 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Post> Posts { get; set; } = new List<Post>();
        public bool IsDeleted { get; set; }
        //public DateTime? DeletedOnUtc { get ; set ; }
    }
}

