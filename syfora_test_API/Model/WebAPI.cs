using Newtonsoft.Json;
using syfora_test_API.Interface;
using syfora_test_API.Model.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace syfora_test_API.Model
{
    internal class WebAPI:ICRUD
    {
        private readonly HttpClient Client;
        public WebAPI(string path)
        {
            var Handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false,
                UseCookies = false
            };
            Client = new HttpClient(Handler);
            Client.BaseAddress = new Uri("http://localhost:5000/"); 
        }
        public async Task CreateUserAsync(User user, OnSuccess<User> onSuccess, OnFailed onFailed)
        {
            var response = await WebAPIExtentions.Request("api/v1/users/create", HttpMethod.Post)
                                               .UseJson(user)
                                               .SendRequestAsync(Client);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = JsonConvert.DeserializeObject<ApiResponse<User>>(await response.Content.ReadAsStringAsync());
                onSuccess.Invoke(content.Data);
                return;
            }
            if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                var content = JsonConvert.DeserializeObject<ApiResponse>(await response.Content.ReadAsStringAsync());
                onFailed.Invoke(content.Title);
                return;
            }
            if (!response.IsSuccessStatusCode)
                onFailed.Invoke("Непредвиденная ошибка");

        }
        public async Task GetAllUsersAsync(OnSuccess<IEnumerable<User>> onSuccess, OnFailed onFailed)
        {
            var response = await WebAPIExtentions.Request("api/v1/users/getall", HttpMethod.Get)
                                               .SendRequestAsync(Client);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = JsonConvert.DeserializeObject<ApiResponse<IEnumerable<User>>>(await response.Content.ReadAsStringAsync());
                onSuccess.Invoke(content.Data);
                return;
            }
            if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                var content = JsonConvert.DeserializeObject<ApiResponse>(await response.Content.ReadAsStringAsync());
                onFailed.Invoke(content.Title);
                return;
            }
            if (!response.IsSuccessStatusCode)
                onFailed.Invoke("Непредвиденная ошибка");
        }
        public async Task UpdateUserAsync(User user, OnSuccess<User> onSuccess, OnFailed onFailed)
        {
            var response = await WebAPIExtentions.Request("api/v1/users/update", HttpMethod.Post)
                                               .UseJson(user)
                                               .SendRequestAsync(Client);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = JsonConvert.DeserializeObject<ApiResponse>(await response.Content.ReadAsStringAsync());
                onSuccess.Invoke(user);
                return;
            }
            if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                var content = JsonConvert.DeserializeObject<ApiResponse>(await response.Content.ReadAsStringAsync());
                onFailed.Invoke(content.Title);
                return;
            }
            if (!response.IsSuccessStatusCode)
                onFailed.Invoke("Непредвиденная ошибка");
        }
        public async Task DeleteUserAsync(User user, OnSuccess<User> onSuccess, OnFailed onFailed)
        {
            var response = await WebAPIExtentions.Request("api/v1/users/delete", HttpMethod.Delete)
                                               .UseJson(user)
                                               .SendRequestAsync(Client);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = JsonConvert.DeserializeObject<ApiResponse>(await response.Content.ReadAsStringAsync());
                onSuccess.Invoke(user);
                return;
            }
            if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                var content = JsonConvert.DeserializeObject<ApiResponse>(await response.Content.ReadAsStringAsync());
                onFailed.Invoke(content.Title);
                return;
            }
            if (!response.IsSuccessStatusCode)
                onFailed.Invoke("Непредвиденная ошибка");
        }
    }
}
