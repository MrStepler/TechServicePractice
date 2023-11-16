using Microsoft.EntityFrameworkCore;
using System;
using System.CodeDom.Compiler;
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
    /// Логика взаимодействия для RequestPage.xaml
    /// </summary>
    public partial class RequestPage : Page
    {
        private string currentRole;
        private bool EditMode;
        private long requestId;
        public RequestPage()
        {
            InitializeComponent();
            currentRole = "Manager";
            EditMode = false;
        }
        /// <summary>
        /// Конструктор страницы формы заявки для редактирования существующей заявки
        /// </summary>
        /// <param name="requestId">Идентификатор заявки в БД </param>
        /// <param name="role">Роль пользователя инициирующий заявку</param>
        public RequestPage(long requestId, string role)
        {
            InitializeComponent();
            currentRole = role;
            this.requestId = requestId;
            EditMode = true;
            SetUp(requestId);
        }

        private async void SetUp()
        {
            PriorityCb.ItemsSource = new int[] { 1,2,3,4,5};
            PriorityCb.SelectedIndex = 4;
            using (var DbContext = new TechServicePracticeDbContext())
            {
                var ListAppealsId = await DbContext.Appeals.Where(x=>x.AppealStatus == "В обработке").Select(x=>x.Id).ToListAsync();
                if (ListAppealsId.Count() > 0)
                {
                    ExecutorCb.Text = "Выберете обращение";
                    AppealCb.ItemsSource = ListAppealsId;
                }
                else
                {
                    AppealCb.ItemsSource = new string[] { "Обращений нет" };
                    AppealCb.SelectedIndex = 0;
                    AppealCb.IsEnabled = false;
                }
                var ListUsers = await DbContext.Users.Where(x => x.UserRoler=="Executor").ToListAsync();
                if (ListUsers.Count() > 0)
                {
                    ExecutorCb.Text = "Выберете исполнителя";
                    ExecutorCb.ItemsSource = ListUsers;
                }
                else
                {
                    ExecutorCb.SelectedValue = "Исполнителей нет";
                    ExecutorCb.IsEditable = false;
                }
                
                HeaderLabel.Content = "Новая заявка";
                RequestButton.Content = "Создать";
            }
        }
        private async void SetUp(long requestId)
        {
            if (currentRole == "Executor")
            {
                ExecutorCb.IsEnabled = false;
                PriorityCb.IsEnabled = false;

            }
            PriorityCb.ItemsSource = new int[] { 1, 2, 3, 4, 5 };
            PriorityCb.SelectedIndex = 4;
            using (var DbContext = new TechServicePracticeDbContext())
            {

                var ListUsers = await DbContext.Users.Where(x => x.UserRoler == "Executor").ToListAsync();
                var SelectedRequest = await DbContext.Requests.FirstAsync(x => x.Id == requestId);
                AppealCb.ItemsSource = await DbContext.Appeals.Select(x =>x.Id).ToListAsync();

                PriorityCb.SelectedItem = SelectedRequest.RequestPriority;
                
                AppealCb.SelectedItem = SelectedRequest.AppealId;
                AppealCb.IsEnabled = false;
                ExecutorCb.ItemsSource = ListUsers;
                ExecutorCb.SelectedItem = SelectedRequest.Executor;


                ProblemTypeTextBox.Text = SelectedRequest.ProblemType;
                SearialNumberTb.Text = SelectedRequest.SerialNumber.ToString();
                CompletingDateDp.SelectedDate = SelectedRequest.CompleatingDate;
                CommentaryTb.Text = SelectedRequest.Commentary;

                HeaderLabel.Content = "Редактировать заявку";
                RequestButton.Content = "Сохранить";
            }
        }

        private void BackButtonHandler(object sender, RoutedEventArgs e)
        {
            MainWindow.NavigateMainPage("Employee");
        }
        private async void RequestButtonHandler(object sender, RoutedEventArgs e)
        {
            if (ExecutorCb.SelectedValue == null || ExecutorCb.SelectedValue == "Выберете исполнителя" || ExecutorCb.SelectedValue == "Исполнителей нет")
            {
                MessageBox.Show("Выберите исполнителя");
                return;
            }
            if (AppealCb.SelectedValue == null || AppealCb.SelectedValue == "Обращений нет" || AppealCb.SelectedValue == "Выберете обращение")
            {
                MessageBox.Show("Выберите обращение");
                return;
            }
            using (var DbContext = new TechServicePracticeDbContext())
            {
                if (CompletingDateDp.SelectedDate != null)
                {
                    var selectedAppeal = await DbContext.Appeals.FirstOrDefaultAsync(x => x.Id == (long)AppealCb.SelectedValue);
                    selectedAppeal.AppealStatus = "Закрыто";
                }
                else
                {
                    if (!EditMode)
                    {
                        var selectedAppeal = await DbContext.Appeals.FirstOrDefaultAsync(x => x.Id == (long)AppealCb.SelectedValue);
                        selectedAppeal.AppealStatus = "В работе";
                    }
                }

                if (EditMode)
                {
                    Request request = await DbContext.Requests.FirstOrDefaultAsync(x => x.Id == requestId);
                    request.AppealId = (long)AppealCb.SelectedValue;
                    var selectedExecutor = (User)ExecutorCb.SelectedValue;
                    request.ExecutorId = selectedExecutor.Id;
                    request.ProblemType = ProblemTypeTextBox.Text;
                    request.SerialNumber = !String.IsNullOrWhiteSpace(SearialNumberTb.Text) ? long.Parse(SearialNumberTb.Text) : null;
                    request.CompleatingDate = CompletingDateDp.SelectedDate != null ? CompletingDateDp.SelectedDate : null;
                    request.Commentary = CommentaryTb.Text;
                    request.RequestPriority = (int)PriorityCb.SelectedValue;
                    DbContext.Requests.Update(request);
                }
                else
                {
                    Request request = new Request();
                    request.AppealId = (long)AppealCb.SelectedValue;
                    var selectedExecutor = (User)ExecutorCb.SelectedValue;
                    request.ExecutorId = selectedExecutor.Id;
                    request.ProblemType = ProblemTypeTextBox.Text;
                    request.SerialNumber = !String.IsNullOrWhiteSpace(SearialNumberTb.Text) ? long.Parse(SearialNumberTb.Text) : null;
                    request.CompleatingDate = CompletingDateDp.SelectedDate != null ? CompletingDateDp.SelectedDate : null;
                    request.Commentary = CommentaryTb.Text;
                    request.RequestPriority = (int)PriorityCb.SelectedValue;
                    DbContext.Requests.Add(request);
                }

                await DbContext.SaveChangesAsync();


            }
            if (EditMode)
            {
                MessageBox.Show("Заявка успешно изменена");
            }
            else
            {
                MessageBox.Show("Заявка успешно создана");
            }

            MainWindow.NavigateMainPage("Employee");

        }

        private void RequestDataLoaded(object sender, RoutedEventArgs e)
        {
            if (currentRole == "Manager")
            {
                if (!EditMode)
                {
                    SetUp();
                }
                
            }
        }
    }
}
