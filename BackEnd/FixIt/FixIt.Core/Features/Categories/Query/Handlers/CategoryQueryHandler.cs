using FixIt.Core.Bases;
using FixIt.Core.Features.Categories.Query.Models;
using FixIt.Domain.Entities;
using FixIt.Service.Abstracts;
using MediatR;

namespace FixIt.Core.Features.Categories.Query.Handlers
{
    public class CategoryQueryHandler : ResponseHandler,
                IRequestHandler<GetCategoriesListQuery, Response<List<Category>>>
    {
        private readonly ICategoryService _categoryService;

        public CategoryQueryHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<Response<List<Category>>> Handle(GetCategoriesListQuery request, CancellationToken cancellationToken)
        {
            var Categories = await _categoryService.GetAllCategoriesAsync();
            return Success(Categories);
        }



    }
}
