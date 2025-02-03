using Azure;
using Microsoft.AspNetCore.Mvc;
using syfora_test_DB.Tables;
using syfora_test_DB.Utils;
using syfora_test_WebServer.Model;
using System.Net;

namespace syfora_test_WebServer.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    public class UserController: ControllerBase
    {
        private readonly IUserUtils UserUtils;
        public UserController(IUserUtils userUtils)
        {
            UserUtils=userUtils;
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users=await UserUtils.GetAllUsersAsync();
            if (users==null)
                return new JSON<string>()
                {
                    Title = "Ошибка",
                    Status = (int)HttpStatusCode.Conflict,
                    Errors = new Dictionary<string, string>() { { nameof(Exception), "Ошибка" } }
                };
            return new JSON<IEnumerable<User>>()
            {
                Title = "Успешно",
                Status = (int)HttpStatusCode.OK,
                Errors = new Dictionary<string, string>(),
                Data = users
            };
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateUser([FromBody] User newUser)
        {
            if (!await UserUtils.LoginIsUniqAsync(newUser.Login))
                return new JSON<string>()
                {
                    Title = "Логин занят",
                    Status = (int)HttpStatusCode.Conflict,
                    Errors = new Dictionary<string, string>() { { nameof(Exception), "Ошибка" } }
                };
            var user = await UserUtils.CreateUserAsync(newUser);            
            if (user == null)
                return new JSON<string>()
                {
                    Title = "Ошибка",
                    Status = (int)HttpStatusCode.Conflict,
                    Errors = new Dictionary<string, string>() { { nameof(Exception), "Ошибка" } }
                };
            return new JSON<User>()
            {
                Title = "Пользователь создан",
                Status = (int)HttpStatusCode.OK,
                Errors = new Dictionary<string, string>(),
                Data = user
            };
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteUser([FromBody] User user)
        {
            if (!await UserUtils.DeleteUserAsync(user.Id.ToString()))
                return new JSON<string>()
                {
                    Title = "Ошибка удаления пользователя",
                    Status = (int)HttpStatusCode.Conflict,
                    Errors = new Dictionary<string, string>() { { nameof(Exception), "Ошибка" } }
                };
            return new JSON<string>()
            {
                Title = "Пользователь удален",
                Status = (int)HttpStatusCode.OK,
                Errors = new Dictionary<string, string>()
            };
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {            
            if (!await UserUtils.UpdateUserAsync(user))
                return new JSON<string>()
                {
                    Title = "Ошибка",
                    Status = (int)HttpStatusCode.Conflict,
                    Errors = new Dictionary<string, string>() { { nameof(Exception), "Ошибка" } }
                };
            return new JSON<string>()
            {
                Title = "Данные пользователя обновлены",
                Status = (int)HttpStatusCode.OK,
                Errors = new Dictionary<string, string>()
            };
        }
    }
}
