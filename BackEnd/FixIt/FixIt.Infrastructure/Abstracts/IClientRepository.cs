using FixIt.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Infrastructure.Abstracts
{
    public interface IClientRepository :IGenericRepositoryAsync<User>
    {
        Task<List<Favorite>> GetAllFavoritesByClientId(object Clientid);
    }
}
