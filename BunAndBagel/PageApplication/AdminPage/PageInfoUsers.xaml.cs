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

namespace BunAndBagel.PageApplication.AdminPage
{
    /// <summary>
    /// Логика взаимодействия для PageInfoUsers.xaml
    /// </summary>
    public partial class PageInfoUsers : Page
    {
        public PageInfoUsers()
        {
            InitializeComponent();
            List<User> products = AppConnect.modelOdb.User.ToList();
            var currentProduct = BunAndBagelEntities.GetContext().User.ToList();
            ListUsers.ItemsSource = currentProduct;
        }

        private void btnAddUsers_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.FrmMain.Navigate(new PageUserAdd());
        }
        List<User> users;
        public void Downloads()
        {
            users = AppConnect.modelOdb.User.ToList();

            if (users.Count > 0)
            {
                tbCounter.Text = "Найдено " + users.Count + " товаров";
            }
            else
            {
                tbCounter.Text = "Ничего не найдено";
            }
            ListUsers.ItemsSource = users;
            comboSort.Items.Add("По вохврастанию товаров на складе");
            comboSort.Items.Add("По уменьшению товаров на складе");
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedUsers = ListUsers.SelectedItems.Cast<User>().ToList();
            List<User> users = AppConnect.modelOdb.User.ToList();
            var userall = users;
            if (selectedUsers != null)
            {
                if (MessageBox.Show("Вы точно хотите удалить выбранный товар?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        BunAndBagelEntities.GetContext().User.RemoveRange(selectedUsers);
                        BunAndBagelEntities.GetContext().SaveChanges();
                        MessageBox.Show("Данные удалены");
                        ListUsers.ItemsSource = BunAndBagelEntities.GetContext().ProductBunAndBagel.ToList();
                        this.users = AppConnect.modelOdb.User.ToList();

                        if (this.users.Count > 0)
                        {
                            tbCounter.Text = "Найдено " + this.users.Count + " товаров";
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
            else
            {
                MessageBox.Show("Вы ничего не выбрали", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void ListOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.FrmMain.Navigate(new Main());
        }

        private void btnEditUser_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.FrmMain.Navigate(new PageUserEdit((sender as Button).DataContext as User));
        }
    }
}
