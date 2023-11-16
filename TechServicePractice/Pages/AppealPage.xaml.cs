using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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

namespace TechServicePractice.Pages
{
    /// <summary>
    /// Логика взаимодействия для AppealPage.xaml
    /// </summary>
    public partial class AppealPage : Page
    {
        public AppealPage()
        {
            InitializeComponent();
        }

        private async void NewAppealButtonHandler(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(DeviceTypeTextBox.Text))
            {
                MessageBox.Show("Заполните тип устройства");
                return;
            }
            if (String.IsNullOrWhiteSpace(ProblemDecriptionTextBox.Text))
            {
                MessageBox.Show("Заполните описание проблемы");
                return;
            }
            using (var DbContext = new TechServicePracticeDbContext())
            {
                await DbContext.Appeals.AddAsync(new Appeal
                {
                    DateOfAppeal = DateTime.Now,
                    AppealStatus = "В обработке",
                    GadgetType = DeviceTypeTextBox.Text,
                    ProblemDescription = ProblemDecriptionTextBox.Text,
                    Client = await DbContext.Users.FirstAsync(x=>x.Id == MainWindow.GetCurrentUser())
                });
                await DbContext.SaveChangesAsync();
                MessageBox.Show("Обращение создано");
            }
            MainWindow.NavigateMainPage("Client");
        }

        private void BackButtonHandler(object sender, RoutedEventArgs e)
        {
            MainWindow.NavigateMainPage("Client");
        }
    }
}
