using Microsoft.AspNetCore.Http;
using MovieApp.Core.Domain.Entites;
using MovieApp.Core.Domain.RepositoryContracts;
using MovieApp.Core.DTO;
using MovieApp.Core.Helper;
using MovieApp.Core.ServicesContracts;
using OfficeOpenXml;

public class GenreServices : IGenreServices
{
    private readonly IGenreRepository _genreRepository;

    public GenreServices(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }

    public async Task<GenreResponse> AddGenre(GenreAddRequest? genreAddRequest)
    {
        if (genreAddRequest == null)
            throw new ArgumentNullException(nameof(genreAddRequest));

        ValidationModel.ValidateModel(genreAddRequest);

        var genre = genreAddRequest.ToGenre();
        genre.GenreID = Guid.NewGuid();

        await _genreRepository.AddGenre(genre);

        return genre.ToGenreResponse();
    }

    public async Task<bool?> DeleteGenre(Guid? genreID)
    {
        if (genreID == null)
            throw new ArgumentNullException(nameof(genreID));

        var genreResponse = await GetGenreByID(genreID);
        if (genreResponse == null)
            return false;

        return await _genreRepository.DeleteGenre(genreID.Value);
    }

    public async Task<IEnumerable<Genre>> GetAllGenre()
    {
        return await _genreRepository.GetAllGenre();
    }

    public async Task<GenreResponse?> GetGenreByID(Guid? genreID)
    {
        if (genreID == null)
            throw new ArgumentNullException(nameof(genreID));

        var genre = await _genreRepository.GetGenreByID(genreID.Value);
        return genre?.ToGenreResponse();
    }

    public async Task<GenreResponse> UpdateGenre(GenreUpdateRequest? genreAddRequest)
    {
        if (genreAddRequest == null)
            throw new ArgumentNullException(nameof(genreAddRequest));

        ValidationModel.ValidateModel(genreAddRequest);

        var genre = genreAddRequest.ToGenre();

        await _genreRepository.UpdateGenre(genre);

        return genre.ToGenreResponse();
    }

    public async Task<int> UploadGenresFromExcelFile(IFormFile formFile)
    {
        var memoryStream  = new MemoryStream();
        await formFile.CopyToAsync(memoryStream);
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        int genreInserted = 0;
        using(var excelPackage = new ExcelPackage(memoryStream))
        {
            ExcelWorksheet workbook = excelPackage.Workbook.Worksheets["Genres"];
            int rowsExcel = workbook.Dimension.Rows;

            for(int row = 2; row <= rowsExcel; row++)
            {
                var cellValue = workbook.Cells[row,1].Value?.ToString();

                if (!String.IsNullOrEmpty(cellValue))
                {
                    var genreName = cellValue;
                    var existingGenre = await _genreRepository.GerGenreByName(genreName);
                    if (existingGenre == null)
                    {
                        var genre = new Genre() { GenreName = genreName , GenreID = Guid.NewGuid()};
                        await _genreRepository.AddGenre(genre);
                        genreInserted++;
                    }
                }
            }
            return genreInserted;
        }
    }
}
