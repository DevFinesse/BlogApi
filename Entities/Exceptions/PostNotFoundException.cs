namespace Entities.Exceptions
{
    public sealed class PostNotFoundException : NotFoundException
    {
        public PostNotFoundException(Guid postId) 
            : base($"The Post with the {postId} does not exist in the database") 
        { }
    }
}
