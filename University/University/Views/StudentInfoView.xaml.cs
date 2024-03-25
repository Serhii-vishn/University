using System;

namespace University.Views
{
    public partial class StudentInfoView : Window
    {
        public StudentInfoView(ApplicationDbContext appDBContext, int studentId)
        {
            InitializeComponent();
            DataContext = new StudentInfoVM(appDBContext, studentId);
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
