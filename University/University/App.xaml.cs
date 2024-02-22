using System.Windows;
using University.DbContexts;

namespace University
{

    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ApplicationDbContext applicationDbContext = new();
            DBInitializer.Initialize(applicationDbContext);
        }
    }
}
