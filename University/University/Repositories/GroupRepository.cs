using Microsoft.EntityFrameworkCore;
using University.DbContexts;
using University.Models;
using University.Repositories.Interfaces;

namespace University.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public GroupRepository(ApplicationDbContext context)
        {
            _applicationDbContext = context;
        }

        public async Task<Group?> GetAsync(int id)
        {
            return await _applicationDbContext.Groups
                        .Where(u => (u.Id == id))
                        .SingleOrDefaultAsync();
        }

        public async Task<Group?> GetByNameAsync(string name)
        {
            return await _applicationDbContext.Groups
                        .Where(u => string
                        .Equals(u.GroupName, name))
                        .SingleOrDefaultAsync();
        }

        public async Task<Group?> GetAllAsync(int id)
        {
            return await _applicationDbContext.Groups
                        .Include(g => g.Teacher)
                        .Include(g => g.Curriculum)
                        .Include(g => g.Students).ThenInclude(s => s.Human)
                        .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<IList<Group>> ListByCurriculumIdAsync(int curriculumId)
        {
            return await _applicationDbContext.Curriculums
                        .Where(g => g.Id == curriculumId)
                        .SelectMany(g => g.Groups).ToListAsync();
        }

        public async Task<IList<Group>> ListAsync()
        {
            return await _applicationDbContext.Groups
                        .ToListAsync();
        }

        public async Task<int> AddAsync(Group group)
        {
            var transaction = await _applicationDbContext.Database.BeginTransactionAsync();

            try
            {
                var result = await _applicationDbContext.Groups.AddAsync(group);
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

        public async Task<int> UpdateAsync(Group group)
        {
            var transaction = await _applicationDbContext.Database.BeginTransactionAsync();

            try
            {
                _applicationDbContext.Entry(group).State = EntityState.Modified;
                await _applicationDbContext.SaveChangesAsync();
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

        public async Task<List<Group>> FilterByNameListAsync(string name)
        {
            return await _applicationDbContext.Groups
                        .Where(u => u.GroupName.StartsWith(name))
                        .ToListAsync();
        }
    }
}
