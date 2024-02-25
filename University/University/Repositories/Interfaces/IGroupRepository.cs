using University.Models;

namespace University.Repositories.Interfaces
{
    public interface IGroupRepository
    {
        Task<Group?> GetGroupByIdAsync(int id);
        Task<IList<Group>> ListAsync();
        Task<IList<Group>> ListByCurriculumIdAsync(int curriculumId);
        Task<int> AddAsync(Group group);
        Task<int> UpdateAsync(Group group);
        Task<int> DeleteAsync(int id);
    }
}
