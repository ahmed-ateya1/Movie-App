using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.Core.Domain.Entites;

namespace MovieApp.Infrastructure.Configuration
{
    public class GenresConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasKey(x => x.GenreID);
            builder.Property(x => x.GenreID)
                .ValueGeneratedNever();

            builder.Property(x => x.GenreName)
                .HasColumnType("VARCHAR(50)")
                .IsRequired();
           

            builder.ToTable("Genres");
        }
    }
}
