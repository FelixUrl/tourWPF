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
    /// Логика взаимодействия для WindowEditFlights.xaml
    /// </summary>
    public partial class WindowEditFlights : Window
    {
        Entities4 data = new Entities4();
        Ticket tick;
        public WindowEditFlights(Ticket ticket)
        {
            InitializeComponent();
            tick = ticket;
            txtDeparture_Point.Text = ticket.Departure_Point;
            date_DepartureDate.SelectedDate = ticket.Departure_Date;
            txtArrival_Point.Text = ticket.Arrival_Point;
            date_ArrivalDate.SelectedDate = ticket.Arrival_Date;
            txtNumber.Text = Convert.ToString(ticket.Number);
            txtPrice.Text = Convert.ToString(ticket.Price);
            txtCount.Text = Convert.ToString(ticket.Count);
            List<Company> comp = data.Company.ToList();
            cmbCompany.ItemsSource = comp;
            cmbCompany.SelectedIndex = comp.FindIndex(x => x.ID == ticket.ID_Company);
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
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
                                Ticket edit = data.Ticket.Where(x => x.ID == tick.ID).SingleOrDefault();
                                edit.Departure_Point = txtDeparture_Point.Text;
                                edit.Departure_Date = date_DepartureDate.SelectedDate;
                                edit.Arrival_Point = txtArrival_Point.Text;
                                edit.Arrival_Date = date_ArrivalDate.SelectedDate;
                                edit.Number = Convert.ToInt32(txtNumber.Text);
                                edit.Price = Convert.ToDecimal(txtPrice.Text);
                                edit.Count = Convert.ToInt32(txtCount.Text);
                                edit.ID_Company = (cmbCompany.SelectedValue as Company).ID;
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
