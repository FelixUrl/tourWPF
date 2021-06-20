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
    /// Логика взаимодействия для WindowPassengerFlights.xaml
    /// </summary>
    public partial class WindowPassengerFlights : Window
    {
        Entities4 data = new Entities4();
        Ticket tick;
        public WindowPassengerFlights(Ticket ticket)
        {
            tick = ticket;
            InitializeComponent();
            gridPassenger.ItemsSource = data.Passenger.Where(x => x.ID_Ticket == tick.ID).ToList();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            WindowAdmin wa = new WindowAdmin();
            wa.Show();
            Close();
        }
    }
}
