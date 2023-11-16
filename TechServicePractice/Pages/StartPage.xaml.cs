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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TechServicePractice
{
    /// <summary>
    /// Логика взаимодействия для StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        public StartPage()
        {
            InitializeComponent();
        }

        private void SingInButtonHandler(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.Navigate(MainWindow.SignInPage);
        }

        private void SignUpButtonHandler(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.Navigate(MainWindow.RegistrationPage);
        }

    }
}
