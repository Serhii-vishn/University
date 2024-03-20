namespace University.Views.Controls.Teacher
{
    public partial class CurriculumsOuputCL : UserControl
    {
        public CurriculumsOuputCL()
        {
            InitializeComponent();
            DataContext = new CurriculumsOuputVM(); 
        }
    }
}
