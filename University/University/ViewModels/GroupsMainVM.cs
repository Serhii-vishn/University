﻿using Prism.Commands;
using System.Collections.ObjectModel;
using System.Windows;
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

        private ObservableCollection<Group> _groups;

        private DelegateCommand<Group> _deleteCommand;
        private DelegateCommand<Group> _editCommand;
        private DelegateCommand _addNewGroupCommand;

        public DelegateCommand<Group> DeleteCommand =>
            _deleteCommand ?? (_deleteCommand = new DelegateCommand<Group>(ExecuteDeleteCommand));
        public DelegateCommand<Group> EditCommand =>
            _editCommand ?? (_editCommand = new DelegateCommand<Group>(ExecuteEditCommand));
        public DelegateCommand AddNewGroupCommand =>
            _addNewGroupCommand ?? (_addNewGroupCommand = new DelegateCommand(ExecuteAddNewGroupCommand));

        public ObservableCollection<Group> Groups
        {
            get { return _groups; }
            set
            {
                _groups = value;
                OnPropertyChanged(nameof(Groups));
            }
        }

        public GroupsMainVM()
        {
            var appDBContext = new ApplicationDbContext();

            _groupService = new GroupService(new GroupRepository(appDBContext));

            LoadDataAsync();
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

        private async void ExecuteEditCommand(Group group)
        {
            try
            {
                if (taskWindow == null || !taskWindow.IsVisible)
                {
                    taskWindow = new EditGroupView(group.Id);
                    taskWindow.Closed += (s, eventArgs) => taskWindow = null;
                    taskWindow.Show();
                    OnPropertyChanged(nameof(Groups));                   
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

        private async void ExecuteAddNewGroupCommand()
        {
            try
            {
                if (taskWindow == null || !taskWindow.IsVisible)
                {
                    taskWindow = new AddGroupView();
                    taskWindow.Closed += (s, eventArgs) => taskWindow = null;
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
    }
}