using AccountsApi.DataContext;
using AccountsApi.Entities;
using Dapper;

namespace AccountsApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            using var connection = _context.CreateConnection();
            var users = await connection.QueryAsync<User>("SELECT * FROM Users");
            return users;
        }

        public async Task<User> GetUserAsync(int id)
        {
            using var connection = _context.CreateConnection();
            //var sql = """SELECT * FROM Users WHERE Id = @id""";
            var user = await connection.QuerySingleOrDefaultAsync<User>("SELECT * FROM Users WHERE Id = @id", new { Id = id });
            return user;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            using var connection = _context.CreateConnection();
            var user = await connection.QuerySingleOrDefaultAsync<User>("SELECT * FROM Users WHERE Email = @email", new { Email = email });
            return user;
        }

        public async Task AddUserAsync(User user)
        {
            using var connection = _context.CreateConnection();
            var query = """
                INSERT INTO Users (Title, FirstName, LastName, Email, Role, PasswordHash)
                VALUES (@Title, @FirstName, @LastName, @Email, @Role, @PasswordHash)
            """;
            await connection.ExecuteAsync(query, user);
        }

        public async Task UpdateUserAsync(User user)
        {
            using var connection = _context.CreateConnection();
            var query = """
                UPDATE Users SET 
                    Title = @Title, 
                    FirstName = @FirstName, 
                    LastName = @LastName, 
                    Email = @Email, 
                    Role = @Role, 
                    PasswordHash = @PasswordHash 
                    WHERE Id = @user.Id
            """;
            await connection.ExecuteAsync(query, user);
        }
        
        public async Task DeleteUserAsync(int id)
        {
            using var connection = _context.CreateConnection();
            var query = """
                DELETE FROM Users WHERE Id = @id
            """;
            await connection.ExecuteAsync(query, new { Id = id });
        }
    }
}