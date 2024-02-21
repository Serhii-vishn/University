using University.Models;

namespace University.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetAsync(int id);
        Task<IList<User>> ListAsync();
        Task<int> AddAsync(User user);
        Task<int> UpdateAsync(User user);
        Task<int> DeleteAsync(int id);
    }
}
