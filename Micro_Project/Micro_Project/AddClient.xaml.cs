using Micro_Project.MyDbClass;
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
    /// Логика взаимодействия для AddClient.xaml
    /// </summary>
    public partial class AddClient : Window
    {
        MainWindow _owner;
        public AddClient(MainWindow owner)
        {
            InitializeComponent();
            TBName.Text = "Имя";
            TBSurname.Text = "Фамилия";
            TBLastname.Text = "Отчество";
            TBLogin.Text = "Логин";
            TBPassword.Text = "Пароль";
            TBSeriapassport.Text = "Серия Паспорта";
            TBNumerpassport.Text = "Номер Паспорта";
            TBDeschotel.Text = "Описание";

            _owner = owner;
            this.Closed += new EventHandler(this.AddClient_FormClosing);
        }

        private void AddClient_FormClosing(object sender, EventArgs e)
        {
            _owner.RefreshAddClient();
        }

        public void addClient()
        {
            using (var context = new MyDbContext())
            {
                var query = context.Clients.Where(x => x.login == TBLogin.Text || x.numberPassport == TBNumerpassport.Text);

                var client = new Client()
                {
                    name = TBName.Text,
                    surname = TBSurname.Text,
                    lastname = TBLastname.Text,
                    login = TBLogin.Text,
                    password = TBPassword.Text,
                    seriaPassport = TBSeriapassport.Text,
                    numberPassport = TBNumerpassport.Text,
                    dataOfBirth = DateTime.Parse(dateTimePickerDateOfBirth.SelectedDate.Value.ToString()),
                    countDaysLive = 0,
                    deschotel = TBDeschotel.Text
                };

                if (query.ToList().Count < 1)
                {
                    context.Clients.Add(client);
                    context.SaveChanges();
                    MessageBox.Show("Добавлено");
                    this.Close();
                }

                else if (query.ToList().Count > 0)
                {
                    MessageBox.Show("Логин или Номер Пасспорта такой уже есть в базе данных");
                }
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            addClient();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Client();
            this.Close();
        }

        private void TBName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TBName.Text == "Имя")
            {
                TBName.Text = "";
            }
        }

        private void TBName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TBName.Text == "")
            {
                TBName.Text = "Имя";
            }
        }

        private void TBSurname_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TBSurname.Text == "Фамилия")
            {
                TBSurname.Text = "";
            }
        }

        private void TBSurname_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TBSurname.Text == "")
            {
                TBSurname.Text = "Фамилия";
            }
        }

        private void TBLastname_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TBLastname.Text == "Отчество")
            {
                TBLastname.Text = "";
            }
        }

        private void TBLastname_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TBLastname.Text == "")
            {
                TBLastname.Text = "Отчество";
            }
        }

        private void TBLogin_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TBLogin.Text == "Логин")
            {
                TBLogin.Text = "";
            }
        }

        private void TBLogin_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TBLogin.Text == "")
            {
                TBLogin.Text = "Логин";
            }
        }

        private void TBPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TBPassword.Text == "")
            {
                TBPassword.Text = "Пароль";
            }
        }

        private void TBPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TBPassword.Text == "Пароль")
            {
                TBPassword.Text = "";
            }
        }

        private void TBSeriapassport_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TBSeriapassport.Text == "Серия Паспорта")
            {
                TBSeriapassport.Text = "";
            }
        }

        private void TBSeriapassport_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TBSeriapassport.Text == "")
            {
                TBSeriapassport.Text = "Серия Паспорта";
            }
        }

        private void TBNumerpassport_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TBNumerpassport.Text == "")
            {
                TBNumerpassport.Text = "Номер Паспорта";
            }
        }

        private void TBNumerpassport_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TBNumerpassport.Text == "Номер Паспорта")
            {
                TBNumerpassport.Text = "";
            }
        }

        private void TBDeschotel_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TBDeschotel.Text == "Описание")
            {
                TBDeschotel.Text = "";
            }
        }

        private void TBDeschotel_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TBDeschotel.Text == "")
            {
                TBDeschotel.Text = "Описание";
            }
        }

        private void dateTimePickerDateOfBirth_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DateTime.Today < DateTime.Parse(dateTimePickerDateOfBirth.SelectedDate.Value.ToString()))
            {
                MessageBox.Show("Вы не могли родиться в будущем", "Ошибка");
                dateTimePickerDateOfBirth.SelectedDate = DateTime.Today;
            }
        }
    }
}
