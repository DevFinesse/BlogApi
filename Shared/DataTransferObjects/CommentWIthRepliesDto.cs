using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record CommentWithRepliesDto
    {
        public Guid Id { get; init; }
        public string? Content { get; init; }

    }
}
