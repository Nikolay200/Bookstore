using Carter;
using FluentValidation;
using Marten;
using Simple_Microservice_WebApp.Behaviors;
using Simple_Microservice_WebApp.Data.Seed;

var builder = WebApplication.CreateBuilder(args);
// Add connection string

var connectionString = builder.Configuration.GetConnectionString("PostgresStringConnection")!;

builder.Services.AddMarten(options =>
{
    options.Connection(connectionString);
}).UseLightweightSessions().InitializeWith<InitializeBookDataBase>();

var assembly = typeof(Program).Assembly;

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(assembly);
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
