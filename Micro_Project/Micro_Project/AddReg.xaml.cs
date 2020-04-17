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

namespace Micro_Project.Migrations
{
    /// <summary>
    /// Логика взаимодействия для AddReg.xaml
    /// </summary>
    public partial class AddReg : Window
    {
        MyDbContext db = new MyDbContext();
        MainWindow _owner;
        public AddReg(MainWindow owner)
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
        //Client --------------------------------------------- DATA GRID VIEW ------------------------------------------------ //


        //HotelRoom --------------------------------------------- DATA GRID VIEW ------------------------------------------------ //
        internal void Nomer()
        {


            //Подключение к базе данных MicroSoft Sql server
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
        //HotelRoom --------------------------------------------- DATA GRID VIEW ------------------------------------------------ //


        private void BAddNomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var context = new MyDbContext())
                {

                    dynamic clientget = DataGridViewClient.SelectedItem;
                    dynamic nomerget = DataGridViewNomer.SelectedItem;

                    int getclientids = Convert.ToInt32(clientget.Id);
                    int getnomerids = Convert.ToInt32(nomerget.Id);

                    var getclientid = context.Clients.Where(x => x.idClient == getclientids).Select(x => x.idClient).First();

                    var getnomerid = context.HotelRooms.Where(x => x.idFlat == getnomerids).Select(x => x.idFlat).First();

                    var query = context.RegHotels.Where(x => x.idClient == getclientid && x.idFlat == getnomerid);

                    var reghotel = new RegHotel()
                    {
                        idClient = getclientids,
                        idFlat = getnomerid,
                        startLive = dateTimePickerStart.SelectedDate.Value,
                        endLive = dateTimePickerEnd.SelectedDate.Value,
                        desclive = CBDesclive.SelectedItem.ToString(),
                    };


                    if (query.ToList().Count > 0)
                    {
                        MessageBox.Show("Такое изделие и материал уже есть");
                    }

                    else
                    {
                        context.RegHotels.Add(reghotel);
                        context.SaveChanges();
                        db.SaveChanges();
                        MessageBox.Show("Добавлено");
                        this.Close();
                    }

                }
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException error)
            {
                MessageBox.Show("Выберите данные в таблице", "ошибка");
            }
            catch (System.FormatException error)
            {
                MessageBox.Show("Укажите вес", "ошибка");
            }
        }

        public void addComboBox()
        {
            CBDesclive.Items.Add("Оплачено");
            CBDesclive.Items.Add("Не оплачено");
        }

    }
}
