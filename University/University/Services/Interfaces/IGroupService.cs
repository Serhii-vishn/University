using University.Models;

namespace University.Services.Interfaces
{
    public interface IGroupService
    {
        Task<Group?> GetGroupByIdAsync(int id);
        Task<Group?> GetAllGroupDataAsync(int id);
        Task<IList<Group>> ListAsync();
        Task<IList<Group>> ListByCurriculumIdAsync(int curriculumId);
        Task<int> AddAsync(Group group);
        Task<int> UpdateAsync(Group group);
        Task<int> DeleteAsync(int id);
        Task ExportGroupToFile(Group group, string filePath);
    }
}
