using Microsoft.Extensions.Configuration;
using syfora_test_DB.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace syfora_test_DB.Utils
{
    public class UserUtils : IUserUtils
    {
        private readonly IConfiguration Config;
        public UserUtils(IConfiguration config)
        {
            Config= config;
        }
        public async Task<User> CreateUserAsync(User user)
        {
            try
            {
                using (var DB = new DataBase(Config))
                {
                    user.Id= Guid.NewGuid();
                    while (DB.Users.FirstOrDefault(it => it.Id.ToString() == user.Id.ToString())!=null)
                        user.Id = Guid.NewGuid();
                    DB.Users.Add(user);
                    DB.SaveChanges();
                    return user;
                }
            }
            catch (Exception ex)
            {
                //TODO:logs
                return null;
            }
        }

        public async Task<bool> DeleteUserAsync(string guid)
        {
            try
            {
                using (var DB = new DataBase(Config))
                {
                    var _user = DB.Users.FirstOrDefault(it => it.Id.ToString() == guid);
                    if (_user == null) return false;
                    DB.Users.Remove(_user);
                    DB.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                //TODO:logs
                return false;
            }
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            try
            {                
                using (var DB = new DataBase(Config))
                {
                    if (DB.Users.Any())
                        return DB.Users.ToList();
                }
                return new List<User>();
            }
            catch (Exception ex)
            {
                //TODO:logs
                return null;
            }
        }

        public async Task<bool> LoginIsUniqAsync(string login)
        {
            try
            {
                using (var DB = new DataBase(Config))
                {
                    var _user = DB.Users.FirstOrDefault(it => it.Login == login);
                    if (_user == null) return true;
                    else return false;
                }
            }
            catch (Exception ex)
            {
                //TODO:logs
                return false;
            }
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            try
            {
                using (var DB = new DataBase(Config))
                {
                    var _user = DB.Users.FirstOrDefault(it => it.Id.ToString() == user.Id.ToString());
                    if (_user==null) return false;
                    if (user.Login!=_user.Login) _user.Login = user.Login;
                    if (user.FirstName != _user.FirstName) _user.FirstName = user.FirstName;
                    if (user.LastName != _user.LastName) _user.LastName = user.LastName;
                    DB.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                //TODO:logs
                return false;
            }
        }
    }
}
