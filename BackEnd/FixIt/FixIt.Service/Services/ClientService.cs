using FixIt.Domain.Entities;
using FixIt.Infrastructure.Abstracts;
using FixIt.Service.Abstracts;
using Microsoft.AspNetCore.Http;

namespace FixIt.Service.Services
{
    internal class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IFileService _fileService;


        public ClientService(IClientRepository clientRepository, IFileService fileService)
        {
            _clientRepository = clientRepository;
            _fileService = fileService;
        }
        public async Task<User> GetClientById(object id)
        {
            return await _clientRepository.GetByIdAsync(id);
        }

        public async Task<String> EditClinetAsync(User user)
        {
            await _clientRepository.UpdateAsync(user);
            return "success";
        }

        public async Task<List<User>> GetAllClientAsync()
        {
            return await _clientRepository.GetAllAsync();
        }

        public async Task<string> DeleteClientAsync(User user)
        {
            await _clientRepository.DeleteAsync(user);
            return "success";
        }

        public async Task<string> ChangeClinetPasswordAsync(User user, string passward)
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(passward);
            await _clientRepository.UpdateAsync(user);
            return "success";
        }

        public async Task<string> ChangeClientImage(User client, IFormFile file)
        {

            var ImgUrl = await _fileService.UploadImage("Clients", file);
            client.ImgUrl = ImgUrl;

            switch (ImgUrl)
            {
                case "No Image !!": return "No Image !!";
                case "Feild to Uplaod !!": return "Feild to Uplaod !!";
            }

            try
            {
                await _clientRepository.UpdateAsync(client);
                return "success";

            }
            catch (Exception)
            {

                return "FaildinAdd";
            }




        }
    }
}
