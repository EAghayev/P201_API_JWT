using BookStore.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Data.Configurations
{
    class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(150).IsRequired(true);
            builder.Property(x => x.Desc).HasMaxLength(500).IsRequired(false);
            builder.Property(x => x.CostPrice).IsRequired(true);
            builder.Property(x => x.SalePrice).IsRequired(true);
            builder.HasOne(x => x.Author).WithMany(x => x.Books).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
