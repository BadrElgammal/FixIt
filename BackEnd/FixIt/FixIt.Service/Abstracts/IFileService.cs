using Microsoft.AspNetCore.Http;

namespace FixIt.Service.Abstracts
{
    public interface IFileService
    {
        public Task<string> UploadImage(string location, IFormFile file);
    }
}
