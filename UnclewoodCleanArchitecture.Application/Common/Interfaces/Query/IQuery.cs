using MediatR;
using UnclewoodCleanArchitecture.Domain.Common;

namespace UnclewoodCleanArchitecture.Application.Common.Interfaces.Query;

public interface IQuery<TResponse>:IRequest<Result<TResponse>>
{
}