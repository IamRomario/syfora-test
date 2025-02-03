using syfora_test_API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syfora_test_API.Interface
{
    public interface ICRUD
    {
        public Task CreateUserAsync(User user, OnSuccess<User> onSuccess, OnFailed onFailed);
        public Task GetAllUsersAsync(OnSuccess<IEnumerable<User>> onSuccess, OnFailed onFailed);
        public Task UpdateUserAsync(User user, OnSuccess<User> onSuccess, OnFailed onFailed);
        public Task DeleteUserAsync(User user, OnSuccess<User> onSuccess, OnFailed onFailed);
    }
}
