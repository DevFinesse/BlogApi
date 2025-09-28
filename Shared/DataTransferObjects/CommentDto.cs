namespace Shared.DataTransferObjects
{
    public record CommentDto
    {
        public Guid Id { get; init; }
        public string? Content { get; init; }
        public DateTimeOffset CreatedAt { get; init; }
        public DateTimeOffset? LastUpdatedAt { get; init; }
        public Guid? PostId { get; init; }
        public Guid? ParentCommentId { get; init; }
        public int Depth { get; init; } = 0;
        public int ReplyCount { get; init; } = 0;

        public List<CommentDto>? Replies { get; init; }

    }
}
