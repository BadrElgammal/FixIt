using FixIt.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Service.Abstracts
{
    public interface IFavoritesService
    {
        Task<List<Favorite>> GetAllFavoritesByUserId(object userId);

    }
}
