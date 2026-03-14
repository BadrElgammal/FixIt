using FixIt.Core.Features.Workers.Commands.Models;
using FixIt.Domain.Entities;

namespace FixIt.Core.Mapping.Workers
{
    public partial class WorkerProfileMapper
    {

        // Guid WorkerId 
        // string? JobTitle 
        // string? Description 
        // decimal? ServiceBalance 
        // bool AvailabilityStatus 
        // double? RatingAverage 
        // string? Area 

        // int? CategoryId 

        //public Guid UserId

        // Portfolios 
        // ReceivedRequests 
        // Reviews 
        // Favorites 
        //public Guid UserId   
        //public string FullName
        //public string Email 
        //public string Phone 
        //public string City 
        //public string PasswordHash 
        //public string? ImgUrl 
        //public string Role 
        //public bool IsActive 

        // SentRequests 
        // Reviews 
        // ClientChatRooms 
        // WorkerChatRooms 
        // Messages 
        // Favorites 
        public void EditeWorkerCommandMapping()
        {
            CreateMap<EditeWorkerCommand, WorkerProfile>();
            CreateMap<EditeWorkerCommand, User>();
        }

    }
}
