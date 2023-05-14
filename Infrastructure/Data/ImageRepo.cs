using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.RepoInterfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Data
{
    public class ImageRepo : IImage
    {
        //UploadImage
        public async Task<string> UploadImageAsync(IFormFile file, string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"Resources\Images", fileName);
            using Stream fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);
            return GetServerRelativePath(fileName);
        }

        private string GetServerRelativePath(string fileName)
        {
            return Path.Combine(@"Resources\Images", fileName);
        }
    }
}