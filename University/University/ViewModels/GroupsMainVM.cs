namespace University.ViewModels
{
    public class GroupsMainVM : ViewModelBase
    {
        private Window? taskWindow = null;
        private readonly IGroupService _groupService;
        private readonly ApplicationDbContext appDBContext;
        private ObservableCollection<Groups> _groups;
        private string _searchName;

        public string Search
        {
            get { return _searchName; }
            set
            {
                _searchName = value;
                OnPropertyChanged(nameof(Search));
            }
        }

        public ObservableCollection<Groups> Groups
        {
            get { return _groups; }
            set
            {
                _groups = value;
                OnPropertyChanged(nameof(Groups));
            }
        }

        private DelegateCommand<Groups> _deleteCommand;
        private DelegateCommand<Groups> _editCommand;
        private DelegateCommand<Groups> _exportGroupCommand;
        private DelegateCommand _addNewGroupCommand;
        private DelegateCommand _searchCommand;

        public DelegateCommand<Groups> DeleteCommand =>
            _deleteCommand ?? (_deleteCommand = new DelegateCommand<Groups>(ExecuteDeleteCommand));
        public DelegateCommand<Groups> EditCommand =>
            _editCommand ?? (_editCommand = new DelegateCommand<Groups>(ExecuteEditCommand));
        public DelegateCommand<Groups> ExportCommand =>
            _exportGroupCommand ?? (_exportGroupCommand = new DelegateCommand<Groups>(ExecuteExportCommand));
        public DelegateCommand AddNewGroupCommand =>
            _addNewGroupCommand ?? (_addNewGroupCommand = new DelegateCommand(ExecuteAddNewGroupCommand));
        public DelegateCommand SearchCommand =>
            _searchCommand ?? (_searchCommand = new DelegateCommand(ExecuteSearchCommand));

        public GroupsMainVM()
        {
            appDBContext = new ApplicationDbContext();

            _groupService = new GroupService(new GroupRepository(appDBContext));

            LoadDataAsync();
        }

        private async void LoadDataAsync()
        {
            try
            {
                var groups = await _groupService.ListAsync();
                Groups = new ObservableCollection<Groups>(groups);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void ExecuteExportCommand(Groups group)
        {
            try
            {
                var saveDialog = new SaveFileDialog();
                saveDialog.Filter = "PDF (*.pdf)|*.pdf|Word Document (*.docx)|*.docx";

                if (saveDialog.ShowDialog() == true)
                {
                    string selectedPath = Path.GetDirectoryName(saveDialog.FileName);
                    string fileExtension = Path.GetExtension(saveDialog.FileName);

                    if (fileExtension.ToLower() == ".pdf")
                    {
                        await _groupService.ExportGroupToPdf(group, selectedPath);
                        MessageBox.Show($"Students from {group.GroupName} exported successfully as PDF.");
                    }
                    else if (fileExtension.ToLower() == ".docx")
                    {
                        await _groupService.ExportGroupToDocx(group, selectedPath);
                        MessageBox.Show($"Students from {group.GroupName} exported successfully as Word Document.");
                    }
                    else
                    {
                        MessageBox.Show("Invalid file format selected.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ExecuteEditCommand(Groups group)
        {
            try
            {
                if (taskWindow == null || !taskWindow.IsVisible)
                {
                    taskWindow = new AddEditGroupView(appDBContext, group.Id);
                    taskWindow.Closed += (s, eventArgs) =>
                    {
                        taskWindow = null;
                        LoadDataAsync();
                    };
                    
                    taskWindow.Show();
                }
                else
                {
                    taskWindow.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ExecuteAddNewGroupCommand()
        {
            try
            {
                if (taskWindow == null || !taskWindow.IsVisible)
                {
                    taskWindow = new AddEditGroupView();
                    taskWindow.Closed += (s, eventArgs) =>
                    {
                        taskWindow = null;
                        LoadDataAsync();
                    };
                    taskWindow.Show();
                }
                else
                {
                    taskWindow.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void ExecuteDeleteCommand(Groups group)
        {
            try
            {
                await _groupService.DeleteAsync(group.Id);
                _groups.Remove(group);
                OnPropertyChanged(nameof(Groups));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void ExecuteSearchCommand()
        {
            if(!string.IsNullOrEmpty(_searchName))
            {
                var filterdList = await _groupService.FilterByNameListAsync(_searchName);
                Groups = new ObservableCollection<Groups>(filterdList);
            }
            else
            {
                LoadDataAsync();
            }       
        }
    }
}
