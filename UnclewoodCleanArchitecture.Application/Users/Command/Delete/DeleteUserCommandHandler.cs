using UnclewoodCleanArchitecture.Application.Common.Interfaces;
using UnclewoodCleanArchitecture.Application.Common.Interfaces.Command;
using UnclewoodCleanArchitecture.Domain.Common;

namespace UnclewoodCleanArchitecture.Application.Users.Command.Delete;

public class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {

        await _userRepository.DeleteUserAsync(request.UserId);
        await _unitOfWork.CommitChangesAsync();
   
        return Result.Success();
    }
}