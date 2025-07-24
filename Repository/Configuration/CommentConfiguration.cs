using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Configuration
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasData
                (
                    new Comment
                    {
                        Id = 1,
                        Content = "Comment one",
                        PostId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870")
                    },
                     new Comment
                     {
                         Id = 2,
                         Content = "Comment Two",
                         PostId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3")
                     },
                     new Comment
                     {
                         Id = 3,
                         Content = "Comment Three",
                         PostId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3")
                     }
                );
        }
    }
}
