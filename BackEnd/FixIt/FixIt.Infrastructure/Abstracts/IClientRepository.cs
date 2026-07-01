using FixIt.Domain.Entities;

namespace FixIt.Infrastructure.Abstracts
{
    public interface IClientRepository : IGenericRepositoryAsync<User>
    {
        IQueryable<User> GetAllClientsPaginated();
        //Task<List<Payment>> GetAllPaymentsForUserByUserId(Guid userId);
    }
}
