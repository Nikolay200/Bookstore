using Marten;
using Marten.Schema;
using Simple_Microservice_WebApp.Model;

namespace Simple_Microservice_WebApp.Data.Seed
{
    public class InitializeBookDataBase : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession(); // Получаем сессию
            if (!await session.Query<Book>().AnyAsync()) //Если никаких данных нет
            {
                session.Store<Book>(InitialData.Books);
                await session.SaveChangesAsync(cancellation);
            }
        }
    }
}
