using University.Models;

namespace University.Repositories.Interfaces
{
    public interface ITeacherRepository
    {
        Task<Teacher?> GetAsync(int id);
        Task<IList<Teacher>> ListAsync();
        Task<int> AddAsync(Teacher teacher);
        Task<int> UpdateAsync(Teacher teacher);
        Task<int> DeleteAsync(int id);
    }
}
