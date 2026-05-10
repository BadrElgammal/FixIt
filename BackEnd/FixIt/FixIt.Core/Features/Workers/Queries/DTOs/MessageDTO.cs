namespace FixIt.Core.Features.Workers.Queries.DTOs
{
    public class MessageDTO
    {
        public string? LastMessage { get; set; }
        public DateTime? LastMessageAt { get; set; }

        //Sender NAme & img
        public string SenderName { get; set; }
        public string? SenderImgUrl { get; set; }


        //Reciver NAme & img
        public string TargetUserName { get; set; }
        public string? TargetUserImgUrl { get; set; }



    }
}
