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
    /// Логика взаимодействия для EditReg.xaml
    /// </summary>
    public partial class EditReg : Window
    {

        internal int clientid;
        internal int nomerid;
        internal int regid;

        MyDbContext db = new MyDbContext();
        MainWindow _owner;

        public EditReg(MainWindow owner)
        {
            InitializeComponent();
            addComboBox();
            Client();
            Nomer();
            _owner = owner;
            this.Closed += new EventHandler(AddClient_FormClosing);
        }
        
        private void AddClient_FormClosing(object sender, EventArgs e)
        {
            _owner.RefreshAddClient();
        }

        internal void Client()
        {
            using (var context = new MyDbContext())
            {
                //Запрос для заполнения частично dataGridView без заранее созданых колонок не получиться
                var res = from x in context.Clients
                          select new
                          {
                              Id = x.idClient,
                              Name = x.name,
                              Surname = x.surname,
                              Lastname = x.lastname,
                              dateOfBirth = x.dataOfBirth,
                              seriapassport = x.seriaPassport,
                              nomerpassport = x.numberPassport,
                              countlive = x.countDaysLive
                          };

                DataGridViewClient.ItemsSource = res.ToList();

            }
        }

        internal void Nomer()
        {
            using (var context = new MyDbContext())
            {
                //Запрос для заполнения частично dataGridView без заранее созданых колонок не получиться
                var res = from x in context.HotelRooms
                          select new
                          {
                              Id = x.idFlat,
                              Nomer = x.nomer,
                              CountPeople = x.countPeople,
                              Category = x.category,
                              Payment = x.payment,
                          };

                DataGridViewNomer.ItemsSource = res.ToList();

            }
        }

        private void BAddNomer_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new MyDbContext())
            {
                RegHotel regHotel = db.RegHotels.Find(regid);

                int getclientids = 0;
                int getnomerids = 0;

                if (DataGridViewClient.SelectedItem != null)
                {
                    dynamic clientget = DataGridViewClient.SelectedItem;
                    getclientids = Convert.ToInt32(clientget.Id);
                }
                else
                {
                    getclientids = regHotel.idClient;
                }

                if (DataGridViewNomer.SelectedItem != null)
                {
                    dynamic nomerget = DataGridViewNomer.SelectedItem;
                    getnomerids = Convert.ToInt32(nomerget.Id);
                }
                else 
                {
                    getnomerids = regHotel.idFlat;
                }


                Client clients = db.Clients.Find(getclientids); // добавить GRidView Selected сверху для выбора 
                HotelRoom hotelRooms = db.HotelRooms.Find(getnomerids);
                

                var querygetidClient = context.RegHotels.Where(x => x.idClient == getclientids).Select(x => x.idClient).First();

                Client client = db.Clients.Find(querygetidClient);
                {
                    client.countDaysLive = client.countDaysLive - (regHotel.endLive - regHotel.startLive).Days;
                }

                var query = context.RegHotels.Where(x => x.idFlat == nomerid && (dateTimePickerStart.SelectedDate.Value >= x.startLive && x.endLive >= dateTimePickerStart.SelectedDate.Value || dateTimePickerEnd.SelectedDate.Value >= x.startLive && x.endLive >= dateTimePickerEnd.SelectedDate.Value));
                //Селект из data grid view на номер id
                var queryPerson = context.RegHotels.Where(x => x.idFlat == nomerid && x.idClient == clientid && (dateTimePickerStart.SelectedDate.Value >= x.startLive && x.endLive >= dateTimePickerStart.SelectedDate.Value || dateTimePickerEnd.SelectedDate.Value >= x.startLive && x.endLive >= dateTimePickerEnd.SelectedDate.Value));

                regHotel.desclive = CBDesclive.SelectedItem.ToString();
                regHotel.startLive = dateTimePickerStart.SelectedDate.Value;
                regHotel.endLive = dateTimePickerEnd.SelectedDate.Value;
                regHotel.idClient = clientid;
                regHotel.idFlat = nomerid;

                client = db.Clients.Find(clientid);
                {
                    client.countDaysLive = client.countDaysLive + (dateTimePickerEnd.SelectedDate.Value - dateTimePickerStart.SelectedDate.Value).Days;
                }

                if (queryPerson.ToList().Count > 0)
                {
                    db.SaveChanges();
                    MessageBox.Show("Объект изменён");
                    this.Close();
                }

                else if (query.ToList().Count > 0)
                {
                    MessageBox.Show("Это дата уже занята");
                }

                else
                {
                    db.SaveChanges();
                    MessageBox.Show("Объект изменён");
                    this.Close();
                }
            }

        }

        public void addComboBox()
        {
            CBDesclive.Items.Add("Оплачено");
            CBDesclive.Items.Add("Не оплачено");
        }
    }
}
