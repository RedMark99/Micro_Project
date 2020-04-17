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

namespace Micro_Project
{
    /// <summary>
    /// Логика взаимодействия для Auto.xaml
    /// </summary>
    public partial class Auto : Window
    {
        public bool show = false;
        public Auto()
        {
            InitializeComponent();
            TBLogin.Text = "Введите логин";
            TBPassword.Password = "Введите пароль";
        }

        private void BAuto_Click(object sender, RoutedEventArgs e)
        {
            if (TBLogin.Text == "" || TBPassword.Password == "")
                return;

            using (var context = new MyDbContext())
            {
                var query = context.Clients.Where(x => x.login == TBLogin.Text && x.password == TBPassword.Password);

                if (TBLogin.Text == "Admin" && TBPassword.Password == "12345") // для администратора
                {
                    MainWindow adminmenu = new MainWindow();
                    adminmenu.Show();
                    this.Visibility = Visibility.Hidden;
                }
                else if (query.ToList().Count > 0) // для пользователя
                {
                    sendDate.login = TBLogin.Text;
                    UserMenu usermenu = new UserMenu();
                    usermenu.Show();
                    this.Visibility = Visibility.Hidden;
                }
                else 
                {
                    MessageBox.Show("Логин или пароль был введён не правильно");
                }
            }
        }

        private void TBLogin_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TBLogin.Text == "Введите логин")
            {
                TBLogin.Text = "";
            }
        }

        private void TBLogin_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TBLogin.Text == "")
            {
                TBLogin.Text = "Введите логин";
            }
        }

        private void TBPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TBPassword.Password == "Введите пароль")
            {
                TBPassword.Password = "";
                TBPassword.PasswordChar = '*';
            }
        }

        private void TBPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TBPassword.Password == "")
            {
                TBPassword.Password = "Введите пароль";
                TBPassword.PasswordChar = '\0';
            }
        }

        private void BClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
