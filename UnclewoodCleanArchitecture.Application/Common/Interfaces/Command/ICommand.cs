using MediatR;
using UnclewoodCleanArchitecture.Domain.Common;

namespace UnclewoodCleanArchitecture.Application.Common.Interfaces.Command;

public interface ICommand : IRequest<Result> , IBaseCommand;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>,IBaseCommand;

public interface IBaseCommand;