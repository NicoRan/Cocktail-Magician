using Cocktail_Magician_DB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cocktail_Magician_DB.Configurations
{
    public class BarReviewConfigurations : IEntityTypeConfiguration<BarReview>
    {
        public void Configure(EntityTypeBuilder<BarReview> builder)
        {
            builder
                .HasKey(br => new { br.UserId, br.BarId });

            builder
                .HasOne(br => br.Bar)
                .WithMany(u => u.BarReviews)
                .HasForeignKey(br => br.BarId);

            builder
                .HasOne(u => u.User)
                .WithMany(b => b.BarReviews)
                .HasForeignKey(fk => fk.UserId);

            builder
                .Property(br => br.Grade)
                .HasColumnType("numeric(18, 2)")
                .IsRequired();

            builder
                .Property(bc => bc.Comment)
                .IsRequired();
        }
    }
}
