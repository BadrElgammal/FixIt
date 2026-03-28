using FixIt.Core.Features.Reviews.Query.DTOs;
using FixIt.Domain.Entities;

namespace FixIt.Core.Mapping.Reviews
{
    public partial class ReviewMapper
    {
        public void ReviewsListMapper()
        {
            CreateMap<Review, ReviewDTO>()
                            .ForMember(dest => dest.ReviewerName, opt => opt.MapFrom(src => src.Reviewer.FullName))
                            .ForMember(dest => dest.ReviewerImgUrl, opt => opt.MapFrom(src => src.Reviewer.ImgUrl))
                            .ForMember(dest => dest.ReviewerRole, opt => opt.MapFrom(src => src.Reviewer.Role));


            CreateMap<WorkerProfile, ReviewForWorkerDTO>()
                .ForMember(dest => dest.WorkerName, opt => opt.MapFrom(src => src.User.FullName))
                .ForMember(dest => dest.ImgUrl, opt => opt.MapFrom(src => src.User.ImgUrl))
                //ReviewsForWorker  <==> Worker   ===>    RwviewsDTO
                .ForMember(dest => dest.Reviews, opt => opt.MapFrom(src => src.Reviews));

        }


    }
}
