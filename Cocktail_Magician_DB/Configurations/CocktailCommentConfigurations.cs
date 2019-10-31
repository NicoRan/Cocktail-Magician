using Cocktail_Magician_DB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cocktail_Magician_DB.Configurations
{
    public class CocktailCommentConfigurations : IEntityTypeConfiguration<CocktailComment>
    {
        public void Configure(EntityTypeBuilder<CocktailComment> builder)
        {
            builder
                .HasKey(cc => new { cc.UserId, cc.CocktailId });

            builder
                .HasOne(cc => cc.User)
                .WithMany(u => u.CocktailComments)
                .HasForeignKey(cc => cc.UserId);

            builder
                .HasOne(cc => cc.Cocktail)
                .WithMany(c => c.CocktailComments)
                .HasForeignKey(cc => cc.CocktailId);

            builder
                .Property(cc => cc.Comment)
                .IsRequired();
        }
    }
}
