namespace University.Views
{
    public partial class AddEditStudentView : Window
    {
        public AddEditStudentView()
        {
            InitializeComponent();
            DataContext = new AddStudentVM();
        }

        public AddEditStudentView(ApplicationDbContext appDBContext, int studentId)
        {
            InitializeComponent();
            DataContext = new EditStudentVM(appDBContext, studentId);
        }

    private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
