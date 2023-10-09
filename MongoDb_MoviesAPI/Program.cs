using MongoDb_MoviesAPI.Data;
using MongoDb_MoviesAPI.Models;
using MongoDb_MoviesAPI.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<MongoDbSetting>(builder.Configuration.GetSection("MongoDbSetting"));
builder.Services.AddSingleton<MovieService>();
var app = builder.Build();

app.MapGet("/", () => "Mongo Movies API");

//RouteAPI
app.MapPost("/api/movies", async (MovieService movieService, Movie movie) =>
{
    await movieService.Create(movie);
    return Results.Ok();
});

app.MapGet("/api/movies", async (MovieService movieService) =>
{
    var movies = await movieService.Get();
    return Results.Ok(movies);
});

app.Run();
