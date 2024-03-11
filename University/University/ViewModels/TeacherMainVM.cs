using System.Windows;
using University.DbContexts;
using University.Models;
using University.Repositories;
using University.Services;
using University.Services.Interfaces;

namespace University.ViewModels
{
    public class TeacherMainVM :
        ViewModelBase
    {
        private ITeacherService _teacherService;
        private IHumanService _humanService;
        private Teacher? _teacher;

        public Teacher Teacher
        {
            get { return _teacher; }
            set
            {
                _teacher = value;
                OnPropertyChanged(nameof(Teacher));
            }
        }

        public TeacherMainVM(int userId)
        {
            var appDBContext = new ApplicationDbContext();

            _teacherService = new TeacherService(new TeacherRepository(appDBContext));
            _humanService = new HumanService(new HumanRepository(appDBContext));

            LoadGroupDataAsync(userId);
        }

        private async void LoadGroupDataAsync(int userId)
        {
            try
            {
                var human = await _humanService.GetHumanByIdAsync(userId);
                Teacher = await _teacherService.GetAllTeacherDataByHumanAsync(human.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
