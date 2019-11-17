using Cocktail_Magician_DB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cocktail_Magician_DB.Configurations
{
    public class CocktailReviewConfigurations : IEntityTypeConfiguration<CocktailReview>
    {
        public void Configure(EntityTypeBuilder<CocktailReview> builder)
        {
            builder
                .HasKey(cr => new { cr.UserId, cr.CocktailId });

            builder
                .HasOne(cr => cr.User)
                .WithMany(u => u.CocktailReviews)
                .HasForeignKey(cr => cr.UserId);

            builder
                .HasOne(cr => cr.Cocktail)
                .WithMany(c => c.CocktailReviews)
                .HasForeignKey(cr => cr.CocktailId);

            builder
                .Property(cr => cr.Grade)
                .HasColumnType("numeric(18, 2)")
                .IsRequired();

            builder
                .Property(cc => cc.Comment)
                .IsRequired();
        }
    }
}
