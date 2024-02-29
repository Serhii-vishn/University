using Microsoft.EntityFrameworkCore;
using University.DbContexts;
using University.Models;
using University.Repositories.Interfaces;

namespace University.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public TeacherRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Teacher?> GetAsync(int id)
        {
            return await _applicationDbContext.Teachers
                .Where(t => (t.Id == id))
                .SingleOrDefaultAsync();
        }

        public async Task<IList<Teacher>> ListAsync()
        {
            return await _applicationDbContext.Teachers
                .ToListAsync();
        }

        public async Task<IList<Teacher>> GetAllAsync()
        {
            return await _applicationDbContext.Teachers
                .Include(t => t.Human) 
                .Include(t => t.Groups)
                .Include(t => t.Curriculums)
                .ToListAsync();
        }

        public async Task<int> AddAsync(Teacher teacher)
        {
            var transaction = await _applicationDbContext.Database.BeginTransactionAsync();

            try
            {
                var result = await _applicationDbContext.Teachers.AddAsync(teacher);
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

        public async Task<int> UpdateAsync(Teacher teacher)
        {
            var transaction = await _applicationDbContext.Database.BeginTransactionAsync();

            try
            {
                _applicationDbContext.Entry(teacher).State = EntityState.Modified;
                await _applicationDbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return teacher.Id;
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
    }
}
