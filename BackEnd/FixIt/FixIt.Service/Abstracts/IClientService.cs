using FixIt.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Service.Abstracts
{
    public interface IClientService 
    {
        Task<List<User>> GetAllClientAsync();
        Task<User> GetClientById(object id);
        Task<String> EditClinetAsync(User user);
        Task<String> DeleteClientAsync(User user);
        Task<String> ChangeClinetPasswordAsync(User user, string passward);
        Task<List<Favorite>> GetAllFavoritesByClientId(object Clientid);
    }
}
