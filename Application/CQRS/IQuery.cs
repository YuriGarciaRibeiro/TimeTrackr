using FluentResults;
using MediatR;

namespace Application.CQRS;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
