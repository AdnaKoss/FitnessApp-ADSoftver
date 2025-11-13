using System;
using System.IO;
using System.Threading.Tasks;
using SQLite;
using FitnesAplikacija.Models;
//using Windows.System;

namespace FitnesAplikacija.Services
{
    public class DatabaseService
    {
        private static SQLiteAsyncConnection _database;

        public static async Task Init()
        {
            if (_database != null)
                return;

            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "FitnesAplikacija.db");
            _database = new SQLiteAsyncConnection(dbPath);

            await _database.CreateTableAsync<User>();
        }

        public static async Task<int> AddUser(User user)
        {
            await Init();
            return await _database.InsertAsync(user);
        }

        public static async Task<User> GetUserByEmailAsync(string email)
        {
            await Init();
            return await _database.Table<User>().FirstOrDefaultAsync(u => u.Email == email);
        }

        public static async Task<User> AuthenticateUserAsync(string email, string password)
        {
            await Init();
            return await _database.Table<User>().FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }

        public static async Task<User> GetUser(string username, string password)
        {
            await Init();
            return await _database.Table<User>()
                .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        }
    }
}
