using FixIt.Domain.Entities;
using FixIt.Infrastructure.Abstracts;
using FixIt.Service.Abstracts;
using Microsoft.AspNetCore.Http;

namespace FixIt.Service.Services
{
    public class AdminService : GenericService<User>, IAdminService
    {
        private readonly IAdminRepository _adminRepo;
        private readonly IFileService _fileService;

        public AdminService(IAdminRepository adminRepo, IFileService fileService) : base(adminRepo)
        {
            _adminRepo = adminRepo;
            _fileService = fileService;
        }

        public async Task<string> ChangeAdminPasswordAsync(User user, string passward)
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(passward);
            await _adminRepo.UpdateAsync(user);

            return "success";
        }

        public async Task<string> ChangeClientImage(User admin, IFormFile file)
        {

            var ImgUrl = await _fileService.UploadImage("Admins", file);
            admin.ImgUrl = ImgUrl;

            switch (ImgUrl)
            {
                case "No Image !!": return "No Image !!";
                case "Feild to Uplaod !!": return "Feild to Uplaod !!";
            }

            try
            {
                await _adminRepo.UpdateAsync(admin);
                return "success";

            }
            catch (Exception)
            {

                return "FaildinAdd";
            }

        }

        public async Task<string> DeleteAdminAsync(User user)
        {
            await _adminRepo.DeleteAsync(user);
            return "success";
        }

        public async Task<string> EditeAdminAsync(User user)
        {
            await _adminRepo.UpdateAsync(user);
            return "success";
        }
    }
}
