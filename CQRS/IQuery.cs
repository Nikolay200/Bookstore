using MediatR;

namespace Simple_Microservice_WebApp.CQRS
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
        where TResponse : notnull
    {
    }
}
