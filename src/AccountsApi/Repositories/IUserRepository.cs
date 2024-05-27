using AccountsApi.Entities;

namespace AccountsApi.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(int id);
        Task<User> GetUserByEmailAsync(string email);
        Task<IEnumerable<User>> GetUsersAsync();
        Task AddUserAsync(User entity);
        Task UpdateUserAsync(User entity);
        Task DeleteUserAsync(int id);
    }
}
