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
    /// Логика взаимодействия для WindowAdmin.xaml
    /// </summary>
    public partial class WindowAdmin : Window
    {
        Entities4 data = new Entities4();
        public WindowAdmin()
        {
            InitializeComponent();
            gridFlights.ItemsSource = data.Ticket.ToList();
            List<Ticket> date = data.Ticket.ToList();
            var orderticket = from b in date
                              orderby b.Departure_Date
                              select b;
            cmbDate.ItemsSource = orderticket;
        }

        private void cmbDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Ticket dateCurrent = cmbDate.SelectedValue as Ticket;
            gridFlights.ItemsSource = null;
            gridFlights.ItemsSource = data.Ticket.Where(x => x.Departure_Date == dateCurrent.Departure_Date).ToList();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            WindowAddFlight waf = new WindowAddFlight();
            waf.Show();
            Close();
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            switch (gridFlights.SelectedValue)
            {
                case Ticket ticket:
                    data.Ticket.Remove(ticket);
                    data.Database.ExecuteSqlCommand("DELETE FROM Passenger WHERE ID_Ticket = " + ticket.ID);
                    data.SaveChanges();
                    gridFlights.ItemsSource = null;
                    gridFlights.ItemsSource = data.Ticket.ToList();
                    MessageBox.Show("Успешно удалено!", "");
                    break;
                default:
                    MessageBox.Show("Вы собирались удалить пустоту", "Ошибка");
                    break;
            }
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Ticket ticket = gridFlights.SelectedValue as Ticket;
            WindowEditFlights wef = new WindowEditFlights(ticket);
            wef.Show();
            Close();
        }
        private void Passenger_Click(object sender, RoutedEventArgs e)
        {
            Ticket ticket = gridFlights.SelectedValue as Ticket;
            WindowPassengerFlights wpf = new WindowPassengerFlights(ticket);
            wpf.Show();
            Close();
        }

        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            Close();
        }
    }
}
