using AutoMapper;

namespace FixIt.Core.Mapping.Workers
{
    public partial class WorkerProfileMapper : Profile
    {
        public WorkerProfileMapper()
        {
            GetWorkerByIdMapping();
            EditeWorkerCommandMapping();
            GetWorkerProfileByWorkerIdMapping();


        }



    }
}
