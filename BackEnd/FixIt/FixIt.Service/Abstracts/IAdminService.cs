using FixIt.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace FixIt.Service.Abstracts
{
    public interface IAdminService : IService<User>
    {
        Task<string> EditeAdminAsync(User user);
        Task<string> DeleteAdminAsync(User user);
        Task<string> ChangeAdminPasswordAsync(User user, string passward);
        Task<string> ChangeClientImage(User user, IFormFile file);


    }
}
