using FixIt.Core.Bases;
using FixIt.Domain.Entities;
using MediatR;

namespace FixIt.Core.Features.Categories.Query.Models
{
    public class GetCategoriesListQuery : IRequest<Response<List<Category>>>
    {

    }
}
