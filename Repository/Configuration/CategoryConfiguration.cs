using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData
                (
                    new Category
                    {
                        Id = new Guid("f67c36e4-8428-4570-858b-6c8b7edad5c3"),
                        Name = "Category One"
                    },
                    new Category
                    {
                        Id = new Guid("21c77926-23b5-42ce-96ea-d389e08faab8"),
                        Name = "Category 2"
                    }
                );
        }
    }
}
