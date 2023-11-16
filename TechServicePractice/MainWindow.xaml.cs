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
using TechServicePractice.Pages;

namespace TechServicePractice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static long currentUserId;
        public static Frame Instance { get; private set; }
        public static readonly Page SignInPage = new SingInPage();
        public static readonly Page StartPage = new StartPage();
        public static readonly Page RegistrationPage = new RegistrationPage();
        public static readonly Page NewAppealPage = new AppealPage();
        public static Page RequestPageEdit;
        public static Page ClientPage;
        public static Page EmployeePage;
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Content = StartPage;
            
            Instance = MainFrame;
        }
        public static long GetCurrentUser()
        {
            return currentUserId;
        }
        public static void AuthoriseUser(long id, string role)
        {
            currentUserId = id;
            if (role == "Client")
            {
                ClientPage = new ClientPage(id);
                Instance.Navigate(ClientPage);
            }
            if (role == "Manager" || role == "Executor")
            {
                EmployeePage = new EmployeePage(id);
                Instance.Navigate(EmployeePage);
            }
        }
        public static void DeathoriseUser()
        {
            currentUserId = 0;
            Instance.Navigate(StartPage);
        }
        public static void NavigateMainPage(string role)
        {
            if (role == "Client")
            {
                Instance.Navigate(ClientPage);
            }
            if (role == "Employee")
            {
                Instance.Navigate(EmployeePage);
            }
        }
        
        public static void NavigateRequestPage()
        {
            RequestPageEdit = new RequestPage();
            Instance.Navigate(RequestPageEdit);
        }
        public static void NavigateRequestPage(long id, string role)
        {
            RequestPageEdit = new RequestPage(id, role);
            Instance.Navigate(RequestPageEdit);
        }
    }
}
