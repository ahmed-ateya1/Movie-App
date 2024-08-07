using MovieApp.Core.Domain.Entites;
using MovieApp.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Core.Domain.RepositoryContracts
{
    public interface IGenreRepository
    {
        Task<Genre> AddGenre(Genre genre);
        Task<Genre> UpdateGenre(Genre genre);
        Task<bool> DeleteGenre(Guid genreID);
        Task<Genre> GetGenreByID(Guid genreID);
        Task<Genre> GerGenreByName(string genreName);
        Task<IEnumerable<Genre>> GetAllGenre();
    }
}
