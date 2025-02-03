using syfora_test_API.Model;
using syfora_test_front.Models;
using syfora_test_front.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace syfora_test_front.Commands
{
    internal class DeleteUserCommand: CommandBase
    {
        private readonly Model _model;
        public DeleteUserCommand(Model model)
        {
            _model = model;
        }
        public override async void Execute(object parameter)
        {
            try
            {
                if (parameter != null && parameter is User)
                {
                    var result = MessageBox.Show(
                        $"Вы действительно хотите удалить пользователя '{(parameter as User).Login}'",
                        "Потверждение",
                        MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.Yes)
                        await _model.DeleteUserAsync(parameter as User,
                            async (user) => {                            
                                await _model.DeleteUserAsync(user);
                                MessageBox.Show("Пользователь удален");
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
