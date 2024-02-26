using University.Models;
using University.Repositories.Interfaces;
using University.Services.Interfaces;

namespace University.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _grouprepository;

        public GroupService(IGroupRepository grouprepository)
        {
            _grouprepository = grouprepository;
        }

        public async Task<IList<Group>> ListByCurriculumIdAsync(int curriculumId)
        {
            if (curriculumId <= 0)
                throw new ArgumentException("Invalid user id");

            return await _grouprepository.ListByCurriculumIdAsync(curriculumId);
        }

        public async Task<IList<Group>> ListAsync()
        {
            return await _grouprepository.ListAsync();
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



        public Task<int> UpdateAsync(Group group)
        {
            throw new NotImplementedException();
        }
    }
}
