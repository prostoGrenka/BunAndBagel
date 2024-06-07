using BunAndBagel.ApplicationData;
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
using System.Xml.Linq;

namespace BunAndBagel.PageApplication.AdminPage
{
    /// <summary>
    /// Логика взаимодействия для PageUserAdd.xaml
    /// </summary>
    public partial class PageUserAdd : Page
    {
        private User _editgoods = new User();
        public PageUserAdd()
        {
            InitializeComponent();
            roleCombo.ItemsSource = BunAndBagelEntities.GetContext().Role.Select(x => x.Role1).ToList();

        }
        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            if (AppConnect.modelOdb.User.Count(x => x.Login == txbLogin.Text) > 0)
            {
                MessageBox.Show("Пользователь с таки логином уже есть!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            try
            {
                User user = new User()
                {
                    Login = txbLogin.Text,
                    Name = txbName.Text,
                    Lastname = txbLastName.Text,
                    Password = txbLogin.Text,
                    Email = txbEmail.Text,
                    Phone = txbNumberPhone.Text,
                    Adrees = txbAdress.Text,
                    Id_Role = roleCombo.SelectedIndex+1,
                };
                AppConnect.modelOdb.User.Add(user);
                AppConnect.modelOdb.SaveChanges();
                MessageBox.Show("данные добавлены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                AppFrame.FrmMain.Navigate(new Main((sender as Button).DataContext as User));

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
                psbPass.Background = Brushes.Red;
            }
            else
            {
                RegBtn.IsEnabled = true;
                psbPass.Background = Brushes.LightGreen;
                psbPass.Background = Brushes.Green;
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.FrmMain.Navigate(new PageInfoUsers());
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            roleCombo.SelectedIndex = -1;
            txbLogin.Clear();
            txbName.Clear();
            txbLastName.Clear();
            txbLogin.Clear();
            txbEmail.Clear();
            txbNumberPhone.Clear();
            txbAdress.Clear();
        }
    }
}

