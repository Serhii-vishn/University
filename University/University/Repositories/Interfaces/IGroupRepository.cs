using University.Models;

namespace University.Repositories.Interfaces
{
    public interface IGroupRepository
    {
        Task<Group?> GetAsync(int id);
        Task<IList<Group>> ListByCurriculumIdAsync(int curriculumId);
        Task<IList<Group>> ListAsync();
        Task<int> AddAsync(Group group);
        Task<int> UpdateAsync(Group group);
        Task<int> DeleteAsync(int id);
    }
}
