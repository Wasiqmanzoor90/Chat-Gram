using MongoDB.Driver;
using Server.Domain.Model;

namespace Server.Data
{
    public class MongoDbService
    {
        private readonly IMongoDatabase _database;

        public MongoDbService(IConfiguration configuration)
        {
            var ConnectionString = configuration["MongoDB:ConnectionString"];
            var databasename = configuration["MongoDB:Database"];
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase(databasename);
            _database = database;
        }

        public IMongoCollection<UserDetail> Users => _database.GetCollection<UserDetail>("Users");
        public IMongoCollection<PostDetails> Posts => _database.GetCollection<PostDetails>("Posts");
        public IMongoCollection<Comment> Comments => _database.GetCollection<Comment>("Commnets");
    }
    
}
