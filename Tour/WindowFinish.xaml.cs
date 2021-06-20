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
    /// Логика взаимодействия для WindowFinish.xaml
    /// </summary>
    public partial class WindowFinish : Window
    {
        Entities4 data = new Entities4();
        public WindowFinish(Passenger pas, int id)
        {
            InitializeComponent();
            Passenger edit = data.Passenger.Where(x => x.ID == pas.ID).SingleOrDefault();
            edit.ID_Ticket = id;
            data.SaveChanges();
            MessageBox.Show("Успешно куплено", "");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            Close();
        }
    }
}
