using FixIt.Core.Bases;
using FixIt.Core.Features.Users.Commands.Models;
using FixIt.Domain.Entities;
using FixIt.Service.Abstracts;
using MediatR;

namespace FixIt.Core.Features.Users.Commands.Handlers
{
    public class DeleteUserCommandHandler : ResponseHandler, IRequestHandler<DeleteUserCommand, Response<string>>
    {
        private readonly IService<User> _userService;

        public DeleteUserCommandHandler(IService<User> userService)
        {
            _userService = userService;
        }

        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            // Check if user exists
            var user = await _userService.GetByIdAsync(request.UserId);
            if (user == null)
                return NotFound<string>("المستخدم غير موجود");


            // Delete the user (cascade delete will handle related entities)
            await _userService.DeleteAsync(user);

            return Deleted<string>("تم حذف المستخدم بنجاح");
        }
    }
}