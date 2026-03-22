using FixIt.Core.Features.Reviews.Command.Models;
using FixIt.Domain.Entities;

namespace FixIt.Core.Mapping.Reviews
{
    public partial class ReviewMapper
    {
        public void AddReviewCommandMapping()
        {
            CreateMap<AddReviewCommand, Review>()
                .ForMember(dest => dest.RequestId, opt => opt.MapFrom(src => src.RequestId));
        }

    }
}
