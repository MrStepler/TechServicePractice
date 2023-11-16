using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void BackButtonHandler(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.Navigate(MainWindow.StartPage);
        }

        private void RegistrationButtonHadler(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(FioTextBox.Text))
            {
                MessageBox.Show("Введите ФИО");
                return;
            }
            if (!FIOisValid(FioTextBox.Text))
            {
                MessageBox.Show("Неверный формат ввода ФИО");
                return;
            }
            if (String.IsNullOrWhiteSpace(PhoneNumberTextBox.Text))
            {
                MessageBox.Show("Введите номер телефона");
                return;
            }
            if (!PhoneNumberIsValid(PhoneNumberTextBox.Text))
            {
                MessageBox.Show("Неверный формат ввода номера телефона");
                return;
            }
            if (BirtDateBox.SelectedDate == null)
            {
                MessageBox.Show("Введите дату рождения");
                return;
            }
            if (!DateIsValid((DateTime)BirtDateBox.SelectedDate))
            {
                MessageBox.Show("Пользование услугами запрещено лицам младше 18 лет");
                return;
            }
            if (String.IsNullOrWhiteSpace(PasswordTextBox.Password))
            {
                MessageBox.Show("Введите пароль");
                return;
            }
            if (PasswordTextBox.Password.Count<char>() < 10)
            {
                MessageBox.Show("Пароль должен быть длинне 10 символов");
                return;
            }

            using (var dbContext = new TechServicePracticeDbContext())
            {
                if (dbContext.Users.Any(x =>x.PhoneNumber == PhoneNumberTextBox.Text))
                {
                    MessageBox.Show("Пользователь с данным номером телефона уже существует");
                    return;
                }
                User newUser = new User
                {
                    Fio = FioTextBox.Text,
                    DateOfBirth = (DateTime)BirtDateBox.SelectedDate,
                    PhoneNumber = PhoneNumberTextBox.Text,
                    UserPassword = PasswordTextBox.Password,
                    UserRoler = "Client"
                };
                dbContext.Users.Add(newUser);
                dbContext.SaveChanges();
                MainWindow.AuthoriseUser(newUser.Id, newUser.UserRoler);
            }

        }
        private bool FIOisValid(string fiostring)
        {
            string FIOpattern = @"[А-Я]{1}[а-я]+ [А-Я]{1}[а-я]+ [А-Я]*[а-я]*";
            if (!Regex.IsMatch(fiostring, FIOpattern))
            {
                return false;
            }
            return true;
        }
        private bool PhoneNumberIsValid(string phoneNumber)
        {
            string numberPattern = @"\D";
            Regex regex = new Regex(numberPattern);
            string replace = "";
            string number = regex.Replace(phoneNumber, replace);
            if (!Regex.IsMatch(number, @"\d{11}"))
            {
                return false;
            }
            return true;
        }
        private bool DateIsValid(DateTime birtDate)
        {
            int age = DateTime.Now.Year - birtDate.Year - 1;
            if (DateTime.Now.Month > birtDate.Month)
            {
                age++;
            }
            if (age < 18)
            {
                return false;
            }
            return true;
        }
    }
}
