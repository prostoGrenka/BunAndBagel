using BunAndBagel.ApplicationData;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Логика взаимодействия для PageEdit.xaml
    /// </summary>
    public partial class PageEdit : Page
    {
        private ProductBunAndBagel _editgoods = new ProductBunAndBagel();
        public PageEdit(ProductBunAndBagel selectedP)
        {
            InitializeComponent();


            if (selectedP != null)
            {
                _editgoods = selectedP;
            }
            //categoryCombo.ItemsSource = selectedP.categoryCombo;


            DataContext = _editgoods;
            categoryCombo.ItemsSource = BunAndBagelEntities.GetContext().Category.Select(x => x.Category1).ToList();
            kindCombo.ItemsSource = BunAndBagelEntities.GetContext().KindBunAndBagel.Select(x => x.Kind).ToList();
            doughCombo.ItemsSource = BunAndBagelEntities.GetContext().KindDough.Select(x => x.KindDough1).ToList();
            fillingCombo.ItemsSource = BunAndBagelEntities.GetContext().Filling.Select(x => x.Filling1).ToList();
            cookCombo.ItemsSource = BunAndBagelEntities.GetContext().Cook.Select(x => x.Name).ToList();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrEmpty(_editgoods.Name))
            {
                errors.AppendLine("Введите название");
            }
            else if (categoryCombo.Text == "")
            {
                MessageBox.Show("Введите значение 'Категория'!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error); return;
            }
            if (kindCombo.Text == "")
            {
                MessageBox.Show("Введите значение 'Вид выпечки'!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error); return;
            }
            else if (doughCombo.Text == "")
            {
                MessageBox.Show("Введите значение 'Вид теста'!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error); return;
            }
            else if (fillingCombo.Text == "")
            {
                MessageBox.Show("Введите значение 'Начинка'!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error); return;
            }   
            else if (cookCombo.Text == "")
            {
                MessageBox.Show("Введите значение 'Повар'!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error); return;
            }       
            else if (priceBox.Text == "")
            {
                MessageBox.Show("Введите значение 'Цена'!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error); return;
            }    
            else if (weightBox.Text == "")
            {
                MessageBox.Show("Введите значение 'Вес'!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error); return;
            }      

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_editgoods.Id == 0)
            {
                BunAndBagelEntities.GetContext().ProductBunAndBagel.Add(_editgoods);
            }
            try
            {
                BunAndBagelEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные успешно изменены!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            AppFrame.FrmMain.Navigate(new Main((sender as Button).DataContext as User));

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.FrmMain.Navigate(new Main((sender as Button).DataContext as User));
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            categoryCombo.SelectedIndex = -1;
            kindCombo.SelectedIndex = -1;
            doughCombo.SelectedIndex = -1;
            fillingCombo.SelectedIndex = -1;
            cookCombo.SelectedIndex = -1;
            nameBox.Clear();
            priceBox.Clear();
            weightBox.Clear();
            quantityBox.Clear();
            photoBox.Clear();
        }

        private void textBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key < Key.D0 || e.Key > Key.D9) && e.Key != Key.Back)
            {
                e.Handled = true;
            }
        }
    }
}
