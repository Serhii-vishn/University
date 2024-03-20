namespace University.Views
{
    public partial class AddEditGroupView : Window
    {
        public AddEditGroupView()
        {
            InitializeComponent();
            DataContext = new AddGroupVM();
        }

        public AddEditGroupView(ApplicationDbContext appDBContext, int groupId)
        {
            InitializeComponent();
            DataContext = new EditGroupVM(appDBContext, groupId);
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
