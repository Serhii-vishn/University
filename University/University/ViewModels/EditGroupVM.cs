using University.Models;

namespace University.ViewModels
{
    public class EditGroupVM
         : ViewModelBase
    {
        private readonly Group _baseGeroup;
        public EditGroupVM(Group group) 
        {
            _baseGeroup = group;
        }



    }
}
