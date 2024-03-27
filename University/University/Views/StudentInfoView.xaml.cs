using System;

namespace University.Views
{
    public partial class StudentInfoView : Window
    {
        public StudentInfoView(ApplicationDbContext appDBContext, int studentId)
        {
            

            InitializeComponent();
            inputFields.Visibility = Visibility.Collapsed;
            DataContext = new StudentInfoVM(appDBContext, studentId);
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (inputFields.Visibility == Visibility.Collapsed)
            {
                inputFields.Visibility = Visibility.Visible;
            }
            else
            {
                inputFields.Visibility = Visibility.Collapsed;
            }
        }
    }
}
