
using FluentValidation;
using Marten;
using Simple_Microservice_WebApp.CQRS;
using Simple_Microservice_WebApp.Model;

namespace Simple_Microservice_WebApp.Books.AppendBook
{
    public record AppendBookCommand(
        string Title,
        string Name,
        string Description,
        string ImageUrl,
        decimal Price,
        List<string> Category
        ) : ICommand<AppendBookResult>;


    public class AppendBookCommandValidator : AbstractValidator<AppendBookCommand>
    {
        public AppendBookCommandValidator()
        {
            RuleFor( x => x.Title ).NotEmpty().WithMessage("Title не может быть пустым!");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price не может быть отридцательным!");
        }
    }
    public record AppendBookResult(Guid Id);
    public class AppendBookCommandHandler(IDocumentSession session) : ICommandHandler<AppendBookCommand, AppendBookResult>
    {
        public async Task<AppendBookResult> Handle(AppendBookCommand command, CancellationToken cancellationToken)
        {
            var book = new Book
            {
                Title = command.Title,
                Name = command.Name,
                Description = command.Description,
                ImageUrl = command.ImageUrl,
                Price = command.Price,
                Category = command.Category
            };

            session.Store( book );
            await session.SaveChangesAsync(cancellationToken);
            return new AppendBookResult(book.Id);
        }
    }
}
