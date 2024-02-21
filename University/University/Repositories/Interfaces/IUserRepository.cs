using University.Models;

namespace University.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> GetUserByLogPassAsync(string login, string password);
        Task<IList<User>> ListAsync();
        Task<int> AddAsync(User user);
        Task<int> UpdateAsync(User user);
        Task<int> DeleteAsync(int id);
    }
}
