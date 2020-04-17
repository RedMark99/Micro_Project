using Micro_Project.Migrations;
using Micro_Project.MyDbClass;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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

namespace Micro_Project
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MyDbContext db = new MyDbContext();

        public string id;

        //Переменые для подсветки

        bool ClientLight = false;
        bool NomerLight = false;
        bool RegHotelLight = false;
        public MainWindow()
        {
            InitializeComponent();
        }



        public void RefreshAddClient()
        {
            if (ClientLight == true)
            {
                Client();
            }
            else if (NomerLight == true)
            {
                Nomer();
            }
            else if (RegHotelLight == true)
            {
                RegHotel();
            }
        }
        public void light()
        {
            if (ClientLight == true)
            {
                BClient.Background = Brushes.Orange; // orange
                BNomer.Background = Brushes.Transparent; // gray
                BReghotel.Background = Brushes.Transparent;
            }
            else if (NomerLight == true)
            {
                BNomer.Background = Brushes.Orange;
                BClient.Background = Brushes.Transparent;
                BReghotel.Background = Brushes.Transparent;
            }
            else if (RegHotelLight == true)
            {
                BReghotel.Background = Brushes.Orange;
                BClient.Background = Brushes.Transparent;
                BNomer.Background = Brushes.Transparent;
            }
        }

        //Client --------------------------------------------- DATA GRID VIEW ------------------------------------------------ //
        internal void Client()
        {

            //Очищение dateGridView вместе с шапкой
            dataGridView.Columns.Clear();
            dataGridView.SelectAll();


            //Подключение к базе данных MicroSoft Sql server
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

                dataGridView.ItemsSource = res.ToList();

            }
        }
        //Client --------------------------------------------- DATA GRID VIEW ------------------------------------------------ //


        //HotelRoom --------------------------------------------- DATA GRID VIEW ------------------------------------------------ //
        internal void Nomer()
        {

            //Очищение dateGridView вместе с шапкой

            dataGridView.SelectAll();
            dataGridView.Columns.Clear();


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

                dataGridView.ItemsSource = res.ToList();

            }
        }
        //HotelRoom --------------------------------------------- DATA GRID VIEW ------------------------------------------------ //

        //RegHotel --------------------------------------------- DATA GRID VIEW ------------------------------------------------ //
        public void RegHotel()
        {
            //Очищение dateGridView вместе с шапкой

            dataGridView.SelectAll();
            dataGridView.Columns.Clear();


            //Подключение к базе данных MicroSoft Sql server
            using (var context = new MyDbContext())
            {
                //Запрос для заполнения частично dataGridView без заранее созданых колонок не получиться
                var res = from x in context.RegHotels
                          from y in context.HotelRooms
                          from z in context.Clients
                          where x.idClient == z.idClient && x.idFlat == y.idFlat
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
        private void BAddnomer_Click(object sender, RoutedEventArgs e)
        {
            AddNomer addNomer = new AddNomer(this);
            addNomer.Show();
        }

        private void BClient_Click(object sender, RoutedEventArgs e)
        {
            //Custom Внешний вид
            ClientLight = true;
            NomerLight = false;
            RegHotelLight = false;
            light();
            //Обновление dateGridView данных
            Client();
        }

        private void BAddclient_Click(object sender, RoutedEventArgs e)
        {
            AddClient addClient = new AddClient(this); // передаём owner в addClient для автомочиского обновление DATAGRIDVIEW с дочерней формы
            addClient.Show();
        }

        private void BNomer_Click(object sender, RoutedEventArgs e)
        {
            //Custom Внешний вид
            ClientLight = false;
            NomerLight = true;
            RegHotelLight = false;
            light();
            //Обновление dateGridView данных
            Nomer();
        }

        private void BReghotel_Click(object sender, RoutedEventArgs e)
        {
            //Custom Внешний вид
            ClientLight = false;
            NomerLight = false;
            RegHotelLight = true;
            light();
            //Обновление dateGridView данных
            RegHotel();
        }

        private void BDelete_Click(object sender, RoutedEventArgs e)
        {
            if (NomerLight == true)
            {
                if (dataGridView.SelectedItem.ToString().Count() > 0)
                {
                    dynamic nomerget = dataGridView.SelectedItem;

                    int id = 0;
                    id = Convert.ToInt32(nomerget.Id);


                    HotelRoom hotelroom = db.HotelRooms.Find(id);
                    db.HotelRooms.Remove(hotelroom);
                    db.SaveChanges();

                    MessageBox.Show("Обьект удален");
                    Nomer();
                }
            }
            else if (ClientLight == true)
            {
                if (dataGridView.SelectedItem.ToString().Count() > 0)
                {
                    dynamic clientget = dataGridView.SelectedItem;

                    int id = 0;
                    id = Convert.ToInt32(clientget.Id);

                    Client client = db.Clients.Find(id);
                    db.Clients.Remove(client);
                    db.SaveChanges();

                    MessageBox.Show("Обьект удален");
                    Client();

                }

            }
            else if (RegHotelLight == true)
            {
                if (dataGridView.SelectedItem.ToString().Count() > 0)
                {
                    dynamic regHotel = dataGridView.SelectedItem;

                    int id = 0;
                    id = Convert.ToInt32(regHotel.Id);

                    RegHotel reghotel = db.RegHotels.Find(id);
                    db.RegHotels.Remove(reghotel);
                    db.SaveChanges();

                    MessageBox.Show("Обьект удален");
                    RegHotel();

                }
            }
        }

        private void dataGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //DataGrid gd = (DataGrid)sender;
            ////DataRowView row_selected = gd.SelectedItem as DataRowView;
            ////if (row_selected != null) 
            ////{
            ////    id = row_selected[1].ToString();
            ////}

            //DataRowView row = dataGridView.SelectedItem as DataRowView;

            //MessageBox.Show(row.Row[0].ToString());

        }

        private void BAddreg_Click(object sender, RoutedEventArgs e)
        {
            AddReg addreg = new AddReg(this);
            addreg.Show();
        }

        private void BEdit_Click(object sender, RoutedEventArgs e)
        {
            try 
            {
                using (var context = new MyDbContext())
                {

                    if (ClientLight == true)
                    {
                        dynamic clients = dataGridView.SelectedItem;

                        int id = 0;
                        id = Convert.ToInt32(clients.Id);
                        Client client = db.Clients.Find(id);

                        EditClient editClient = new EditClient(this);

                        DateTime tempDate = DateTime.Parse(client.dataOfBirth.ToShortDateString().ToString());

                        editClient.clientid = id;
                        editClient.TBName.Text = client.name; // что бы поля были видны в свойствах нужно было поменять private на internal
                        editClient.TBSurname.Text = client.surname;
                        editClient.TBLastname.Text = client.lastname;
                        editClient.TBLogin.Text = client.login;
                        editClient.TBPassword.Text = client.password;
                        editClient.TBSeriapassport.Text = client.seriaPassport;
                        editClient.TBNumerpassport.Text = client.numberPassport;
                        editClient.dateTimePickerDateOfBirth.SelectedDate = tempDate;
                        editClient.TBDeschotel.Text = client.deschotel;

                        editClient.Show();
                    }

                    else if (NomerLight == true) 
                    {
                        dynamic nomer = dataGridView.SelectedItem;

                        int id = 0;
                        id = Convert.ToInt32(nomer.Id);
                        HotelRoom hotelRoom = db.HotelRooms.Find(id);

                        EditNomer editNomer = new EditNomer(this);

                        editNomer.nomerid = id;
                        editNomer.TBNomer.Text = hotelRoom.nomer;
                        editNomer.TBCountpeople.Text = hotelRoom.countPeople.ToString();
                        editNomer.CBCateogury.Text = hotelRoom.category;
                        editNomer.TBPayment.Text = hotelRoom.payment.ToString();

                        editNomer.Show();
                    }

                    else if (RegHotelLight == true)
                    {
                        dynamic reg = dataGridView.SelectedItem;

                        int id = 0;
                        id = Convert.ToInt32(reg.Id);

                        RegHotel regHotel = db.RegHotels.Find(id);

                        EditReg editReg = new EditReg(this);

                        editReg.regid = id;
                        editReg.dateTimePickerStart.SelectedDate = regHotel.startLive;
                        editReg.dateTimePickerEnd.SelectedDate = regHotel.endLive;
                        editReg.CBDesclive.Text = regHotel.desclive;
                        editReg.clientid = regHotel.idClient;
                        editReg.nomerid = regHotel.idFlat;

                        editReg.Show();
                    }
                }
                
            }
            catch(System.NullReferenceException error) 
            {

            }
        }

        private void BInfo_Click(object sender, RoutedEventArgs e)
        {
            Info inf = new Info();
            inf.Show();
        }

        private void BPay_Click(object sender, RoutedEventArgs e)
        {
            payForHotel();
        }

        public void payForHotel()
        {
            try 
            {
                dynamic reg = dataGridView.SelectedItem;

                int id = 0;
                id = Convert.ToInt32(reg.Id);


                using (var context = new MyDbContext())
                {
                    RegHotel regHotel = db.RegHotels.Find(id);

                    var query = from x in context.HotelRooms
                                from y in context.RegHotels
                                from z in context.Clients
                                where y.idFlat == regHotel.idFlat && regHotel.idClient == y.idClient && regHotel.startLive == y.startLive && regHotel.endLive == y.endLive && x.idFlat == y.idFlat && z.idClient == y.idClient
                                select new
                                {
                                    money = x.payment,
                                    name = z.name,
                                    surname = z.surname,
                                    lastname = z.lastname,
                                    nomer = y.idFlat,
                                    startDays = y.startLive,
                                    endDays = y.endLive,
                                    count = z.countDaysLive
                                };

                    Check check = new Check(this);

                    foreach (var x in query.ToList())
                    {
                        check.LFIO.Content = x.surname.ToString() + " " + x.name.ToString() + " " + x.lastname.ToString();
                        check.LNomer.Content = x.nomer.ToString();
                        check.LStartlive.Content = x.startDays.ToShortDateString().ToString();
                        check.LEndlive.Content = x.endDays.ToShortDateString().ToString();
                        check.LDays.Content = ((x.endDays - x.startDays).Days + 1).ToString();

                        if (x.count > 20)
                        {
                            check.LSale.Content = "Есть";
                            int countDays = (x.endDays - x.startDays).Days + 1;
                            int summa = x.money * countDays;
                            int procent = (summa / 100) * 15;
                            summa = summa - procent;
                            check.LSumma.Content = summa.ToString();
                        }
                        else
                        {
                            check.LSale.Content = "Отсутствует";
                            int countDays = (x.endDays - x.startDays).Days + 1;
                            int summa = x.money * countDays;
                            check.LSumma.Content = summa.ToString();
                        }
                        check.id = id;
                        check.Show();
                    }
                }
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException error) 
            {
                MessageBox.Show("В таблице регистрация поле не выбрано");
            }
            
        }

    }
}
