using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using Prism.Commands;
using University.DbContexts;
using University.Models;
using University.Repositories;
using University.Services;
using University.Services.Interfaces;
using University.Views;

namespace University.ViewModels
{
    public class GroupsMainVM : 
        ViewModelBase
    {
        private Window? taskWindow = null;
        private readonly IGroupService _groupService;
        private readonly ApplicationDbContext appDBContext;
        private ObservableCollection<Group> _groups;
        private string _search;

        public string Search
        {
            get { return _search; }
            set
            {
                _search = value;
                OnPropertyChanged(nameof(Search));
            }
        }

        public ObservableCollection<Group> Groups
        {
            get { return _groups; }
            set
            {
                _groups = value;
                OnPropertyChanged(nameof(Groups));
            }
        }

        private DelegateCommand<Group> _deleteCommand;
        private DelegateCommand<Group> _editCommand;
        private DelegateCommand<Group> _exportGroupCommand;
        private DelegateCommand _addNewGroupCommand;
        private DelegateCommand _searchCommand;

        public DelegateCommand<Group> DeleteCommand =>
            _deleteCommand ?? (_deleteCommand = new DelegateCommand<Group>(ExecuteDeleteCommand));
        public DelegateCommand<Group> EditCommand =>
            _editCommand ?? (_editCommand = new DelegateCommand<Group>(ExecuteEditCommand));
        public DelegateCommand<Group> ExportCommand =>
            _exportGroupCommand ?? (_exportGroupCommand = new DelegateCommand<Group>(ExecuteExportCommand));
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
                Groups = new ObservableCollection<Group>(groups);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void ExecuteExportCommand(Group group)
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

        private void ExecuteEditCommand(Group group)
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

        private async void ExecuteDeleteCommand(Group group)
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
            await _groupService.
        }
    }
}
