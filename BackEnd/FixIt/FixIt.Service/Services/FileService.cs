using FixIt.Service.Abstracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace FixIt.Service.Services
{
    public class FileService : IFileService
    {

        private readonly IWebHostEnvironment _webHost;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FileService(IWebHostEnvironment webHost, IHttpContextAccessor httpContextAccessor)
        {
            _webHost = webHost;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<string> UploadImage(string location, IFormFile file)
        {

            var path = Path.Combine(_webHost.WebRootPath, location);//_webHost.ContentRootPath + "/" + location + "/";
            var extention = Path.GetExtension(file.FileName);
            var fileName = Guid.NewGuid().ToString().Replace("-", string.Empty) + extention;

            var fullPath = Path.Combine(path, fileName);


            if (file.Length > 0)
            {
                try
                {

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);

                    }


                    using (FileStream fileStream = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                        await fileStream.FlushAsync();

                    }

                    // return $"/{location}/{fileName}";



                    //// --- الجزء الجديد هنا ---
                    //// بنجيب الطلب الحالي (Request)
                    var request = _httpContextAccessor.HttpContext.Request;

                    //// بنركب الـ Base URL (سواء كان localhost أو رابط سيرفر حقيقي)
                    //// النتيجة هتكون حاجة زي https://fixit.com أو http://localhost:7066
                    var baseUrl = $"{request.Scheme}://{request.Host}";

                    //// بنرجع الرابط الكامل عشان يتخزن في الداتابيز
                    return $"{baseUrl}/{location}/{fileName}";



                    //using (FileStream fileStream = File.Create(path + fileName))
                    //{
                    //    await file.CopyToAsync(fileStream);
                    //    await fileStream.FlushAsync();

                    //    return $"{path}/{fileName}";
                    //}
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
