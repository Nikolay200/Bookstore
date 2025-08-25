using Carter;
using Marten;
using Simple_Microservice_WebApp.Data.Seed;

var builder = WebApplication.CreateBuilder(args);
// Add connection string

var connectionString = builder.Configuration.GetConnectionString("PostgresStringConnection")!;

builder.Services.AddMarten(options =>
{
    options.Connection(connectionString);
}).UseLightweightSessions().InitializeWith<InitializeBookDataBase>();


builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddCarter();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapCarter();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
