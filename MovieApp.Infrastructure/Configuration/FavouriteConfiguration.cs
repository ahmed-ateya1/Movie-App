using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.Core.Domain.Entites;

namespace MovieApp.Infrastructure.Configuration
{
    public class FavouriteConfiguration : IEntityTypeConfiguration<Favourite>
    {
        public void Configure(EntityTypeBuilder<Favourite> builder)
        {
            builder.HasKey(x => x.FavouriteID);

            builder.Property(x => x.FavouriteID)
                .ValueGeneratedNever();

            builder.HasOne(x => x.User)
                 .WithOne(x => x.Favourite)
                 .HasForeignKey<Favourite>(x => x.UserID)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Favourites");
        }
    }
}
