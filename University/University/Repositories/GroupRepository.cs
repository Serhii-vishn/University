using Microsoft.EntityFrameworkCore;
using University.DbContexts;
using University.Models;
using University.Repositories.Interfaces;

namespace University.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly ApplicationDbContext _context;

        public GroupRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Group?> GetGroupByIdAsync(int id)
        {
            return await _context.Groups.Where(u => (u.Id == id)).SingleOrDefaultAsync();
        }

        public async Task<IList<Group>> ListAsync()
        {
            return await _context.Groups.ToListAsync();
        }

        public async Task<int> AddAsync(Group group)
        {
            var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var result = await _context.Groups.AddAsync(group);
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

        public async Task<int> UpdateAsync(Group group)
        {
            var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                _context.Entry(group).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return group.Id;
            }
            catch 
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<Group>> ListByCurriculumIdAsync(int curriculumId)
        {
            var result = await _context.Curriculums
                .Where(g => g.Id == curriculumId)
                .SelectMany(g => g.Groups).ToListAsync();

            return result;
        }
    }
}
