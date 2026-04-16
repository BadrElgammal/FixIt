using AutoMapper;

namespace FixIt.Core.Mapping.Reviews
{
    public partial class ReviewMapper : Profile
    {
        public ReviewMapper()
        {
            AddReviewCommandMapping();


        }

    }
}
