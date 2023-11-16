using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для Requests.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
        private long currentUserId;
        public ClientPage(long currentUserId)
        {
            InitializeComponent();
            this.currentUserId = currentUserId;
        }

        private void DeathoriseButtonHandler(object sender, RoutedEventArgs e)
        {
            MainWindow.DeathoriseUser();
        }

        private async void LoadUserAppeals(object sender, RoutedEventArgs e)
        {
            using (var DbContext = new TechServicePracticeDbContext())
            {
                var currentUser = await DbContext.Users.FirstOrDefaultAsync(x => x.Id == currentUserId);
                FioLabel.Content = currentUser.Fio;
                DateLabel.Content = currentUser.DateOfBirth;
                PhoneLabel.Content = currentUser.PhoneNumber;

                AppealGrid.ItemsSource = DbContext.Appeals.Where(x => x.ClientId == currentUserId).ToList();
            }
        }

        private void NewAppealHandler(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.Navigate(MainWindow.NewAppealPage);
        }
    }
}
