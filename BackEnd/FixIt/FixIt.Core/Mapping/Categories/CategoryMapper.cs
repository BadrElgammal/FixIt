using AutoMapper;

namespace FixIt.Core.Mapping.Categories
{
    partial class CategoryMapper : Profile
    {
        public CategoryMapper()
        {
            AddCategoryMapping();
            EditeCategoryMapping();

        }

    }
}
