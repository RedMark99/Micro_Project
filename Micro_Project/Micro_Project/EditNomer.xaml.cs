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
    /// Логика взаимодействия для EditNomer.xaml
    /// </summary>
    public partial class EditNomer : Window
    {

        internal int nomerid;
        MyDbContext db = new MyDbContext();
        MainWindow _owner;

        public EditNomer(MainWindow owner)
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

                HotelRoom hotelRoom = db.HotelRooms.Find(nomerid);

                var query = context.HotelRooms.Where(x => x.nomer == TBNomer.Text);
                if (query.ToList().Count < 1)
                {
                    hotelRoom.nomer = TBNomer.Text;
                    hotelRoom.category = CBCateogury.SelectedItem.ToString();
                    hotelRoom.countPeople = Convert.ToInt32(TBCountpeople.Text);
                    hotelRoom.payment = Convert.ToInt32(TBPayment.Text);

                    db.SaveChanges();
                    MessageBox.Show("Объект изменён");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Такой номер уже существует");
                }

            }
        }
    }
}
