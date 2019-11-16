using Cocktail_Magician_DB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cocktail_Magician_DB.Configurations
{
    public class CocktailIngredientConfigurations : IEntityTypeConfiguration<CocktailIngredient>
    {
        public void Configure(EntityTypeBuilder<CocktailIngredient> builder)
        {
            builder
                .HasKey(ci => new { ci.CocktailId, ci.IngredientId });

            builder
                .HasOne(ci => ci.Cocktail)
                .WithMany(c => c.CocktailIngredient)
                .HasForeignKey(fk => fk.CocktailId);

            builder
                .HasOne(ci => ci.Ingredient)
                .WithMany(i => i.CocktailIngredient)
                .HasForeignKey(fk => fk.IngredientId);
        }
    }
}
