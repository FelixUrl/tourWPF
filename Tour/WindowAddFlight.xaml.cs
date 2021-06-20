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

namespace Tour
{
    /// <summary>
    /// Логика взаимодействия для WindowAddFlight.xaml
    /// </summary>
    public partial class WindowAddFlight : Window
    {
        Entities4 data = new Entities4();
        public WindowAddFlight()
        {
            InitializeComponent();
            cmbCompany.ItemsSource = data.Company.ToList();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            int number = Convert.ToInt32(txtNumber.Text);
            var check = (from b in data.Ticket
                         where b.Departure_Date == date_DepartureDate.SelectedDate && b.Departure_Point == txtDeparture_Point.Text && b.Arrival_Date == date_ArrivalDate.SelectedDate && b.Arrival_Point == txtArrival_Point.Text && b.Number == number
                         select b).SingleOrDefault();
            if(check==null)
            {
                if ((txtDeparture_Point.Text == "") && (date_ArrivalDate.SelectedDate == null) && (txtArrival_Point.Text == "") && (date_ArrivalDate.SelectedDate == null) && (cmbCompany.SelectedIndex == -1))
                {
                    MessageBox.Show("Вы не заполнили все поля", "Ошибка");
                }
                else
                {
                    if ((txtNumber.Text == "") && (txtPrice.Text == "") && (txtCount.Text == ""))
                    {
                        MessageBox.Show("Вы не заполнили все поля", "Ошибка");
                    }
                    else
                    {
                        if ((txtPrice.Text.Length < 11) && (txtPrice.Text.Length > 0))
                        {
                            if (date_DepartureDate.SelectedDate < date_ArrivalDate.SelectedDate)
                            {
                                if (Convert.ToInt32(txtCount.Text) > 0)
                                {
                                    data.Ticket.Add(new Ticket
                                    {
                                        Departure_Point = txtDeparture_Point.Text,
                                        Departure_Date = date_DepartureDate.SelectedDate,
                                        Arrival_Point = txtArrival_Point.Text,
                                        Arrival_Date = date_ArrivalDate.SelectedDate,
                                        Number = Convert.ToInt32(txtNumber.Text),
                                        Price = Convert.ToDecimal(txtPrice.Text),
                                        Count = Convert.ToInt32(txtCount.Text),
                                        ID_Company = (cmbCompany.SelectedValue as Company).ID
                                    });
                                    data.SaveChanges();
                                    WindowAdmin wa = new WindowAdmin();
                                    wa.Show();
                                    Close();
                                }
                                else
                                {
                                    MessageBox.Show("Кол-во должно быть положительное", "Ошибка");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Не правильные даты", "Ошибка");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Цена не может быть больше 11 симв.", "Ошибка");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Уже есть точно такая же запись", "Ошибка");
            }
        }

        private void numberPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char s = Convert.ToChar(e.Text);
            if ((!(s < '0' || s > '9')) && (s > '!') || (s < ')')) e.Handled = true;
        }

        private void textPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char s = Convert.ToChar(e.Text);
            if (!((s < 'A' || s > 'z') && (s < 'А' || s > 'я'))) e.Handled = true;
        }

        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            WindowAdmin wa = new WindowAdmin();
            wa.Show();
            Close();
        }
    }
}
