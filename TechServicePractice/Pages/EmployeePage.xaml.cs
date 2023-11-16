using Microsoft.EntityFrameworkCore;
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

namespace TechServicePractice.Pages
{
    /// <summary>
    /// Логика взаимодействия для EmployeePage.xaml
    /// </summary>
    public partial class EmployeePage : Page
    {
        private long empId;
        private string role;
        public EmployeePage(long EmployeeId)
        {
            InitializeComponent();
            empId = EmployeeId;
            SetUpEmloyeePage();
            NewRequestButton.Visibility = Visibility.Hidden;
            
        }

        private void RequestTabSelected(object sender, RoutedEventArgs e)
        {
            if (role != "Executor")
            {
                NewRequestButton.Visibility = Visibility.Visible;
            }
            
        }

        private void AppealTabSelected(object sender, RoutedEventArgs e)
        {
            NewRequestButton.Visibility = Visibility.Hidden;
        }

        private void RequestTableLoaded(object sender, RoutedEventArgs e)
        {
            //Добавить роли
            if (role == "Executor")
            {
                using (var DbContext = new TechServicePracticeDbContext())
                {
                    RequestTable.ItemsSource = DbContext.Requests.Where(x=>x.ExecutorId == empId).ToList();
                }
            }
            else
            {
                using (var DbContext = new TechServicePracticeDbContext())
                {
                    RequestTable.ItemsSource = DbContext.Requests.ToList();
                }
            }
            
        }
        private void AppealTableLoaded(object sender, RoutedEventArgs e)
        {
            if (role == "Executor")
            {
                using (var DbContext = new TechServicePracticeDbContext())
                {
                    AppealTable.ItemsSource = DbContext.Appeals.Join(DbContext.Requests,
                        x => x.Id,
                        y =>y.AppealId,
                        (x,y)=> new
                        {
                            x,
                            y
                        }).Where(f=>f.y.ExecutorId ==empId).Select(z =>z.x).ToList();
                }
            }
            else
            {
                using (var DbContext = new TechServicePracticeDbContext())
                {
                    AppealTable.ItemsSource = DbContext.Appeals.ToList();
                }
            }
            
        }

        private async void EditSelectedRequest(object sender, RoutedEventArgs e)
        {
           Button button = sender as Button;
            long id = (long) button.CommandParameter;
            using (var DbContext = new TechServicePracticeDbContext())
            {
                var selectedRequest = await DbContext.Requests.FirstOrDefaultAsync(x => x.Id == id);
                var currentUser = await DbContext.Users.FirstOrDefaultAsync(x => x.Id == empId);
                MainWindow.NavigateRequestPage(selectedRequest.Id, currentUser.UserRoler);
            }
            
        }
        private void SetUpEmloyeePage()
        {
            using (var DbContext = new TechServicePracticeDbContext())
            {
                var currentUser = DbContext.Users.FirstOrDefault(x => x.Id == empId);
                FioLabel.Content = currentUser.Fio;
                role = currentUser.UserRoler;
                if (currentUser.UserRoler == "Executor")
                {
                    NewRequestButton.Visibility = Visibility.Hidden;
                }
            }
        }

        private void DeathoriseUserButtonHandler(object sender, RoutedEventArgs e)
        {
            MainWindow.DeathoriseUser();
        }

        private void NewRequestButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.NavigateRequestPage();
        }
    }
}
