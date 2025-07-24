using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Repository.Configuration
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder) 
        {
            builder.HasData
            (
                new Post
                {
                    Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                    Title = "Post One Title",
                    Content = "Post one Contents",
                    Slug = "Post-One-Title",
                    Status = "Published",
                    CategoryId = 2
                },
                new Post
                {
                    Id = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                    Title = "Post Two Title",
                    Content = "Post Two Contents",
                    Slug = "Post-Two-Title",
                    Status = "Draft",
                    CategoryId = 1
                }
            );
        }
    }
}
