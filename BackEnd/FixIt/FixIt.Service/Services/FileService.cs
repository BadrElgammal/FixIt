using FixIt.Service.Abstracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace FixIt.Service.Services
{
    public class FileService : IFileService
    {

        private readonly IWebHostEnvironment _webHost;


        public FileService(IWebHostEnvironment webHost)
        {
            _webHost = webHost;
        }


        public async Task<string> UploadImage(string location, IFormFile file)
        {
            var path = _webHost.ContentRootPath + "/" + location + "/";
            var extention = Path.GetExtension(file.FileName);
            var fileName = Guid.NewGuid().ToString().Replace("-", string.Empty) + extention;


            if (file.Length > 0)
            {
                try
                {

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);

                    }
                    using (FileStream fileStream = File.Create(path + fileName))
                    {
                        await file.CopyToAsync(fileStream);
                        await fileStream.FlushAsync();

                        return $"{path}/{fileName}";
                    }
                }
                catch (Exception)
                {

                    return "Feild to Uplaod !!";
                }

            }

            else
            {
                return "No Image !!";
            }



        }
    }
}
