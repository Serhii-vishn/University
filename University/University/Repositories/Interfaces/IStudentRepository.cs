﻿namespace University.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        Task<Student?> GetAsync(int id);
        Task<Student?> GetAllAsync(int id);
        Task<Student?> GetAllByHumanAsync(int humanId);
        Task<IList<Student>> ListAsync();
        Task<IList<Student>> ListAllAsync();
        Task<int> AddAsync(Student student);
        Task<int> UpdateAsync(Student student);
        Task<int> DeleteAsync(int id);
        Task<IList<Student>> FilterByFullNameListAsync(string fullName);
    }
}
