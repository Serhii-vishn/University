using University.Models;

namespace University.Repositories.Interfaces
{
    public interface IHumanRepository
    {
        Task<Human?> GetByIdAsync(int id);
        Task<IList<Human>> ListAsync();
        Task<int> AddAsync(Human human);
        Task<int> UpdateAsync(Human human);
        Task<int> DeleteAsync(int id);
    }
}
