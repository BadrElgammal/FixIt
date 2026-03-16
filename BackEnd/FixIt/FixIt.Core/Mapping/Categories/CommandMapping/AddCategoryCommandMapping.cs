using FixIt.Core.Features.Categories.Command.Models;
using FixIt.Domain.Entities;

namespace FixIt.Core.Mapping.Categories
{
    partial class CategoryMapper
    {
        public void AddCategoryMapping()
        {
            CreateMap<AddCategoryCommand, Category>();
        }


    }
}
