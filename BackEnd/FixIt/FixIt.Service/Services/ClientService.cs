using FixIt.Domain.Entities;
using FixIt.Infrastructure.Abstracts;
using FixIt.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Service.Services
{
    internal class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
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

    }
}
