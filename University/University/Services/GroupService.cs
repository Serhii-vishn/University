using University.Models;
using University.Repositories.Interfaces;
using University.Services.Interfaces;

namespace University.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _grouprpository;

        public GroupService(IGroupRepository grouprpository)
        {
            _grouprpository = grouprpository;
        }

        public async Task<IList<Group>> ListByCurriculumIdAsync(int curriculumId)
        {
            if (curriculumId <= 0)
                throw new ArgumentException("Invalid user id");

            return await _grouprpository.ListByCurriculumIdAsync(curriculumId);
        }

        public Task<int> AddAsync(Group group)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Group?> GetGroupByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<Group>> ListAsync()
        {
            return await _grouprpository.ListAsync();
        }

        public Task<int> UpdateAsync(Group group)
        {
            throw new NotImplementedException();
        }
    }
}
