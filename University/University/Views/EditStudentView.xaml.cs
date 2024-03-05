﻿using System.Windows;
using University.ViewModels;

namespace University.Views
{
    public partial class EditStudentView : Window
    {
        public EditStudentView(int studentId)
        {
            InitializeComponent();
            DataContext = new EditStudentVM(studentId);
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
