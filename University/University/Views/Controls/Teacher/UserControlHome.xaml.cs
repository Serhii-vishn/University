using System.Windows;
using System.Windows.Controls;
using University.Models;
using University.ViewModels;

namespace University.Views.Controls.Teacher
{
    public partial class UserControlHome : UserControl
    {
        private readonly TeacherViewModel _viewModel;

        public UserControlHome(object dataContext)
        {
            InitializeComponent();

            _viewModel = dataContext as TeacherViewModel;
            if (_viewModel == null)
                throw new ArgumentException("DataContext is not of type TeacherViewModel");
        }
        private void TxtSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            searchTextBlock.Visibility = Visibility.Collapsed;
        }

        private void TxtSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                searchTextBlock.Visibility = Visibility.Visible;
            }
        }
    }
}
