namespace University.Repositories.Interfaces
{
    public interface ITeacherRepository
    {
        Task<Teacher?> GetAsync(int id);
        Task<Teacher?> GetAllAsync(int id);
        Task<Teacher?> GetAllByHumanAsync(int humanId);
        Task<IList<Teacher>> ListAsync();
        Task<IList<Teacher>> ListAllAsync();
        Task<int> AddAsync(Teacher teacher);
        Task<int> UpdateAsync(Teacher teacher);
        Task<int> DeleteAsync(int id);
    }
}
