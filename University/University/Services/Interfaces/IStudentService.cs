namespace University.Services.Interfaces
{
    public interface IStudentService
    {
        Task<Student?> GetStudentByIdAsync(int id);
        Task<Student?> GetAllStudentDataAsync(int id);
        Task<IList<Student>> ListAsync();
        Task<IList<Student>> GetAllStudentsDataAsync();
        Task<IList<Student>> GetAllFreeStudentsDataAsync();
        Task<int> AddAsync(Student student);
        Task<IList<Student>> AddFromFileAsync(string filePath);
        Task<int> UpdateAsync(Student student);
        Task<int> DeleteAsync(int id);
        Task<IList<Student>> FilterByFullNameListAsync(string fullName);
    }
}
