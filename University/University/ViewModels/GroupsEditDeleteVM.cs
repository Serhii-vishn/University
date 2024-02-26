using University.DbContexts;
using University.Models;
using University.Repositories;
using University.Services;
using University.Services.Interfaces;

namespace University.ViewModels
{
    public class GroupsEditDeleteVM : ViewModelBase
    {
        private readonly IGroupService _groupService;

        private List<Group> _groups;

        public List<Group> Groups
        {
            get { return _groups; }
            set
            {
                _groups = value;
                OnPropertyChanged(nameof(Groups));
            }
        }

        public GroupsEditDeleteVM()
        {
            var appDBContext = new ApplicationDbContext();

            _groupService = new GroupService(new GroupRepository(appDBContext));
        }

    }
}
