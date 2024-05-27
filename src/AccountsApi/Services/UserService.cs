using AccountsApi.Entities;
using AccountsApi.Models.User;
using AccountsApi.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AccountsApi.Services
{
    public class UserSerive : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserSerive(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            var users = await _userRepository.GetUsersAsync();
            //return _mapper.Map<IEnumerable<User>>(users);
            return users;
        }

        public async Task<User> GetUserAsync(int id)
        {
            var user = await _userRepository.GetUserAsync(id);

            if(user == null) throw new KeyNotFoundException($"User with id {id} not found");

            //return _mapper.Map<User>(user);
            return user;
        }

        public async Task AddUserAsync(CreateUserRequest request)
        {
            // validate 
            if(await _userRepository.GetUserByEmailAsync(request.Email) != null)
                throw new ApplicationException($"User with email {request.Email} already exists");

             //Map model to request model
             var user = _mapper.Map<User>(request);

             //Hash Password
             user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

             await _userRepository.AddUserAsync(user);
        }

        public async Task UpdateUserAsync(int id, UpdateUserRequest request)
        {
            var user = await _userRepository.GetUserAsync(id);

            if(user is null)
                throw new ApplicationException($"User with id {id} not found");

            // Validate
            var emailChanged = !String.IsNullOrEmpty(request.Email) && request.Email != user.Email;
            if(emailChanged && await _userRepository.GetUserByEmailAsync(request.Email) != null)
                throw new ApplicationException($"User with email {request.Email} already exists");

            // Hash password
            if(!String.IsNullOrEmpty(request.Password))
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            }

            // Map request to user
            _mapper.Map(request, user);

            await _userRepository.UpdateUserAsync(user);
        }

        public async Task DeleteUserAsync(int id)
        {
           await _userRepository.GetUserAsync(id);
        }
    }
}