using syfora_test_front.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace syfora_test_front.Views
{
    /// <summary>
    /// Логика взаимодействия для CreateChangeUserView.xaml
    /// </summary>
    public partial class CreateChangeUserView : Window
    {
        public CreateChangeUserView(object dataContext, bool isNew=true)
        {
            InitializeComponent();
            DataContext = dataContext;
        }
    }
}
