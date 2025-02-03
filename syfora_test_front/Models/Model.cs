using syfora_test_API;
using syfora_test_API.Interface;
using syfora_test_API.Model;
using syfora_test_front.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace syfora_test_front.Models
{
    internal sealed class Model
    {
        private readonly MainAPI _mainAPI;
        private ObservableCollection<User> _users { get; set; }
        public Model(MainViewVM viewModel)
        {
            _mainAPI = new MainAPI();
            _mainAPI.GetAllUsersAsync(
                (users) => {
                    _users = viewModel.Users = new ObservableCollection<User>(users);
                },
                (err) =>  MessageBox.Show(err)).Wait();
        }


        public async Task AddUserAsync(User user) { _users.Add(user); }
        public async Task DeleteUserAsync(User user) { _users.Remove(user); }
        public async Task UpdateUserAsync(User user)
        {
            var oldUser = _users.Where(it => it.Id == user.Id).FirstOrDefault();
            if (oldUser == null) return;
            if (oldUser.Login != user.Login) oldUser.Login = user.Login;
            if (oldUser.FirstName != user.FirstName) oldUser.FirstName = user.FirstName;
            if (oldUser.LastName != user.LastName) oldUser.LastName = user.LastName;
        }
        public async Task CreateUserAsync(User user, OnSuccess<User> onSuccess, OnFailed onFailed) => 
            await _mainAPI.CreateUserAsync(user,onSuccess,onFailed);

        public async Task GetAllUsersAsync(OnSuccess<IEnumerable<User>> onSuccess, OnFailed onFailed)=>
            await _mainAPI.GetAllUsersAsync(onSuccess, onFailed);

        public async Task UpdateUserAsync(User user, OnSuccess<User> onSuccess, OnFailed onFailed)=>
            await _mainAPI.UpdateUserAsync(user,onSuccess,onFailed);

        public async Task DeleteUserAsync(User user, OnSuccess<User> onSuccess, OnFailed onFailed)=>
            await _mainAPI.DeleteUserAsync(user,onSuccess,onFailed);

    }
}
