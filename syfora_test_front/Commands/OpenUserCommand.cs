using syfora_test_API.Model;
using syfora_test_front.Models;
using syfora_test_front.ViewModels;
using syfora_test_front.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace syfora_test_front.Commands
{
    class OpenUserCommand : CommandBase
    {
        private readonly MainViewVM _vm;
        public OpenUserCommand(MainViewVM vm)
        {
            _vm= vm;
        }
        public override void Execute(object parameter)
        {
            try
            {
                if (parameter != null && parameter is User)
                {
                    _vm.IsNew = false;
                    _vm.NewUser = new User()
                    {
                        Id = (parameter as User).Id,
                        Login = (parameter as User).Login,
                        FirstName = (parameter as User).FirstName,
                        LastName = (parameter as User).LastName
                    };
                    var win = new CreateChangeUserView(_vm);
                    win.ShowDialog();
                }
                else
                {
                    _vm.IsNew = true;
                    _vm.NewUser = new User()
                    {
                        Login = "defaultLogin",
                        FirstName = "Иван",
                        LastName = "Иванович"
                    };
                    var win = new CreateChangeUserView(_vm);
                    win.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                //TODO:logs
            }
        }
    }
}
