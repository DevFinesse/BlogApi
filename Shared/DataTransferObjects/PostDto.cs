using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record PostDto
    {
        public Guid Id { get; init; }
        public string? Title { get; init; }
        public string? Content { get; init; }
        public bool? IsPublished { get; init; }

        public string? Slug {get; init;}
        public CategoryDto? Category { get; init; }
        public DateTimeOffset? CreatedAt { get; init; }
        public DateTimeOffset? UpdatedAt { get; init; }

    }
}
