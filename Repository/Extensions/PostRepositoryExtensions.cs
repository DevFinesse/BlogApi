using Entities.Models;

namespace Repository.Extensions
{
    public static class PostRepositoryExtensions
    {
        public static IQueryable<Post> Search(this IQueryable<Post> posts, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return posts;

            var lowerCaseTerm = searchTerm.Trim().ToLower();

            return posts.Where(p => p.Title.ToLower().Contains(lowerCaseTerm));
        }
    }
}
