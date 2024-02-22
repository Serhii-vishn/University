using University.Models;

namespace University.Repositories.Interfaces
{
    public interface IGroupRepository
    {
        Task<Group?> GetGroupByIdAsync(int id);
        Task<IList<Group>> ListAsync();
        Task<int> AddAsync(Group group);
        Task<int> UpdateAsync(Group group);
        Task<int> DeleteAsync(int id);
    }
}
