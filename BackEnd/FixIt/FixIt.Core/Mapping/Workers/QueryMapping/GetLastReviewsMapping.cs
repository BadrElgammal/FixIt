using FixIt.Core.Features.Workers.Queries.DTOs;
using FixIt.Domain.Entities;

namespace FixIt.Core.Mapping.Workers
{
    public partial class WorkerProfileMapper
    {

        public void GetLastReviewsMapping()
        {
            CreateMap<Review, LastReviewDTO>()
                 .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
                  .ForMember(dest => dest.Rate, opt => opt.MapFrom(src => src.Rate))
                  .ForMember(dest => dest.ReviewerName, opt => opt.MapFrom(src => src.Reviewer.FullName))
                  .ForMember(dest => dest.ReviewerImgUrl, opt => opt.MapFrom(src => src.Reviewer.ImgUrl));


        }

    }
}
