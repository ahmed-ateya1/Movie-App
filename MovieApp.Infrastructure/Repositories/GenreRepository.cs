using Microsoft.EntityFrameworkCore;
using MovieApp.Core.Domain.Entites;
using MovieApp.Core.Domain.RepositoryContracts;
using MovieApp.Infrastructure.ApplicationDbContext;

public class GenreRepository : IGenreRepository
{
    private readonly AppDbContext _db;

    public GenreRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Genre> AddGenre(Genre genre)
    {
        await _db.Genres.AddAsync(genre);
        await _db.SaveChangesAsync();
        return genre;
    }

    public async Task<bool> DeleteGenre(Guid genreID)
    {
        var genre = await GetGenreByID(genreID);
        if (genre == null)
            return false;

        _db.Genres.Remove(genre);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<Genre> GerGenreByName(string genreName)
    {
        return await _db.
            Genres.
            FirstOrDefaultAsync(x => x.GenreName == genreName);
    }

    public async Task<IEnumerable<Genre>> GetAllGenre()
    {
        return await _db.Genres.AsNoTracking().ToListAsync();
    }

    public async Task<Genre> GetGenreByID(Guid genreID)
    {
        return await _db.Genres.AsNoTracking().FirstOrDefaultAsync(g => g.GenreID == genreID);
    }

    public async Task<Genre> UpdateGenre(Genre genre)
    {
        var existingGenre = await GetGenreByID(genre.GenreID);
        if (existingGenre == null)
            throw new ArgumentNullException(nameof(existingGenre));

        existingGenre.GenreName = genre.GenreName;
        _db.Genres.Update(existingGenre);
        await _db.SaveChangesAsync();
        return existingGenre;
    }
}
