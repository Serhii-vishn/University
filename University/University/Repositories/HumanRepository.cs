using Microsoft.EntityFrameworkCore;
using University.DbContexts;
using University.Models;
using University.Repositories.Interfaces;

namespace University.Repositories
{
    public class HumanRepository : IHumanRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public HumanRepository(ApplicationDbContext applicationDbContext) 
        {
            _applicationDbContext = applicationDbContext;
        }

        public Task<int> AddAsync(Human human)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Human?> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<Human>> ListAsync()
        {
            return await _applicationDbContext.Humans.ToListAsync();
        }

        public Task<int> UpdateAsync(Human human)
        {
            throw new NotImplementedException();
        }
    }
}
