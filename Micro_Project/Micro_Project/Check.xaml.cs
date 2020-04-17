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
    /// Логика взаимодействия для Check.xaml
    /// </summary>
    public partial class Check : Window
    {
        internal int id;
        MyDbContext db = new MyDbContext();
        MainWindow _owner;
        public Check(MainWindow owner)
        {
            InitializeComponent();
            _owner = owner;
            this.Closed += new EventHandler(AddClient_FormClosing);
        }

        private void AddClient_FormClosing(object sender, EventArgs e)
        {
            _owner.RefreshAddClient();  
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (id != null)
            {
                RegHotel regHotel = db.RegHotels.Find(id);
                regHotel.desclive = "Оплачено";
                db.SaveChanges();
                this.Close();
            }
            else 
            {
                MessageBox.Show("Не открыта таблица регистрация или не выбран пользователь");
            }
        }
    }
}
