using FixIt.Core.Bases;
using FixIt.Core.Features.Workers.Queries.DTOs;
using FixIt.Core.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Workers.Queries.Models
{
    public class GetWorkersPaginatedListQuery : IRequest<PaginatedResult<GetWorkersPaginatedResponce>>
    {
        public int pageNum { get; set; }
        public int pageSize { get; set; }
        public string? search {  get; set; }
        public string? address { get; set; }
        public bool? IsAvilable { get; set; }
        //public GetWorkersPaginatedListQuery(int pageNum, int pageSize, string? search, string? address, string? isAvilable)
        //{
        //    this.pageNum = pageNum;
        //    this.pageSize = pageSize;
        //    this.search = search;
        //    this.address = address;
        //    IsAvilable = isAvilable;
        //}
    }
}
