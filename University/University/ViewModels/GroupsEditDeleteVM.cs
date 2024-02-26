using Prism.Commands;
using System.Collections.ObjectModel;
using System.Windows;
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

        private ObservableCollection<Group> _groups;

        private DelegateCommand<Group> _deleteCommand;

        public ObservableCollection<Group> Groups
        {
            get { return _groups; }
            set
            {
                _groups = value;
                OnPropertyChanged(nameof(Groups));
            }
        }

        public DelegateCommand<Group> DeleteCommand  => 
            _deleteCommand ??(_deleteCommand = new DelegateCommand<Group>(ExecuteDeleteCommand));

        private async void ExecuteDeleteCommand(Group group)
        {
            try
            {
                await _groupService.DeleteAsync(group.Id);
                OnPropertyChanged(nameof(Groups));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public GroupsEditDeleteVM()
        {
            var appDBContext = new ApplicationDbContext();

            _groupService = new GroupService(new GroupRepository(appDBContext));

            LoadDataAsync();
        }

        private async void LoadDataAsync()
        {
            try
            {
                var groups = await _groupService.ListAsync();
                Groups = new ObservableCollection<Group>(groups);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
