using syfora_test_API.Model;
using syfora_test_front.Models;
using syfora_test_front.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace syfora_test_front.Commands
{
    class ChangeUserCommand : CommandBase
    {
        private readonly Model _model;
        public ChangeUserCommand(Model model)
        {
            _model = model;
        }
        public override async void Execute(object parameter)
        {
            try
            {
                if (parameter != null && parameter is User)
                {
                    await _model.UpdateUserAsync(parameter as User,
                        async (user) => {
                            await _model.UpdateUserAsync(user);
                            MessageBox.Show("Пользователь изменен");
                            var win= Application.Current.Windows
                                        .OfType<CreateChangeUserView>()
                                        .FirstOrDefault();
                            if (win!=null) win.Close();
                        },
                        (err) => MessageBox.Show(err));
                }
            }
            catch (Exception ex)
            {
                //TODO:logs
            }
        }
    }
}
