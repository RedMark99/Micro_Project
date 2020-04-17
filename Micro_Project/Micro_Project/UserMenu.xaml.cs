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
    /// Логика взаимодействия для UserMenu.xaml
    /// </summary>
    public partial class UserMenu : Window
    {

        MyDbContext db = new MyDbContext();
        public string login;
        public int idclient;

        public UserMenu()
        {
            InitializeComponent();
            getIdClient();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            freenomer();
        }

        public void freenomer()
        {
            using (var context = new MyDbContext())
            {
                var res = from x in context.RegHotels
                          from y in context.HotelRooms
                          where x.idFlat == y.idFlat && (dateTimePickerStart.SelectedDate.Value >= x.startLive && x.endLive >= dateTimePickerStart.SelectedDate.Value || dateTimePickerEnd.SelectedDate.Value >= x.startLive && x.endLive >= dateTimePickerEnd.SelectedDate.Value)
                          select new
                          {
                              idFlat = x.idFlat,
                              nomer = y.nomer,
                              countPeople = y.countPeople,
                              category = y.category,
                              payment = y.payment
                          };

                var res2 = context.HotelRooms.Select(x => x.idFlat).ToList().Except(res.Select(x => x.idFlat).ToList()).ToList();

                //Подключение к базе данных MicroSoft Sql server

                //Запрос для заполнения частично dataGridView без заранее созданых колонок не получиться
                var query = from x in context.HotelRooms
                            from y in res2
                            where x.idFlat == y
                            select new
                            {
                                Id = x.idFlat,
                                nomer = x.nomer,
                                countpeople = x.countPeople,
                                category = x.category,
                                payment = x.payment
                            };
                //Считываем в datagrid view

                dataGridView.ItemsSource = query.ToList();
            }
        }
        public void getIdClient()
        {
            var query = db.Clients.Where(x => x.login == sendDate.login).Select(x => x.idClient).First();
            idclient = query;
        }


        private void Add1_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new MyDbContext())
            {

                var res = from x in context.Clients
                          where x.login == login
                          select new
                          {
                              Id = x.idClient,
                              Name = x.name,
                              Surname = x.surname,
                              Lastname = x.lastname,
                              dateOfBirth = x.dataOfBirth,
                              countlive = x.countDaysLive,
                              desc = x.deschotel
                          };

                dynamic nomerget = dataGridView.SelectedItem;

                int getnomerids = Convert.ToInt32(nomerget.Id);

                var getnomerid = context.HotelRooms.Where(x => x.idFlat == getnomerids).Select(x => x.idFlat).First();

                var regHotel = new RegHotel()
                {
                    idClient = idclient,
                    idFlat = getnomerid,
                    startLive = dateTimePickerStart.SelectedDate.Value,
                    endLive = dateTimePickerEnd.SelectedDate.Value,
                    desclive = "Не оплачено"
                };

                Client client = db.Clients.Find(idclient);
                {
                    client.countDaysLive = client.countDaysLive + (regHotel.endLive - regHotel.startLive).Days;
                }

                context.RegHotels.Add(regHotel);
                context.SaveChanges();
                db.SaveChanges();
                MessageBox.Show("Вы забронировали номер");
                freenomer();

            }
        }

        public void RegHotel()
        {

            //Подключение к базе данных MicroSoft Sql server
            using (var context = new MyDbContext())
            {
                //Запрос для заполнения частично dataGridView без заранее созданых колонок не получиться
                var res = from x in context.RegHotels
                          from y in context.HotelRooms
                          from z in context.Clients
                          where x.idClient == z.idClient && x.idFlat == y.idFlat && x.idClient == idclient && z.idClient == idclient
                          select new
                          {
                              Id = x.idLive,
                              Nomer = y.nomer,
                              Name = z.name,
                              Surname = z.surname,
                              Lastname = z.lastname,
                              DataStart = x.startLive,
                              DataEnd = x.endLive,
                              Desc = x.desclive
                          };

                dataGridView.ItemsSource = res.ToList();
            }
        }
        //RegHotel --------------------------------------------- DATA GRID VIEW ------------------------------------------------ //

        private void Add3_Click(object sender, RoutedEventArgs e)
        {
            RegHotel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Info info = new Info();
            info.Show();
        }
    }
}
