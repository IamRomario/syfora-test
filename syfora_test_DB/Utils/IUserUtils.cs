using syfora_test_DB.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syfora_test_DB.Utils
{
    public interface IUserUtils
    {
        public Task<bool> LoginIsUniqAsync(string login);
        public Task<User> CreateUserAsync(User user);
        public Task<IEnumerable<User>> GetAllUsersAsync();
        public Task<bool> UpdateUserAsync(User user);
        public Task<bool> DeleteUserAsync(string giud);
    }
}
