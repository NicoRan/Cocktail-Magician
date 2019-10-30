using Cocktail_Magician_DB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cocktail_Magician_DB.Configurations
{
    internal class BarCocktailConfigurations : IEntityTypeConfiguration<BarCocktail>
    {
        public void Configure(EntityTypeBuilder<BarCocktail> builder)
        {
            builder
                .HasKey(bc => new { bc.BarId, bc.CocktailId });

            builder
                .HasOne(bc => bc.Bar)
                .WithMany(c => c.BarCocktails)
                .HasForeignKey(bc => bc.BarId);

            builder
                .HasOne(bc => bc.Cocktail)
                .WithMany(c => c.BarCocktails)
                .HasForeignKey(bc => bc.CocktailId);
        }
    }
}
