using syfora_test_API.Model;
using syfora_test_front.Commands;
using syfora_test_front.Models;
using syfora_test_front.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace syfora_test_front.ViewModels
{
    class MainViewVM : ViewModelBase
    {
        private readonly Model _model;
        private ObservableCollection<User> _users;
        private User _newUser;
        private bool _isNew;
        public MainViewVM()
        {            
            UsersView = new UsersView(this);
            Users = new ObservableCollection<User>();
            _model = new Model(this);

            DeleteUser = new DeleteUserCommand(_model);
            OpenUserWindow = new OpenUserCommand(this);
            CreateUser=new CreateUserCommand(_model);
            UpdateUser = new ChangeUserCommand(_model);
        }

        public UserControl UsersView { get; set; }
        public User NewUser
        {
            get => _newUser;
            set
            {
                _newUser = value;
                OnPropertyChanged();
            }
        }
        public bool IsNew
        {
            get => _isNew;
            set
            {
                _isNew = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<User> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged();
            }
        }
        public ICommand CreateUser { get; }
        public ICommand UpdateUser { get; }
        public ICommand OpenUserWindow { get; }
        public ICommand DeleteUser { get; }
    }
}
