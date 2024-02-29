using University.Models;

namespace University.Repositories.Interfaces
{
    public interface ITeacherRepository
    {
        Task<Teacher?> GetAsync(int id);
        Task<IList<Teacher>> ListAsync();
        Task<IList<Teacher>> GetAllAsync();
        Task<int> AddAsync(Teacher teacher);
        Task<int> UpdateAsync(Teacher teacher);
        Task<int> DeleteAsync(int id);
    }
}
