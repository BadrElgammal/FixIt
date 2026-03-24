using FixIt.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace FixIt.Service.Abstracts
{
    public interface IClientService
    {
        Task<List<User>> GetAllClientAsync();
        Task<User> GetClientById(object id);
        Task<String> EditClinetAsync(User user);
        Task<String> DeleteClientAsync(User user);
        Task<String> ChangeClinetPasswordAsync(User user, string passward);
        public Task<string> ChangeClientImage(User user, IFormFile file);


    }
}
