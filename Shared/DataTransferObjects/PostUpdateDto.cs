using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record PostUpdateDto
    {
        [Required(ErrorMessage ="Title is Required")]
        [MaxLength(100,ErrorMessage ="Title cannot be 100 characters")]

        public string? Title { get; init; }
        [Required(ErrorMessage ="Content is required")]
        public string? Content { get; set; }
        public bool IsPublished { get; init; }
        public Guid CategoryId { get; init; }
    }
}
