using Aspose.Pdf.Annotations;
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
using System.Xml.Serialization;

namespace BunAndBagel.PageApplication
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public Main()
        {
            InitializeComponent();
            List<ProductBunAndBagel> products = AppConnect.modelOdb.ProductBunAndBagel.ToList();
            var currentProduct = BunAndBagelEntities.GetContext().ProductBunAndBagel.ToList();
            listProducts.ItemsSource = currentProduct;

            Downloads();
        }
        List<ProductBunAndBagel> productBunAndBagel;
        public void Downloads()
        {
            productBunAndBagel = AppConnect.modelOdb.ProductBunAndBagel.ToList();

            if (productBunAndBagel.Count > 0)
            {
                tbCounter.Text = "Найдено " + productBunAndBagel.Count + " товаров";
            }
            else
            {
                tbCounter.Text = "Ничего не найдено";
            }
            listProducts.ItemsSource = productBunAndBagel; 
            comboSort.Items.Add("По вохврастанию товаров на складе");
            comboSort.Items.Add("По уменьшению товаров на складе");
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedProduct = listProducts.SelectedItems.Cast<ProductBunAndBagel>().ToList();
            List<ProductBunAndBagel> product = AppConnect.modelOdb.ProductBunAndBagel.ToList();
            var productall = product;
            if (selectedProduct != null)
            {
                if (MessageBox.Show("Вы точно хотите удалить выбранный товар?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        BunAndBagelEntities.GetContext().ProductBunAndBagel.RemoveRange(selectedProduct);
                        BunAndBagelEntities.GetContext().SaveChanges();
                        MessageBox.Show("Данные удалены");
                        listProducts.ItemsSource = BunAndBagelEntities.GetContext().ProductBunAndBagel.ToList();
                        productBunAndBagel = AppConnect.modelOdb.ProductBunAndBagel.ToList();

                        if(productBunAndBagel.Count > 0)
                        {
                            tbCounter.Text = "Найдено " + productBunAndBagel.Count + " товаров";
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
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.FrmMain.Navigate(new AdminPage.PageAdd());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            findGoods();
        }
        ProductBunAndBagel[] findGoods()
        {
            List<ProductBunAndBagel> product = AppConnect.modelOdb.ProductBunAndBagel.ToList();
            var productall = product;
            if (TBoxSearch != null)
            {
                product = product.Where(x => x.Name.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            }
            if (comboSort.SelectedIndex > 0)
            {
                switch (comboSort.SelectedIndex)
                {
                    case 0:
                        product = product.OrderBy(x => x.Quantity).ToList<ProductBunAndBagel>();
                        break;
                    case 1:
                        product = product.OrderByDescending(x => x.Quantity).ToList<ProductBunAndBagel>();
                        break;
                }
            }
            if (product.Count > 0)
            {
                tbCounter.Text = "Найдено " + product.Count + " товаров";
            }
            else
            {
                tbCounter.Text = "Ничего не найдено";
            }
            listProducts.ItemsSource = product;
            return product.ToArray();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.FrmMain.Navigate(new AdminPage.PageEdit((sender as Button).DataContext as ProductBunAndBagel));
        }

        private void listProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnInfoUser_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.FrmMain.Navigate(new AdminPage.PageInfoUsers());
        }

        private void comboSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            findGoods();
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            findGoods();
        }

        private void btnCart_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.FrmMain.Navigate(new PageCart());
        }

        private void btnBuy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                listProducts.ItemsSource = BunAndBagelEntities.GetContext().ProductBunAndBagel.ToList();
                Button b = sender as Button;

                int selectedGoodsId = int.Parse(((b.Parent as StackPanel).Children[0] as TextBlock).Text);

                int idUsers = Convert.ToInt32(App.Current.Properties["IdUser"].ToString()); 

                var order = BunAndBagelEntities.GetContext().OrderingProducts.FirstOrDefault(o => o.IdUser == idUsers);

                if (order == null)
                {
                    order = new OrderingProducts()
                    {
                        IdUser = idUsers,
                        IdOrder = 1,
                        IdProduct = selectedGoodsId
                    };
                    BunAndBagelEntities.GetContext().OrderingProducts.Add(order);
                    BunAndBagelEntities.GetContext().SaveChanges();
                }

                var cartnew = new Cart()
                {
                    Id_Order = order.IdOrder,
                    Id_Product = selectedGoodsId
                };

                BunAndBagelEntities.GetContext().Cart.Add(cartnew);
                BunAndBagelEntities.GetContext().SaveChanges();


                MessageBox.Show("Товар успешно добавлен в корзину!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                AppFrame.FrmMain.Navigate(new PageCart());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при добавлении товара в корзину: " + ex.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            comboSort.SelectedIndex = -1;
            TBoxSearch.Clear();
        }
    }
}
