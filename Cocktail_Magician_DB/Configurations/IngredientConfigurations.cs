using Cocktail_Magician_DB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cocktail_Magician_DB.Configurations
{
    public class IngredientConfigurations : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {

            builder
                .HasKey(k => k.IngredientId);

            builder
                .Property(bar => bar.Name)
                .HasMaxLength(35)
                .IsRequired();

            builder
                .HasIndex(ingredient => ingredient.Name)
                .IsUnique();

        }
    }
}
