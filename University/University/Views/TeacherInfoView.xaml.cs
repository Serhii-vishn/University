namespace University.Views
{
    public partial class TeacherInfoView : Window
    {
        public TeacherInfoView(ITeacherService teacherService, int teacherId)
        {
            InitializeComponent();
            DataContext = new TeacherInfoVM(teacherService, teacherId);
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
