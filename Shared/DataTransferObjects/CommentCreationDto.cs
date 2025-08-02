using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public record CommentCreationDto
    {
        [Required(ErrorMessage ="Content is required")]
        public string? Content { get; init; }
        public Guid? ParentCommentId { get; init; } 
    }
}
