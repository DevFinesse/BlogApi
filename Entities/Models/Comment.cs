using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Entities.Models
{
    public class Comment
    {
        [Column("CommentId")]
        public int Id { get; set; }
        //comment text,author ID, post ID,timestamp,parent comment  ID(threaded reply)

        [Required(ErrorMessage ="Content cannot be empty")]
        public string? Content { get; set; }
        public int? ParentCommentId { get; set; }
        public Comment? ParentComment { get; set; } //navigation to comment
        public ICollection<Comment> Replies { get; set; } = []; //children comment
        public DateTimeOffset? CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? LastUpdatedAt { get; set; }

        [ForeignKey(nameof(Post))]
        public Guid PostId { get; set; }
        public Post? Post { get; set; }

    }
}
