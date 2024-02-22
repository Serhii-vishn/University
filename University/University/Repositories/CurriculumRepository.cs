using Microsoft.EntityFrameworkCore;
using University.DbContexts;
using University.Models;
using University.Repositories.Interfaces;

namespace University.Repositories
{
    public class CurriculumRepository : ICurriculumRepository
    {
        private readonly ApplicationDbContext _context;

        public CurriculumRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Curriculum?> GetCurriculumAsync(int id)
        {
            return await _context.Curriculums.Where(c => (c.Id == id)).SingleOrDefaultAsync();
        }

        public async Task<IList<Curriculum>> ListAsync()
        {
            return await _context.Curriculums.ToListAsync();
        }

        public async Task<int> AddAsync(Curriculum curriculum)
        {
            var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var result = await _context.Curriculums.AddAsync(curriculum);
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

        public async Task<int> UpdateAsync(Curriculum curriculum)
        {
            var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                _context.Entry(curriculum).State = EntityState.Modified;
                await _context.SaveChangesAsync();
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
            var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                _context.Entry((await GetCurriculumAsync(id))!).State = EntityState.Deleted;
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
