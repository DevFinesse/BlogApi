using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Entities.Models
{
    public class Comment
    {
        [Column("CommentId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage ="Content cannot be empty")]
        [MaxLength(1000, ErrorMessage = "Content cannot exceed 1000 characters")]
        public string? Content { get; set; }
        public DateTimeOffset? CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? LastUpdatedAt { get; set; }
        
        [ForeignKey(nameof(ParentComment))]
        public Guid? ParentCommentId { get; set; } 
        public Comment? ParentComment { get; set; }
        public ICollection<Comment>? Replies { get; set; }

        [ForeignKey(nameof(Post))]
        public Guid PostId { get; set; }
        public Post? Post { get; set; }

    }
}
