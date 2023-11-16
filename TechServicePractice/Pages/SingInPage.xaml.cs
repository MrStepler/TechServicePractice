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
    /// Логика взаимодействия для SingInPage.xaml
    /// </summary>
    public partial class SingInPage : Page
    {
        public SingInPage()
        {
            InitializeComponent();
        }

        private void BackButtonHandler(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.Navigate(MainWindow.StartPage);
        }

        private void SignInHandler(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(PhoneNumberTextBox.Text))
            {
                MessageBox.Show("Введите номер телефона");
                return;
            }
            if (String.IsNullOrWhiteSpace(PasswordTextBox.Password))
            {
                MessageBox.Show("Введите пароль");
                return;
            }
            using (var DbContext = new TechServicePracticeDbContext())
            {
                var user = DbContext.Users.FirstOrDefault(x => x.PhoneNumber == PhoneNumberTextBox.Text && x.UserPassword == PasswordTextBox.Password);
                if (user == null)
                {
                    MessageBox.Show("Пользователь не найден, или неверные входные данные");
                }
                else 
                {
                    MainWindow.AuthoriseUser(user.Id, user.UserRoler);
                }
            }
           
        }
    }
}
