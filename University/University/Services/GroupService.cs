using University.Exceptions;
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

        public async Task<Group?> GetGroupByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid group id");

            var group = await _grouprepository.GetAsync(id);

            if (group is null)
                throw new NotFoundException($"Group with id = {id} does not exist");

            return group;
        }

        public async Task<Group?> GetAllGroupDataAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid group id");

            var group = await _grouprepository.GetAllAsync(id);

            if (group is null)
                throw new NotFoundException($"Group with id = {id} does not exist");

            return group;
        }

        public async Task<IList<Group>> ListByCurriculumIdAsync(int curriculumId)
        {
            if (curriculumId <= 0)
                throw new ArgumentException("Invalid curriculum id");

            return await _grouprepository.ListByCurriculumIdAsync(curriculumId);
        }

        public async Task<IList<Group>> ListAsync()
        {
            return await _grouprepository.ListAsync();
        }

        public async Task<int> AddAsync(Group group)
        {
            await ValidateGroup(group);
            var isExist = await _grouprepository.GetByNameAsync(group.GroupName);
            if (!(isExist is null))
                throw new ArgumentException("Group with this name already exists");

            return await _grouprepository.AddAsync(group);
        }

        public async Task<int> UpdateAsync(Group group)
        {
            await ValidateGroup(group);
            return await _grouprepository.UpdateAsync(group);
        }

        public async Task<int> DeleteAsync(int id)
        {
            var group = await _grouprepository.GetAllAsync(id);
            var students = group.Students.Count();
            if (students == 0)
                return await _grouprepository.DeleteAsync(id);

            throw new ArgumentException("Can`t delete a group in which students are connected");
        }

        private async Task ValidateGroup(Group group)
        {
            if (group is null)
                throw new ArgumentNullException(nameof(group), "Group is empty");

            ValidateGroupName(group.GroupName);
        }

        private static void ValidateGroupName(string groupName)
        {
            if (string.IsNullOrWhiteSpace(groupName))
            {
                throw new ArgumentNullException(nameof(groupName), "Group name is empty");
            }
            else 
            {
                groupName = groupName.Trim();

                if (groupName.Length > 10)
                    throw new ArgumentException(nameof(groupName), "Group name must be maximum of 10 characters");
            }
        }
    }
}
