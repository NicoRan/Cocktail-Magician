using Cocktail_Magician_DB.Configurations;
using Cocktail_Magician_DB.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cocktail_Magician_DB
{
    public class CMContext : IdentityDbContext<User>
    {
        public CMContext()
        {

        }

        public CMContext(DbContextOptions<CMContext> options)
            : base(options)
        {

        }

        public DbSet<Bar> Bars { get; set; }
        public DbSet<Cocktail> Cocktails { get; set; }
        public DbSet<CocktailIngredient> CocktailIngredients { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new BarConfigurations());

            base.OnModelCreating(builder);
        }
    }
}
