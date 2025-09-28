using Microsoft.EntityFrameworkCore;
using Repository;

namespace Service
{
    public static class SlugGenerator
{
    public static string GenerateSlug(string title, int maxLength = 100)
    {
        if (string.IsNullOrWhiteSpace(title))
            return string.Empty;

        var slug = title.ToLowerInvariant()
            .Replace(" ", "-")
            .Replace("[^a-z0-9-]", "") // Remove non-alphanumeric except hyphens
            .Trim('-'); // Remove leading/trailing hyphens

        if (slug.Length > maxLength)
            slug = slug.Substring(0, maxLength).TrimEnd('-');

        return slug;
    }

    public static async Task<string> EnsureUniqueSlugAsync(string slug, RepositoryContext context, Guid? postId = null)
    {
        var originalSlug = slug;
        int counter = 1;

        while (await context.Posts.AnyAsync(p => p.Slug == slug && (!postId.HasValue || p.Id != postId)))
        {
            slug = $"{originalSlug}-{counter}";
            counter++;
        }

        return slug;
    }
}
}
