using Microsoft.AspNetCore.Http;
using MovieApp.Core.Domain.Entites;
using MovieApp.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Core.ServicesContracts
{
    public interface IGenreServices
    {
        Task<GenreResponse> AddGenre(GenreAddRequest? genreAddRequest);
        Task<GenreResponse> UpdateGenre(GenreUpdateRequest? genreAddRequest);
        Task<bool?> DeleteGenre(Guid? genreID);
        Task<GenreResponse?> GetGenreByID(Guid? genreID);
        Task<IEnumerable<Genre>> GetAllGenre();
        Task<int> UploadGenresFromExcelFile(IFormFile formFile);
    }
}
