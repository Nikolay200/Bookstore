using MediatR;

namespace Simple_Microservice_WebApp.CQRS
{
    public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
        where TResponse:notnull
    {
    }
}
