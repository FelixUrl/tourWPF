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
    /// Логика взаимодействия для WindowUser.xaml
    /// </summary>
    public partial class WindowUser : Window
    {
        Entities4 data = new Entities4();
        Ticket tick;
        public WindowUser(Ticket ticket)
        {
            InitializeComponent();
            tick = ticket;
            txbInfoPass.Text = $"{ticket.Departure_Point} - {ticket.Arrival_Point} №{ticket.Number}";
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var check = (from b in data.Passenger
                         where b.ID_Ticket == tick.ID && b.Name == txtName.Text && b.Surname == txtSurname.Text && b.Birthday == dateBirth.SelectedDate && b.Telephone == txtTelephone.Text && b.NumPassport == txtPassport.Text
                         select b).SingleOrDefault();
            if(check == null)
            {
                if ((txtName.Text == "") && (txtPassport.Text == "") && (dateBirth.SelectedDate == null) && (txtTelephone.Text == "") && (txtPassport.Text == ""))
                {
                    MessageBox.Show("Не заполены все поля", "Ошибка");
                }
                else
                {
                    if (txtPassport.Text.Length < 10)
                    {
                        MessageBox.Show("Неверный паспорт", "Ошибка");
                    }
                    else
                    {
                        if (txtTelephone.Text.Length != 11)
                        {
                            MessageBox.Show("Неверный номер телефона", "Ошибка");
                        }
                        else
                        {
                            Passenger pas = new Passenger
                            {
                                Name = txtName.Text,
                                Surname = txtSurname.Text,
                                Birthday = dateBirth.SelectedDate,
                                Telephone = txtTelephone.Text,
                                NumPassport = txtPassport.Text
                            };
                            data.Passenger.Add(pas);
                            data.SaveChanges();
                            WindowFinish wf = new WindowFinish(pas, tick.ID);
                            wf.Show();
                            Close();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Дубликат записи", "Ошибка");
            }
        }

        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            WindowFlight wf = new WindowFlight();
            wf.Show();
            Close();
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
    }
}
