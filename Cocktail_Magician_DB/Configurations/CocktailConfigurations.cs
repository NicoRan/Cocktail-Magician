using Cocktail_Magician_DB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cocktail_Magician_DB.Configurations
{
    public class CocktailConfigurations : IEntityTypeConfiguration<Cocktail>
    {

        public void Configure(EntityTypeBuilder<Cocktail> builder)
        {
            builder
                .HasKey(k => k.CocktailId);

            builder
                .Property(cocktail => cocktail.Name)
                .HasMaxLength(35)
                .IsRequired();


            builder
                .HasIndex(cocktail => cocktail.Name)
                .IsUnique();

        }
    }
}
