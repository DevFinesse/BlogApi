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
                    Slug = "post-one-title",
                    IsPublished = false,
                    CategoryId = new Guid("21c77926-23b5-42ce-96ea-d389e08faab8")
                },
                new Post
                {
                    Id = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                    Title = "Post Two Title",
                    Content = "Post Two Contents",
                    Slug = "post-two-title",
                    IsPublished = true,
                    CategoryId = new Guid("f67c36e4-8428-4570-858b-6c8b7edad5c3")
                }
            );
        }
    }
}
