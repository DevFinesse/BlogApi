namespace Shared.DataTransferObjects
{
    public record CommentDto
    {
        public Guid Id { get; init; }
        public string? Content { get; init; }
        public DateTimeOffset CreatedAt { get; init; }
        public Guid? PostId { get; init; }
        public Guid? ParentCommentId { get; init; }
        //public int? Depth { get; init; }

        public List<CommentDto>? Replies { get; init; }

    }
}
