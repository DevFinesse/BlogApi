using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record CommentCreationDto
    {
        [Required(ErrorMessage ="Content is required")]
        public string? Content { get; set; }
        public Guid? ParentCommentId { get; set; }
    }
}
