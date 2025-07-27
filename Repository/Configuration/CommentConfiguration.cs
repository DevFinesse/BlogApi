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
                        Id = new Guid("0bd63e36-ff8e-409f-84a6-ffba66186e48"),
                        Content = "Comment one",
                        PostId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870")
                    },
                     new Comment
                     {
                         Id = new Guid("8faac1fb-eadc-4b12-9f48-ff9632fea906"),
                         Content = "Comment Two",
                         PostId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3")
                     },
                     new Comment
                     {
                         Id = new Guid("8e330783-ccb4-47be-883d-dad77dac71c1"),
                         Content = "Comment Three",
                         PostId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3")
                     }
                );
        }
    }
}
