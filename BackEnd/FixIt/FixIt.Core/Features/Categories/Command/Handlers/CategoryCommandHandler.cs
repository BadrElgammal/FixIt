using AutoMapper;
using FixIt.Core.Bases;
using FixIt.Core.Features.Categories.Command.Models;
using FixIt.Domain.Entities;
using FixIt.Service.Abstracts;
using MediatR;

namespace FixIt.Core.Features.Categories.Command.Handlers
{
    public class CategoryCommandHandler : ResponseHandler,
               IRequestHandler<AddCategoryCommand, Response<string>>,
               IRequestHandler<EditeCategoryCommand, Response<string>>,
               IRequestHandler<DeleteCategoryCommand, Response<string>>
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryCommandHandler(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryMapper = _mapper.Map<Category>(request);
            var result = await _categoryService.AddCategoryAsync(categoryMapper);
            if (result == "success") return Success($"تم اضافة  {request.CategoryName}");
            else return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(EditeCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryService.GetCategoryByIdAsync(request.CategoryId);
            if (category == null) return NotFound<string>("هذا القسم غير موجود");

            var categoryMapper = _mapper.Map<Category>(request);
            var result = await _categoryService.UpdateCategoryAsync(categoryMapper);
            if (result == "success") return Success($"{request.CategoryName} تم التعديل");
            else return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {

            var category = await _categoryService.GetCategoryByIdAsync(request.CategoryId);
            if (category == null) return NotFound<string>("هذا القسم غير موجود");

            var result = await _categoryService.DeleteCategoryAsync(category);
            if (result == "success") return Success("تم الحذف ");
            else return BadRequest<string>();
        }
    }
}
