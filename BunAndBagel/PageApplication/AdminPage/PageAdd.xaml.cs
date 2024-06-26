﻿using BunAndBagel.ApplicationData;
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
    /// Логика взаимодействия для PageAdd.xaml
    /// </summary>
    public partial class PageAdd : Page
    {
        public PageAdd()
        {
            InitializeComponent();

            categoryCombo.ItemsSource = BunAndBagelEntities.GetContext().Category.Select(x => x.Category1).ToList();
            kindCombo.ItemsSource = BunAndBagelEntities.GetContext().KindBunAndBagel.Select(x => x.Kind).ToList();
            doughCombo.ItemsSource = BunAndBagelEntities.GetContext().KindDough.Select(x => x.KindDough1).ToList();
            fillingCombo.ItemsSource = BunAndBagelEntities.GetContext().Filling.Select(x => x.Filling1).ToList();
            cookCombo.ItemsSource = BunAndBagelEntities.GetContext().Cook.Select(x => x.Name).ToList();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               
                int category = categoryCombo.SelectedIndex + 1;
                int kind = kindCombo.SelectedIndex + 1;
                int dough = doughCombo.SelectedIndex + 1;
                int filling = fillingCombo.SelectedIndex + 1;
                int cook = cookCombo.SelectedIndex + 1;
                string name = nameBox.Text;
                int price = Convert.ToInt32(priceBox.Text);
                int weight = Convert.ToInt32(weightBox.Text);
                int quantity = Convert.ToInt32(quantityBox.Text);
                string photo = photoBox.Text;

                try
                {

                    ProductBunAndBagel goods = new ProductBunAndBagel()
                    {
                        Id_Category = category,
                        Id_Kind = kind,
                        Id_Dough = dough,
                        Id_Filling = filling,
                        Id_Cook = cook,
                        Name = name,
                        Price = price,
                        Weight = weight,
                        Quantity = quantity,
                        Photo = photo,


                    };
                    AppConnect.modelOdb.ProductBunAndBagel.Add(goods);
                    AppConnect.modelOdb.SaveChanges();
                    MessageBox.Show("данные добавлены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    AppFrame.FrmMain.Navigate(new Main((sender as Button).DataContext as User));
                }
                catch
                {
                    MessageBox.Show("Ошибка при добавлении данных!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                AppConnect.modelOdb.SaveChanges();
            }
            catch
            {
                MessageBox.Show("Ошибка при добавлении данных! Все поля должны быть заполнены", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            AppConnect.modelOdb.SaveChanges();

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
