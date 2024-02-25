using System.Windows;
using System.Windows.Controls;
using University.ViewModels;

namespace University.Views.Controls.Teacher
{
    public partial class CurriculumsOuputCL : UserControl
    {
        public CurriculumsOuputCL()
        {
            InitializeComponent();
            DataContext = new CurriculumsOuputVM(); 
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
