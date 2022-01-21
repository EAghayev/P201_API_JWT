using BookStore.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Data.Configurations
{
    class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.Property(x => x.FullName).HasMaxLength(50).IsRequired(true);
            builder.Property(x => x.BornYear).IsRequired(true);
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
