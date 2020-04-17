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
    /// Логика взаимодействия для Info.xaml
    /// </summary>
    public partial class Info : Window
    {
        public Info()
        {
            InitializeComponent();
            AddComboxBox();
        }

        public void AddComboxBox()
        {
            CBCategory.Items.Add("Люкс");
            CBCategory.Items.Add("Полулюкс");
            CBCategory.Items.Add("Обычный");
            CBCategory.Items.Add("Все");
            CBCategory2.Items.Add("Люкс");
            CBCategory2.Items.Add("Полулюкс");
            CBCategory2.Items.Add("Обычный");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                //Подключение к базе данных MicroSoft Sql server
                if (CBCategory.SelectedItem.ToString() != "Все")
                {
                    using (var context = new MyDbContext())
                    {

                        //Запрос для заполнения частично dataGridView без заранее созданых колонок не получиться
                        var query = from x in context.HotelRooms
                                    select new
                                    {
                                        id = x.idFlat,
                                        nomer = x.nomer,
                                        countPople = x.countPeople,
                                        payment = x.payment,
                                        category = x.category
                                    };

                        var res = from x in query
                                  join y in context.RegHotels
                                  on x.id equals y.idFlat
                                  where x.category == CBCategory.SelectedItem.ToString()
                                  group x by x.category into g
                                  select new
                                  {
                                      CategoryName = g.Key,
                                      Count = g.Select(x => x.nomer).Count()
                                  };

                        dataGridView.ItemsSource = res.ToList();
                    }
                }
                else
                {
                    using (var context = new MyDbContext())
                    {


                        //Запрос для заполнения частично dataGridView без заранее созданых колонок не получиться
                        var query = from x in context.HotelRooms
                                    select new
                                    {
                                        id = x.idFlat,
                                        nomer = x.nomer,
                                        countPople = x.countPeople,
                                        payment = x.payment,
                                        category = x.category
                                    };

                        var res = from x in query
                                  join y in context.RegHotels
                                  on x.id equals y.idFlat
                                  group x by x.category into g
                                  select new
                                  {
                                      CategoryName = g.Key,
                                      Count = g.Select(x => x.nomer).Count()
                                  };

                        dataGridView.ItemsSource = res.ToList();



                    }
                };
            }

            catch (System.NullReferenceException error)
            {
                MessageBox.Show("Выберите категорию номера в выпадающем списке", "Ошибка");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CBCategory.SelectedItem.ToString() != "Все")
                {
                    using (var context = new MyDbContext())
                    {
                        //Запрос для заполнения частично dataGridView без заранее созданых колонок не получиться
                        var query = from x in context.HotelRooms
                                    where x.category == CBCategory.SelectedItem.ToString()
                                    select new
                                    {
                                        id = x.idFlat,
                                        nomer = x.nomer,
                                        countPople = x.countPeople,
                                        payment = x.payment,
                                        category = x.category
                                    };

                        var res = from x in query
                                  join y in context.RegHotels
                                  on x.id equals y.idFlat
                                  join z in context.Clients
                                  on y.idClient equals z.idClient
                                  where x.category == CBCategory.SelectedItem.ToString()
                                  select new
                                  {
                                      Name = z.name,
                                      Surname = z.surname,
                                      Lastname = z.lastname,
                                      nomer = x.nomer,
                                      category = x.category,
                                      startlive = y.startLive,
                                      endlive = y.endLive
                                  };

                        dataGridView.ItemsSource = res.ToList();
                    }
                }
                else
                {
                    using (var context = new MyDbContext())
                    {
                        //Запрос для заполнения частично dataGridView без заранее созданых колонок не получиться
                        var query = from x in context.HotelRooms
                                    select new
                                    {
                                        id = x.idFlat,
                                        nomer = x.nomer,
                                        countPople = x.countPeople,
                                        payment = x.payment,
                                        category = x.category
                                    };

                        var res = from x in query
                                  join y in context.RegHotels
                                  on x.id equals y.idFlat
                                  join z in context.Clients
                                  on y.idClient equals z.idClient
                                  select new
                                  {
                                      Name = z.name,
                                      Surname = z.surname,
                                      Lastname = z.lastname,
                                      nomer = x.nomer,
                                      category = x.category,
                                      startlive = y.startLive,
                                      endlive = y.endLive
                                  };

                        dataGridView.ItemsSource = res.ToList();
                    }
                };
            }

            catch (System.NullReferenceException error)
            {
                MessageBox.Show("Выберите категорию номера в выпадающем списке", "Ошибка");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            using (var context = new MyDbContext())
            {
                //Запрос для заполнения частично dataGridView без заранее созданых колонок не получиться

                var query = context.RegHotels.Where(x => dateTimePickerStart.SelectedDate.Value <= x.startLive && x.endLive <= dateTimePickerEnd.SelectedDate.Value);

                List<QueryFirst> data = new List<QueryFirst>();


                var res = from x in query
                          from y in context.Clients
                          from z in context.HotelRooms
                          where x.idClient == y.idClient && z.idFlat == x.idFlat
                          select new
                          {
                              name = y.name,
                              surname = y.surname,
                              lastname = y.lastname,
                              startlive = x.startLive,
                              endlive = x.endLive,
                              nomer = z.nomer,
                              category = z.category,
                              summa = z.payment,
                              desc = x.desclive
                          };

                foreach (var item in res.ToList())
                {
                    int days = (item.endlive - item.startlive).Days + 1;
                    int summa = item.summa * days;
                    string start = item.startlive.ToString();
                    string end = item.endlive.ToString();

                    data.Add( new QueryFirst (item.name, item.surname, item.lastname, item.nomer, item.category, start, end, days.ToString(), item.summa.ToString(), item.desc));
                }

                dataGridView.ItemsSource = data.ToList();
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    //Запрос для заполнения частично dataGridView без заранее созданых колонок не получиться
                    List<QueryFirst> data = new List<QueryFirst>();

                    var query = context.RegHotels.Where(x => dateTimePickerStart.SelectedDate.Value <= x.startLive && x.endLive <= dateTimePickerEnd.SelectedDate.Value);

                    var res = from x in query
                              from y in context.Clients
                              from z in context.HotelRooms
                              where x.idClient == y.idClient && z.idFlat == x.idFlat
                              select new
                              {
                                  name = y.name,
                                  surname = y.surname,
                                  lastname = y.lastname,
                                  startlive = x.startLive,
                                  endlive = x.endLive,
                                  nomer = z.nomer,
                                  category = z.category,
                                  summa = z.payment,
                                  desc = x.desclive
                              };

                    foreach (var item in res.ToList())
                    {
                        int days = (item.endlive - item.startlive).Days + 1;
                        int summa = item.summa * days;
                        string start = item.startlive.ToString();
                        string end = item.endlive.ToString();

                        data.Add(new QueryFirst(item.name, item.surname, item.lastname, item.nomer, item.category, start, end, days.ToString(), item.summa.ToString(), item.desc));
                    }

                    dataGridView.ItemsSource = data.ToList();

                }

            }
            catch (System.NotSupportedException error)
            {
                MessageBox.Show("Выберите категорию номера в выпадающем списке", "Ошибка");
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            try
            {

                using (var context = new MyDbContext())
                {
                    //Запрос для заполнения частично dataGridView без заранее созданых колонок не получиться
                    List<QueryFirst> data = new List<QueryFirst>();

                    var query = context.RegHotels.Where(x => dateTimePickerStart.SelectedDate.Value <= x.startLive && x.endLive <= dateTimePickerEnd.SelectedDate.Value);

                    var query2 = from x in context.HotelRooms
                                 where x.category == CBCategory2.SelectedItem.ToString()
                                 select new
                                 {
                                     idFlat = x.idFlat,
                                     nomer = x.nomer,
                                     countPople = x.countPeople,
                                     payment = x.payment,
                                     category = x.category
                                 };

                    var res = from x in query
                              from y in context.Clients
                              from z in query2
                              where x.idClient == y.idClient && z.idFlat == x.idFlat
                              select new
                              {
                                  name = y.name,
                                  surname = y.surname,
                                  lastname = y.lastname,
                                  startlive = x.startLive,
                                  endlive = x.endLive,
                                  nomer = z.nomer,
                                  category = z.category,
                                  summa = z.payment,
                                  desc = x.desclive
                              };

                    foreach (var item in res.ToList())
                    {
                        int days = (item.endlive - item.startlive).Days + 1;
                        int summa = item.summa * days;
                        string start = item.startlive.ToString();
                        string end = item.endlive.ToString();

                        data.Add(new QueryFirst(item.name, item.surname, item.lastname, item.nomer, item.category, start, end, days.ToString(), item.summa.ToString(), item.desc));
                    }

                    dataGridView.ItemsSource = data.ToList();
                }

            }
            catch (System.NotSupportedException error)
            {
                MessageBox.Show("Выберите категорию номера в выпадающем списке", "Ошибка");
            }
        }
    }
}

