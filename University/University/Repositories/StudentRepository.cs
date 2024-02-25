using Microsoft.EntityFrameworkCore;
using University.DbContexts;
using University.Models;
using University.Repositories.Interfaces;

namespace University.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Student?> GetAsync(int id)
        {
            return await _context.Students.Where(c => (c.Id == id)).SingleOrDefaultAsync();
        }

        public async Task<IList<Student>> ListAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<int> AddAsync(Student student)
        {
            var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var result = await _context.Students.AddAsync(student);
                await _context.SaveChangesAsync();
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
            var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                _context.Entry(student).State = EntityState.Modified;
                await _context.SaveChangesAsync();
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
            var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                _context.Entry((await GetAsync(id))!).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return id;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
