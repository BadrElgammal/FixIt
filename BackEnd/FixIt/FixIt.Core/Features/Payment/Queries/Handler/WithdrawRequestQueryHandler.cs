using AutoMapper;
using FixIt.Core.Bases;
using FixIt.Core.Features.Favorites.Queries.DTOs;
using FixIt.Core.Features.Favorites.Queries.Models;
using FixIt.Core.Features.Payment.Queries.DTOs;
using FixIt.Core.Features.Payment.Queries.Models;
using FixIt.Core.Wrapper;
using FixIt.Domain.Entities;
using FixIt.Service.Abstracts;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Payment.Queries.Handler
{
    public class WithdrawRequestQueryHandler : ResponseHandler,
        IRequestHandler<GetAllWithdrawRequestsQuery, PaginatedResult<WithdrawRequestsQueryDTO>>,
        IRequestHandler<GetMyWalletDetailsQuery , Response<WalletQueryDTO>>,
        IRequestHandler<GetAllDepositQuery , PaginatedResult<GetAllDepositQueryDTO>>,
        IRequestHandler<GetAllTransactionsQuery , PaginatedResult<GetAllTransactionsQueryDTO>>
    {

        private readonly IMapper _mapper;
        private readonly IPaymobService _paymobService;

        public WithdrawRequestQueryHandler(IMapper mapper, IPaymobService paymobService)
        {
            _mapper = mapper;
            _paymobService = paymobService;
        }

        public async Task<PaginatedResult<WithdrawRequestsQueryDTO>> Handle(GetAllWithdrawRequestsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<WithdrawRequest, WithdrawRequestsQueryDTO>> expression = e => new WithdrawRequestsQueryDTO(e.Id, e.Amount, e.Method, e.Status, e.CreatedAt, e.PaidAt, e.WalletId, e.Wallet.UserId, e.Wallet.User.FullName, e.Wallet.User.ImgUrl, e.Wallet.User.Email);
            var query = _paymobService.GetAllWithDrawRequestsPaginated(request.status);
            var paginatedList = await query.Select(expression).ToPaginatedListAsync(request.pageNum, request.pageSize);
            return paginatedList;
        }

        public async Task<Response<WalletQueryDTO>> Handle(GetMyWalletDetailsQuery request, CancellationToken cancellationToken)
        {
            var Wallet = await _paymobService.GetWalletAsync(request.UserId);
            if (Wallet == null)
                return NotFound<WalletQueryDTO>("لايوجد لديك محفظه");
            var walletMapping = _mapper.Map<WalletQueryDTO>(Wallet);
            return Success(walletMapping);
        }

        public async Task<PaginatedResult<GetAllDepositQueryDTO>> Handle(GetAllDepositQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Domain.Entities.Payment, GetAllDepositQueryDTO>> expression = e => new GetAllDepositQueryDTO(e.PaymentId, e.Amount, e.Status, e.Gateway, e.GatewayRef, e.CreatedAt, e.ReleasedAt, e.WalletId, e.Wallet.UserId, e.Wallet.User.FullName, e.Wallet.User.ImgUrl, e.Wallet.User.Email);
            var query = _paymobService.GetAllDepositPaginated();
            var paginatedList = await query.Select(expression).ToPaginatedListAsync(request.PageNum , request.PageSize);
            return paginatedList;
        }

        public async Task<PaginatedResult<GetAllTransactionsQueryDTO>> Handle(GetAllTransactionsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Transaction, GetAllTransactionsQueryDTO>> expression = e => new GetAllTransactionsQueryDTO(e.TransactionId, e.Amount, e.ServiceCommetion, e.TransactionId.ToString(), e.RefType, e.CreatedAt, e.FromWalletId, e.FromWallet.UserId, e.FromWallet.User.FullName, e.FromWallet.User.ImgUrl, e.ToWalletId, e.ToWallet.UserId, e.ToWallet.User.FullName, e.ToWallet.User.ImgUrl, e.RequestId);
            var query = _paymobService.GetAllTransactionsPaginated();
            var paginatedList = await query.Select(expression).ToPaginatedListAsync(request.PageNum, request.PageSize);
            return paginatedList;
        }
    }
}
