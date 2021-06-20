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
    /// Логика взаимодействия для WindowFlight.xaml
    /// </summary>
    public partial class WindowFlight : Window
    {
        Entities4 data = new Entities4();
        public WindowFlight()
        {
            InitializeComponent();
            gridFlights.ItemsSource = data.Ticket.ToList();
            List<Ticket> date = data.Ticket.ToList();
            var orderticket = from b in date
                              orderby b.Departure_Date
                              select b;
            cmbDate.ItemsSource = orderticket;
            List<Ticket> depart = data.Ticket.ToList();
            var orderdep = from b in depart
                           orderby b.Departure_Point
                              select b;
            cmbDeparture.ItemsSource = orderdep;
            List<Ticket> arriv = data.Ticket.ToList();
            var orderarriv = from b in arriv
                             orderby b.Arrival_Point
                              select b;
            cmbArrival.ItemsSource = orderarriv;
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            if((cmbDeparture.SelectedIndex == -1) && (cmbArrival.SelectedIndex == -1))
            {
                MessageBox.Show("Не выбраны все поля", "Ошибка");
            }
            else
            {
                try
                {
                    Ticket dep = cmbDeparture.SelectedValue as Ticket;
                    Ticket arrival = cmbArrival.SelectedValue as Ticket;
                    gridFlights.ItemsSource = null;
                    gridFlights.ItemsSource = data.Ticket.Where(x => x.Departure_Point == dep.Departure_Point).Where(x => x.Arrival_Point == arrival.Arrival_Point).ToList();
                }
                catch
                {
                    MessageBox.Show("Не выбраны все поля", "Ошибка");
                }
               
            }
        }

        private void cmbDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Ticket dateCurrent = cmbDate.SelectedValue as Ticket;
            gridFlights.ItemsSource = null;
            gridFlights.ItemsSource = data.Ticket.Where(x => x.Departure_Date == dateCurrent.Departure_Date).ToList();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            Close();
        }

        private void Buy_Click(object sender, RoutedEventArgs e)
        {
            Ticket ticket = gridFlights.SelectedValue as Ticket;
            WindowUser wu = new WindowUser(ticket);
            wu.Show();
            Close();
        }
    }
}
