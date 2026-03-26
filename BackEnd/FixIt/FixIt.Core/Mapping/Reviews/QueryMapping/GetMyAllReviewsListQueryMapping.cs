using FixIt.Core.Features.Reviews.Query.DTOs;
using FixIt.Domain.Entities;

namespace FixIt.Core.Mapping.Reviews
{
    public partial class ReviewMapper
    {
        public void GetMyAllReviewsListQueryMapping()
        {
            CreateMap<Review, ReviewDTO>()
                               .ForMember(dest => dest.ReviewerName, opt => opt.MapFrom(src => src.Reviewer.FullName))
                               .ForMember(dest => dest.ReviewerImgUrl, opt => opt.MapFrom(src => src.Reviewer.ImgUrl))
                               .ForMember(dest => dest.ReviewerRole, opt => opt.MapFrom(src => src.Reviewer.Role));

        }


    }
}
