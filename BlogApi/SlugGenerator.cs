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

        // Convert to lowercase, remove special characters, replace spaces with hyphens
        var slug = title.ToLowerInvariant()
            .Replace(" ", "-")
            .Replace("[^a-z0-9-]", "") // Remove non-alphanumeric except hyphens
            .Trim('-'); // Remove leading/trailing hyphens

        // Truncate to maxLength
        if (slug.Length > maxLength)
            slug = slug.Substring(0, maxLength).TrimEnd('-');

        return slug;
    }

    public static async Task<string> EnsureUniqueSlugAsync(string slug, RepositoryContext context, Guid? postId = null)
    {
        var originalSlug = slug;
        int counter = 1;

        // Check for existing slugs, excluding the current post (if updating)
        while (await context.Posts.AnyAsync(p => p.Slug == slug && (!postId.HasValue || p.Id != postId)))
        {
            // Append a number to make it unique (e.g., my-post, my-post-1, my-post-2)
            slug = $"{originalSlug}-{counter}";
            counter++;
        }

        return slug;
    }
}
}
