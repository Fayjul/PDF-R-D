using MongoDB.Driver;

namespace PDF.Repository
{
    public class MongoRepository<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;

        public MongoRepository(IMongoDatabase database, string collectionName)
        {
            _collection = database.GetCollection<T>(collectionName);
        }

        public async Task<List<T>> GetAllAsync() => await _collection.Find(_ => true).ToListAsync();

        public async Task<T?> GetByIdAsync(string id) =>
            await _collection.Find(Builders<T>.Filter.Eq("_id", id)).FirstOrDefaultAsync();

        public async Task CreateAsync(T item) => await _collection.InsertOneAsync(item);

        public async Task UpdateAsync(string id, T item) =>
            await _collection.ReplaceOneAsync(Builders<T>.Filter.Eq("_id", id), item);

        public async Task DeleteAsync(string id) =>
            await _collection.DeleteOneAsync(Builders<T>.Filter.Eq("_id", id));
    }

}
