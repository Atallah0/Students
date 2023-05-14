using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Core.RepoInterfaces
{
    public interface IImage
    {
        Task<string> UploadImageAsync(IFormFile file, string fileName);
    }
}