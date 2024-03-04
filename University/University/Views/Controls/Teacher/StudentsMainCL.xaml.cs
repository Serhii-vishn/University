using System.Windows.Controls;
using University.ViewModels;

namespace University.Views.Controls.Teacher
{
    public partial class StudentsMainCL : UserControl
    {
        public StudentsMainCL()
        {
            InitializeComponent();
            DataContext = new StudentsMainVM();
        }
    }
}
