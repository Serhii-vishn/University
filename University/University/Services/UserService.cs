using System.Security.Cryptography;
using System.Text;
using University.Exceptions;
using University.Models;
using University.Repositories.Interfaces;
using University.Services.Interfaces;

namespace University.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            if(id <= 0)
                throw new ArgumentException("Invalid user id");

            var user = await _userRepository.GetAsync(id);

            if (user is null)
                throw new NotFoundException($"User with id = {id} does not exist");

            return user;
        }

        public async Task<User?> GetUserByLogPassAsync(string login, string password)
        {
            var passwordHash = GetHashPassword(password);
            var user = await _userRepository.GetByLogPassAsync(login, passwordHash);

            if (user is null)
                throw new NotFoundException("User with this login or password does not exist");

            return user;
        }

        public async Task<IList<User>> ListAsync()
        {
            return await _userRepository.ListAsync();
        }

        public async Task<int> AddAsync(User user)
        {
            ValidateUser(user);
            return await _userRepository.AddAsync(user);
        }

        public async Task<int> UpdateAsync(User user)
        {
            ValidateUser(user);
            await GetUserByIdAsync(user.Id);
            return await _userRepository.UpdateAsync(user);
        }

        public async Task<int> DeleteAsync(int id)
        {
            await GetUserByIdAsync(id);
            return await _userRepository.DeleteAsync(id);
        }

        private static void ValidateUser(User user)
        {
            if (user is null)
                throw new ArgumentNullException(nameof(user), "User is empty");

            ValidateUserLogin(user.Login);
            ValidateUserRole(user.Role);
        }

        private static void ValidateUserLogin(string login)
        {
            if (string.IsNullOrWhiteSpace(login))
            {
                throw new ArgumentNullException(nameof(login), "User login is empty");
            }
            else
            {
                login = login.Trim();

                if (login.Length > 50)
                    throw new ArgumentException(nameof(login), "User login must be maximum of 50 characters");

                LanguageValidator.ValidateWordEnUa(login);
            }
        }

        private static void ValidateUserRole(string role)
        {
            if(string.IsNullOrWhiteSpace(role))
            {
                throw new ArgumentNullException(nameof(role), "Role is empty");
            }
            else
            {
                if(!string.Equals(role.ToLower(), "teacher") && !string.Equals(role.ToLower(), "student"))
                    throw new ArgumentException(nameof(role), "Role should be teacher or student only");
            }
        }

        private string GetHashPassword(string password)
        {
            var md5 = MD5.Create();

            var hashPass = md5.ComputeHash
                (
                    Encoding.UTF8.GetBytes(password)
                );

            return Convert.ToBase64String(hashPass);
        }
    }
}
