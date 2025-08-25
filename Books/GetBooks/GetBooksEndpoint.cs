using Carter;
using Mapster;
using MediatR;
using Simple_Microservice_WebApp.CQRS;
using Simple_Microservice_WebApp.Model;
// Библиотека Carter используется для настройки маршрута
namespace Simple_Microservice_WebApp.Books.GetBooks
{
    public record GetBooksResponse(IEnumerable<Book> Books);
    public record GetBooksRequest(int? PageNumber = 1, int? PageSize = 5);
    public class GetBooksEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            //http://localhost:15000/books
            app.MapGet("/books", async ([AsParameters] GetBooksRequest request, ISender sender) =>
            {
                GetBooksQuery query = request.Adapt<GetBooksQuery>();
                GetBooksResult result = await sender.Send(query);
                GetBooksResponse response = result.Adapt<GetBooksResponse>();
                return Results.Ok(response);
            });
        }
    }
}
