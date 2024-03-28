namespace University.Views
{
    public partial class StudentInfoView : Window
    {
        public StudentInfoView(ApplicationDbContext appDBContext, int studentId, int teacherId)
        {
            InitializeComponent();
            DataContext = new StudentReviewVM(appDBContext, studentId, teacherId);
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ShowInpuFields(object sender, RoutedEventArgs e)
        {
            if (inputFields.Visibility == Visibility.Hidden)
                inputFields.Visibility = Visibility.Visible;
        }

        private void HideInpuFields(object sender, RoutedEventArgs e)
        {
            inputFields.Visibility = Visibility.Hidden;
            txtFeedBack.Clear();
        }
    }
}
