using AccountsApi.Entities;
using AccountsApi.Models.User;

namespace AccountsApi.Services
{
    public interface IUserService
    {
        Task<User> GetUserAsync(int id);
        Task<IEnumerable<User>> GetUsersAsync();
        Task AddUserAsync(CreateUserRequest entity);
        Task UpdateUserAsync(int id, UpdateUserRequest entity);
        Task DeleteUserAsync(int id);
    }
}