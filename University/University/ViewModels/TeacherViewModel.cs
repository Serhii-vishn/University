using System.Collections.ObjectModel;
using System.Windows;
using University.DbContexts;
using University.Models;
using University.Repositories;
using University.Services;
using University.Services.Interfaces;

namespace University.ViewModels
{
    public class TeacherViewModel : ViewModelBase
    {
        private readonly ICurriculumService _curriculumService;
        private readonly IGroupService _groupService;

        private List<Curriculum> _curriculums;
        public List<Curriculum> Curriculums
        {
            get { return _curriculums; }
            set
            {
                _curriculums = value;
                OnPropertyChanged(nameof(Curriculums));
            }
        }

        public TeacherViewModel()
        {
            var appDBContext = new ApplicationDbContext();

            _curriculumService = new CurriculumService(new CurriculumRepository(appDBContext));
            _groupService = new GroupService(new GroupRepository(appDBContext));

            LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                var result = await _curriculumService.ListAsync();
                Curriculums = new List<Curriculum>(result);
            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.Message);
            }
        }
    }
}
