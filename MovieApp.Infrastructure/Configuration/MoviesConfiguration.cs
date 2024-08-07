using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.Core.Domain.Entites;

namespace MovieApp.Infrastructure.Configuration
{
    public class MoviesConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasKey(x => x.MovieID);

            builder.Property(x => x.MovieID)
                .ValueGeneratedNever();

            builder.Property(x => x.ReleaseDate)
                .HasColumnType("date")
                .IsRequired();

            builder.Property(x => x.Title)
                .HasColumnType("VARCHAR(100)")
                .IsRequired();

            builder.Property(x => x.Description)
               .HasColumnType("VARCHAR(1000)")
               .IsRequired();

            builder.Property(x => x.ImageURL)
               .HasColumnType("VARCHAR(100)")
               .IsRequired();

            builder.Property(x => x.Rating)
                .IsRequired();

            builder.HasOne(x => x.Genre)
                .WithMany(x => x.Movies)
                .HasForeignKey(x => x.GenreID)
                .OnDelete(DeleteBehavior.Restrict);

           
            builder.HasMany(x => x.Favourites)
                .WithMany(x => x.Movies)
                .UsingEntity<MovieFavourite>();
           
            builder.ToTable("Movies");
        }
    }
}
