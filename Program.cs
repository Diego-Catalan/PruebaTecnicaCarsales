using RickAndMortyBackend.Interfaces;
using RickAndMortyBackend.Middleware;
using RickAndMortyBackend.Repositories;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IEpisodeRepository, EpisodeRepository>();// Inyección de dependencias
builder.Services.AddControllers();
builder.Services.AddHttpClient("RickAndMortyClient", client =>
{
    client.BaseAddress = new Uri("https://rickandmortyapi.com/api/");// URL base de la API externa
});

builder.Services.AddCors(options =>// Configurar CORS
{
    options.AddPolicy("AllowAngular",
        policy => policy.WithOrigins("http://localhost:4200")
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}





app.UseMiddleware<Middleware>();

app.UseHttpsRedirection();

app.UseCors("AllowAngular"); // Activar CORS

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
