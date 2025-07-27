using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Post
    {
        [Column("PostId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage ="Title is required")]
        [MaxLength(50, ErrorMessage ="Title cannot be more than 50 characters")]
        public string? Title { get; set; }

        [Required(ErrorMessage ="Content cannot be empty")]
        [MinLength(10, ErrorMessage ="Content cannot be less than 10 characters")]
        public string? Content { get; set; }

        [Required(ErrorMessage ="Slug is required")]
        public string? Slug { get; set; }
        public DateTimeOffset? CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? LastUpdatedAt { get; set; } = DateTimeOffset.UtcNow;

        [Required(ErrorMessage ="Status is required")]
        public bool? IsPublished { get; set; }

        [ForeignKey(nameof(Category))]
        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }

        public ICollection<Comment>? Comments { get; set; }
    }
}
