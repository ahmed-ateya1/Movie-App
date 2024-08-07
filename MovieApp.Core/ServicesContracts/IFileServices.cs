using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Core.ServicesContracts
{
    public interface IFileServices 
    {
        Task<string> CreateFile(IFormFile file);
        Task DeleteFile(string? imageUrl);
        Task<string> UpdateFile(IFormFile newFile, string? currentFileName);
    }
}
