using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.Core.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Infrastructure.Configuration
{
    public class MovieFavouriteConfiguration : IEntityTypeConfiguration<MovieFavourite>
    {
        public void Configure(EntityTypeBuilder<MovieFavourite> builder)
        {
            builder.HasKey(new string[] { nameof(MovieFavourite.MovieID), nameof(MovieFavourite.FavouriteID) });
            builder.ToTable("MovieFavourites");
        }
    }
}
