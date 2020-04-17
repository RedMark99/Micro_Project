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
    /// Логика взаимодействия для EditClient.xaml
    /// </summary>
    public partial class EditClient : Window
    {

        internal int clientid;  
        MyDbContext db = new MyDbContext();
        MainWindow _owner;

        public EditClient(MainWindow owner)
        {
            InitializeComponent();
            _owner = owner;
            this.Closed += new EventHandler(AddClient_FormClosing);
        }

        private void AddClient_FormClosing(object sender, EventArgs e)
        {
            _owner.RefreshAddClient();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new MyDbContext())
            {

                Client client = db.Clients.Find(clientid);

                var query = context.Clients.Where(x => x.login == TBLogin.Text || x.numberPassport == TBNumerpassport.Text);

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////Сделать проверку////////////////////////////////////////////////
                if (query.ToList().Count < 1)
                {
                    client.name = TBName.Text;
                    client.surname = TBSurname.Text;
                    client.lastname = TBLastname.Text;
                    client.login = TBLogin.Text;
                    client.password = TBPassword.Text;
                    client.seriaPassport = TBSeriapassport.Text;
                    client.numberPassport = TBNumerpassport.Text;
                    client.dataOfBirth = DateTime.Parse(dateTimePickerDateOfBirth.SelectedDate.Value.ToString());

                    db.SaveChanges();
                    MessageBox.Show("Объект изменён");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Логин или Номер Пасспорта такой уже есть в базе данных");
                }
            }

        }
    }
}
