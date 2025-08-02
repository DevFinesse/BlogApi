using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public record PostCreationDto
    {
        [Required(ErrorMessage = "Post Title is a required field")]
        [MaxLength(100, ErrorMessage = "Maximum length is 30 characters")]
        public string? Title { get; init; }

        [Required(ErrorMessage = "Content cannot be empty")]
        public string? Content { get; init; }
        public Guid CategoryId { get; init; }
        public bool IsPublished {  get; init; } = true;
      };
}
