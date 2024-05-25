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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BunAndBagel.ApplicationData;


namespace BunAndBagel.PageApplication
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {

        Random rnd = new Random();
        public Registration()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.FrmMain.GoBack();
        }

        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            if (AppConnect.modelOdb.User.Count(x => x.Login == txbLogin.Text) > 0)
            {
                MessageBox.Show("Пользователь с таким логином уже есть!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            try
            {
                User userObj = new User()
                {
                    Login = txbLogin.Text,
                    Name = txbName.Text,
                    Lastname = txbLastName.Text,
                    Password = txbLogin.Text,
                    Email = txbEmail.Text,
                    Phone = txbNumberPhone.Text,
                    Adrees = txbAdress.Text,
                    Id_Role = 2,
                };

                AppConnect.modelOdb.User.Add(userObj);
                AppConnect.modelOdb.SaveChanges();
                MessageBox.Show("Данные успешно добавлены!",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch
            {
                MessageBox.Show("Ошибка добавления данных!",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PasswordBox_PasswordChange(object sender, RoutedEventArgs e)
        {
            if (psbPass.Password != txbPass.Text)
            {
                RegBtn.IsEnabled = false;
                psbPass.Background = Brushes.LightCoral;
                psbPass.BorderBrush = Brushes.Red;
            }
            else
            {
                RegBtn.IsEnabled = true;
                psbPass.Background = Brushes.LightGreen;
                psbPass.BorderBrush = Brushes.Green;
            }
        }
    }
}
