using Cocktail_Magician_DB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cocktail_Magician_DB.Configurations
{
    public class BarRatingConfigurations : IEntityTypeConfiguration<BarRating>
    {
        public void Configure(EntityTypeBuilder<BarRating> builder)
        {
            builder
                .HasKey(br => new { br.UserId, br.BarId });

            builder
                .HasOne(br => br.Bar)
                .WithMany(u => u.BarRatings)
                .HasForeignKey(br => br.BarId);

            builder
                .HasOne(u => u.User)
                .WithMany(b => b.BarRatings)
                .HasForeignKey(fk=>fk.UserId);

            builder
                .Property(br => br.Grade)
                .HasColumnType("numeric(18, 2)")
                .IsRequired();
        }
    }
}
