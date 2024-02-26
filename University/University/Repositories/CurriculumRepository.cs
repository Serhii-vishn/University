using Microsoft.EntityFrameworkCore;
using University.DbContexts;
using University.Models;
using University.Repositories.Interfaces;

namespace University.Repositories
{
    public class CurriculumRepository : ICurriculumRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CurriculumRepository(ApplicationDbContext context)
        {
            _applicationDbContext = context;
        }

        public async Task<Curriculum?> GetCurriculumAsync(int id)
        {
            return await _applicationDbContext.Curriculums.Where(c => (c.Id == id)).SingleOrDefaultAsync();
        }

        public async Task<IList<Curriculum>> ListAsync()
        {
            return await _applicationDbContext.Curriculums.ToListAsync();
        }

        public async Task<int> AddAsync(Curriculum curriculum)
        {
            var transaction = await _applicationDbContext.Database.BeginTransactionAsync();

            try
            {
                var result = await _applicationDbContext.Curriculums.AddAsync(curriculum);
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

        public async Task<int> UpdateAsync(Curriculum curriculum)
        {
            var transaction = await _applicationDbContext.Database.BeginTransactionAsync();

            try
            {
                _applicationDbContext.Entry(curriculum).State = EntityState.Modified;
                await _applicationDbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return curriculum.Id;
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
                _applicationDbContext.Entry((await GetCurriculumAsync(id))!).State = EntityState.Deleted;
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
