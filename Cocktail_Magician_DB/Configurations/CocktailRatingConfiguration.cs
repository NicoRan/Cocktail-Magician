using Cocktail_Magician_DB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cocktail_Magician_DB.Configurations
{
    public class CocktailRatingConfiguration : IEntityTypeConfiguration<CocktailRating>
    {
        public void Configure(EntityTypeBuilder<CocktailRating> builder)
        {
            builder
                .HasKey(cr => new {cr.UserId, cr.CocktailId });

            builder
                .HasOne(cr => cr.User)
                .WithMany(u => u.CocktailRatings)
                .HasForeignKey(cr => cr.UserId);

            builder
                .HasOne(cr => cr.Cocktail)
                .WithMany(c => c.CocktailRatings)
                .HasForeignKey(cr => cr.CocktailId);

            builder
                .Property(cr => cr.Grade)
                .IsRequired();
        }
    }
}
