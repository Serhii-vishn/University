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
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task<User?> GetUserByLogPassAsync(string login, string password)
        {
            return await _userRepository.GetUserByLogPassAsync(login, password);
        }

        public Task<IList<User>> ListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> AddAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
