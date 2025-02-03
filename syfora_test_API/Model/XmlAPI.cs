using syfora_test_API.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace syfora_test_API.Model
{
    internal class XmlAPI : ICRUD
    {        
        private string _path;
        public XmlAPI(string path)
        {
            _path = path;
            if (!File.Exists(path))
            {
                XmlDocument doc = new XmlDocument();

                XmlElement root = doc.CreateElement("users");
                doc.AppendChild(root);
                doc.Save(_path);
            }            
        }
        public async Task CreateUserAsync(User user, OnSuccess<User> onSuccess, OnFailed onFailed)
        {
            await GetAllUsersAsync(
                (users) => {
                    if (users.Where(it => it.Login == user.Login).Any())
                    {
                        onFailed.Invoke("Логин занят");
                        return;
                    }                        
                    user.Id = Guid.NewGuid();
                    while (users.Where(it => it.Id.ToString() == user.Id.ToString()).Any())
                        user.Id = Guid.NewGuid();

                    XDocument xdoc = XDocument.Load(_path);
                    XElement? root = xdoc.Element("users");

                    if (root != null)
                    {
                        root.Add(new XElement("user",
                                    new XElement("Id", user.Id.ToString()),
                                    new XElement("Login", user.Login),
                                    new XElement("FirstName", user.FirstName),
                                    new XElement("LastName", user.LastName)));

                        xdoc.Save(_path);
                    }
                    onSuccess.Invoke(user);                    
                },
                (err) => onFailed.Invoke(err));
        }
        public async Task GetAllUsersAsync(OnSuccess<IEnumerable<User>> onSuccess, OnFailed onFailed)
        {
            try
            {
                XDocument xdoc = XDocument.Load(_path);

                var users = xdoc.Element("users")?
                    .Elements("user")
                    .Select(p => new User()
                    {
                        Id = new Guid(p.Element("Id")?.Value),
                        Login = p.Element("Login")?.Value,
                        FirstName = p.Element("FirstName")?.Value,
                        LastName = p.Element("LastName")?.Value
                    }).ToList();

                onSuccess.Invoke(users is null ? new List<User>() : users);
            }
            catch (Exception ex)
            {
                onFailed.Invoke(ex.Message);
            }            
        }
        public async Task UpdateUserAsync(User user, OnSuccess<User> onSuccess, OnFailed onFailed)
        {
            try
            {
                XDocument xdoc = XDocument.Load(_path);

                var _user = xdoc.Element("users")?
                    .Elements("user")
                    .FirstOrDefault(p => p.Element("Id")?.Value == user.Id.ToString());

                if (_user == null)
                {
                    onFailed.Invoke("Пользователь не найден");
                    return;
                }

                var login = _user.Element("Login");
                if (login != null) login.Value = user.Login;
                var firstName = _user.Element("FirstName");
                if (firstName != null) firstName.Value = user.FirstName;
                var lastName = _user.Element("LastName");
                if (lastName != null) lastName.Value = user.LastName;

                xdoc.Save(_path);
                onSuccess.Invoke(user);
            }
            catch (Exception ex)
            {
                onFailed.Invoke(ex.Message);
            }            
        }
        public async Task DeleteUserAsync(User user, OnSuccess<User> onSuccess, OnFailed onFailed)
        {
            try
            {
                XDocument xdoc = XDocument.Load(_path);
                XElement? root = xdoc.Element("users");

                if (root == null)
                {
                    onFailed.Invoke("Пользователь не найден");
                    return;
                }
                var deletedUser = root.Elements("user")
                        .FirstOrDefault(p => p.Element("Id")?.Value == user.Id.ToString());
                if (deletedUser == null)
                {
                    onFailed.Invoke("Пользователь не найден");
                    return;
                }
                deletedUser.Remove();
                xdoc.Save(_path);
                onSuccess.Invoke(user);
            }
            catch (Exception ex)
            {
                onFailed.Invoke(ex.Message);
            }            
        }
    }
}
