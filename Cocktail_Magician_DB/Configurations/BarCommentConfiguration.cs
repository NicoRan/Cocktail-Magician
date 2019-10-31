using Cocktail_Magician_DB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cocktail_Magician_DB.Configurations
{
    class BarCommentConfiguration : IEntityTypeConfiguration<BarComment>
    {
        public void Configure(EntityTypeBuilder<BarComment> builder)
        {

            builder
                .HasKey(key => new { key.UserId ,key.BarId });

            builder
                .HasOne(bc => bc.User)
                .WithMany(u => u.BarComments)
                .HasForeignKey(fk => fk.UserId);

            builder
                .HasOne(bc => bc.Bar)
                .WithMany(b => b.BarComments)
                .HasForeignKey(fk => fk.BarId);

            builder
                .Property(bc => bc.Comment)
                .IsRequired();

        }
    }
}
