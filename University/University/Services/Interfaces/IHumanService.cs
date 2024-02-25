using University.Models;

namespace University.Services.Interfaces
{
    public interface IHumanService
    {
        Task<Human?> GetUserByIdAsync(int id);
        Task<IList<Human>> ListAsync();
        Task<int> AddAsync(Human human);
        Task<int> UpdateAsync(Human human);
        Task<int> DeleteAsync(int id);
    }
}
