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
                        Id = 1,
                        Name = "Category One"
                    },
                    new Category
                    {
                        Id = 2,
                        Name = "Category 2"
                    }
                );
        }
    }
}
