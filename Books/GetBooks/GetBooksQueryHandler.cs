using Marten;
using Marten.Pagination;
using Simple_Microservice_WebApp.CQRS;
using Simple_Microservice_WebApp.Model;

namespace Simple_Microservice_WebApp.Books.GetBooks
{
    public record GetBooksQuery(int? PageNumber = 1, int? PageSize = 5) : IQuery<GetBooksResult>;
    public record GetBooksResult(IEnumerable<Book> Books);
    public class GetBooksQueryHandler(IDocumentSession session) : IQueryHandler<GetBooksQuery, GetBooksResult>
    {
        public async Task<GetBooksResult> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            var books = await session.Query<Book>().ToPagedListAsync(request.PageNumber??1, request.PageSize??5, cancellationToken);
            return new GetBooksResult(books);
        }
    }
}
