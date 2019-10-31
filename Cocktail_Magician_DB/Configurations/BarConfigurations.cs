using Cocktail_Magician_DB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cocktail_Magician_DB.Configurations
{
    public class BarConfigurations : IEntityTypeConfiguration<Bar>
    {
        public void Configure(EntityTypeBuilder<Bar> builder)
        {
            builder
                .HasKey(bar => bar.BarId);

            builder
                .Property(bar => bar.Name)
                .HasMaxLength(35)
                .IsRequired();

            builder
                .Property(bar => bar.Address)
                .IsRequired();


            builder
                .HasIndex(bar => bar.Name)
                .IsUnique();

        }
    }
}
