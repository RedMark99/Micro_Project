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
    /// Логика взаимодействия для AddNomer.xaml
    /// </summary>
    public partial class AddNomer : Window
    {

        MainWindow _owner;

        public AddNomer(MainWindow owner)
        {
            InitializeComponent();
            AddComboxBox();
            CBCateogury.Text = "Выберите категорию";
            TBNomer.Text = "Номер";
            TBCountpeople.Text = "Кол-во людей";
            TBPayment.Text = "Цена";
            //CBCateogury.DropDownStyle = ComboBoxStyle.DropDownList;

            _owner = owner;
            this.Closed += new EventHandler(AddClient_FormClosing);
        }

        private void AddClient_FormClosing(object sender, EventArgs e)
        {
            _owner.RefreshAddClient();
        }

        public void addNomer()
        {
            using (var context = new MyDbContext())
            {

                var query = context.HotelRooms.Where(x => x.nomer == TBNomer.Text);

                var nomer = new HotelRoom()
                {
                    nomer = TBNomer.Text,
                    category = CBCateogury.SelectedItem.ToString(),
                    countPeople = Convert.ToInt32(TBCountpeople.Text),
                    payment = Convert.ToInt32(TBPayment.Text)
                };

                if (query.ToList().Count < 1)
                {
                    if (CBCateogury.Text != "Выберите категорию")
                    {
                        context.HotelRooms.Add(nomer);
                        context.SaveChanges();
                        MessageBox.Show("Добавлено");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Выберите категорию", "Ошибка");
                    }
                }

                else if (query.ToList().Count > 0)
                {
                    MessageBox.Show("Такой номер уже существует");
                }

            }
        }

        public void AddComboxBox()
        {
            CBCateogury.Items.Add("Люкс");
            CBCateogury.Items.Add("Полулюкс");
            CBCateogury.Items.Add("Обычный");
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            addNomer();
            

        }

        private void TBNomer_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TBNomer.Text == "Номер")
            {
                TBNomer.Text = "";
            }
        }

        private void TBNomer_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TBNomer.Text == "")
            {
                TBNomer.Text = "Номер";
            }
        }

        private void TBCountpeople_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TBCountpeople.Text == "Кол-во людей")
            {
                TBCountpeople.Text = "";
            }
        }

        private void TBCountpeople_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TBCountpeople.Text == "")
            {
                TBCountpeople.Text = "Кол-во людей";
            }
        }

        private void TBPayment_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TBPayment.Text == "Цена")
            {
                TBPayment.Text = "";
            }
        }

        private void TBPayment_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TBPayment.Text == "")
            {
                TBPayment.Text = "Цена";
            }
        }

        private void CBCateogury_GotFocus(object sender, RoutedEventArgs e)
        {
            if (CBCateogury.Text == "Выберите категорию")
                CBCateogury.Text = "";
        }

        private void CBCateogury_LostFocus(object sender, RoutedEventArgs e)
        {
            if (CBCateogury.Text == "")
                CBCateogury.Text = "Выберите категорию";
        }
    }
}
