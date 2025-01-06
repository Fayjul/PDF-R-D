using MongoDB.Driver;
using System.Security.Cryptography;
using System.Text;
using PDF.Models;

public class UserRepository
{
    private readonly IMongoCollection<AppUser> _users;

    public UserRepository(IMongoDatabase database)
    {
        _users = database.GetCollection<AppUser>("AppUsers");
    }

    public async Task<AppUser?> GetUserByUsernameAsync(string username) =>
        await _users.Find(u => u.Name == username).FirstOrDefaultAsync();

    public async Task CreateUserAsync(AppUser user) => await _users.InsertOneAsync(user);

    public static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }
}