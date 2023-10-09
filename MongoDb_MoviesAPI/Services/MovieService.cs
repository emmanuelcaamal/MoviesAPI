using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDb_MoviesAPI.Data;
using MongoDb_MoviesAPI.Models;

namespace MongoDb_MoviesAPI.Services
{
    public class MovieService
    {
        private readonly IMongoCollection<Movie> _movies;

        public MovieService(IOptions<MongoDbSetting> mongoSettings)
        {
            var mongoClient = new MongoClient(mongoSettings.Value.ConnectionString);
            _movies = mongoClient.GetDatabase(mongoSettings.Value.DatabaseName)
                .GetCollection<Movie>(mongoSettings.Value.CollectionName);
        }

        public async Task<List<Movie>> Get() =>
            await _movies.Find(_ => true).ToListAsync();

        public async Task<Movie> Get(string id) =>
            await _movies.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task Create(Movie newMovie) =>
            await _movies.InsertOneAsync(newMovie);

        public async Task Update(string id, Movie updateMovie) =>
            await _movies.ReplaceOneAsync(x => x.Id == id, updateMovie);

        public async Task Remove(string id) =>
            await _movies.DeleteOneAsync(x => x.Id == id);
    }
}
