using Microsoft.EntityFrameworkCore;
using University.DbContexts;
using University.Models;
using University.Repositories.Interfaces;

namespace University.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public StudentRepository(ApplicationDbContext context)
        {
            _applicationDbContext = context;
        }

        public async Task<Student?> GetAsync(int id)
        {
            return await _applicationDbContext.Students.Where(c => (c.Id == id)).SingleOrDefaultAsync();
        }

        public async Task<Student?> GetAllAsync(int id)
        {
            return await _applicationDbContext.Students
                        .Include(s => s.Human)
                        .Include(s => s.Group)
                        .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IList<Student>> ListAsync()
        {
            return await _applicationDbContext.Students.ToListAsync();
        }

        public async Task<IList<Student>> ListAllAsync()
        {
            return await _applicationDbContext.Students
                .Include(s => s.Human)
                .Include(s => s.Group)
                .ToListAsync();
        }

        public async Task<int> AddAsync(Student student)
        {
            var transaction = await _applicationDbContext.Database.BeginTransactionAsync();

            try
            {
                var result = await _applicationDbContext.Students.AddAsync(student);
                await _applicationDbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return result.Entity.Id;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<int> UpdateAsync(Student student)
        {
            var transaction = await _applicationDbContext.Database.BeginTransactionAsync();

            try
            {
                _applicationDbContext.Entry(student).State = EntityState.Modified;
                await _applicationDbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return student.Id;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            var transaction = await _applicationDbContext.Database.BeginTransactionAsync();

            try
            {
                _applicationDbContext.Entry((await GetAsync(id))!).State = EntityState.Deleted;
                await _applicationDbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return id;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<IList<Student>> FilterByFullNameListAsync(string fullName)
        {
            return await _applicationDbContext.Students
                       .Include(s => s.Human)
                       .Where(s => s.Human.FirstName.StartsWith(fullName) || s.Human.LastName.StartsWith(fullName))
                       .ToListAsync();
        }
    }
}
