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
using System.Xml.XPath;
using BunAndBagel.ApplicationData;

namespace BunAndBagel.PageApplication.Authorization
{
    /// <summary>
    /// Логика взаимодействия для Entrance.xaml
    /// </summary>
    public partial class Entrance : Page
    {
        public Entrance()
        {
            InitializeComponent();
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var userObj = AppConnect.modelOdb.User.FirstOrDefault(x => x.Login == txbLogin.Text && x.Password == psbPassword.Password);
                if (userObj == null)
                {
                    MessageBox.Show("Такого пользователя нет!", "Ошибка при авторизации!",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    switch (userObj.Id_Role)
                    {
                        case 1:
							App.Current.Properties["Id"] = userObj.Id;
							MessageBox.Show("Здравствуйте, Администратор " + userObj.Name + "!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                            AppFrame.FrmMain.Navigate(new Main((sender as Button).DataContext as User));
                            break;
                        case 2:
							App.Current.Properties["Id"] = userObj.Id;
							MessageBox.Show("Здравствуйте, Пользователь " + userObj.Name + "!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                            AppFrame.FrmMain.Navigate(new Main((sender as Button).DataContext as User));
                            break;
                        default:
                            MessageBox.Show("Данные не обнаружены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                            break;
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Ошибка " + Ex.Message.ToString() + "Критическая работа приложения!", "Уведомление ", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Registr_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.FrmMain.Navigate(new Registration());
        }
    }
}
