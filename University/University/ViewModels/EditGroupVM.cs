using Prism.Commands;
using System.Collections.ObjectModel;
using University.DbContexts;
using University.Models;
using University.Repositories;
using University.Services;
using University.Services.Interfaces;

namespace University.ViewModels
{
    public class EditGroupVM
         : ViewModelBase
    {
        private Group _group;

        private string? _groupName;
        private Curriculum? _curriculum;
        private string? _curator;
        private ObservableCollection<Student> _students;

        private readonly IGroupService _groupService;

        private DelegateCommand _saveChangesCommand;

        public DelegateCommand SaveChangesCommand =>
            _saveChangesCommand ?? (_saveChangesCommand = new DelegateCommand(ExecuteSaveChangesCommand));

        public string GroupName
        {
            get{return _groupName; }
            set
            {
                _groupName = value;
                OnPropertyChanged(nameof(GroupName));
            }
        }
        public Curriculum Curriculum
        {
            get { return _curriculum; }
            set
            {
                _curriculum = value;
                OnPropertyChanged(nameof(Curriculum));
            }
        }
        public string Curator
        {
            get { return _curator; }
            set
            {
                _curator = value;
                OnPropertyChanged(nameof(Curator));
            }
        }
        public ObservableCollection<Student> SelectedStudents
        {
            get { return _students; }
            set
            {
                _students = value;
                OnPropertyChanged(nameof(SelectedStudents));
            }
        }

        public EditGroupVM(int groupId) 
        {
            var dbContext = new ApplicationDbContext();

            _groupService = new GroupService(new GroupRepository(dbContext));
            
            LoadDataAsync(groupId);
        }

        private async void LoadDataAsync(int groupId) 
        {
            _group = await _groupService.GetAllGroupDataAsync(groupId);

            GroupName = _group.GroupName;//fix output
            Curriculum = _group.Curriculum;
           //Curator = _group.Teacher.Human.FirstName;// + _group.Teacher.Human.LastName;    
        }

        private void ExecuteSaveChangesCommand()
        {

        }
    }
}
