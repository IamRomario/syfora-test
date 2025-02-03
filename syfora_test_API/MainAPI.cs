using syfora_test_API.Interface;
using syfora_test_API.Model;
using syfora_test_API.Utils.config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace syfora_test_API
{
    public delegate void OnSuccess<T>(T data);
    public delegate void OnFailed(string errorMessage);

    public class MainAPI:ICRUD
    {
        private ICRUD CrudOptions { get; set; }
        public MainAPI()
        {
            IsLoaded = false;
            using (FileStream fs = new FileStream(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "config.json"), FileMode.OpenOrCreate))
            {
                if (!File.Exists(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "config.json")))
                {
                    IsLoaded = false;
                    return;
                }
                Configuration? config = JsonSerializer.Deserialize<Configuration>(fs);
                if (config == null) return;
                if (config.ApiService.Online)
                    CrudOptions = new WebAPI(config.ApiService.Host);
                else
                    CrudOptions = new XmlAPI(config.ApiService.OfflinePath);
            }
            IsLoaded = true;
        }
        public bool IsLoaded { get; set; }
        public async Task CreateUserAsync(User user,OnSuccess<User> onSuccess, OnFailed onFailed) =>
            await CrudOptions.CreateUserAsync(user, onSuccess, onFailed);
        public async Task GetAllUsersAsync(OnSuccess<IEnumerable<User>> onSuccess, OnFailed onFailed)=>
            await CrudOptions.GetAllUsersAsync(onSuccess, onFailed);
        public async Task UpdateUserAsync(User user, OnSuccess<User> onSuccess, OnFailed onFailed)=>
            await CrudOptions.UpdateUserAsync(user, onSuccess, onFailed);
        public async Task DeleteUserAsync(User user, OnSuccess<User> onSuccess, OnFailed onFailed)=>
            await CrudOptions.DeleteUserAsync(user, onSuccess, onFailed);
    }
}
