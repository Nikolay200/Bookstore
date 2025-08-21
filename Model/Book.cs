namespace Simple_Microservice_WebApp.Model
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string ImageUrl { get; set; } = default!;
        public decimal Price {  get; set; } = default!;
        public List<string> Category {  get; set; } = new ();

    }
}
