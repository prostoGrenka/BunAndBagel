using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using BunAndBagel.ApplicationData;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.IO;
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
//using Aspose.BarCode.Generation;
using Image = iTextSharp.text.Image;
using Paragraph = iTextSharp.text.Paragraph;
using System.Xml.Linq;
using Document = iTextSharp.text.Document;

namespace BunAndBagel.PageApplication
{
    /// <summary>
    /// Логика взаимодействия для PageCart.xaml
    /// </summary>
    public partial class PageCart
    {
        public PageCart()
        {
            InitializeComponent();

            //var orderobj = BunAndBagelEntities.GetContext().Order
            //                  .Where(x => x.idUsers == idusercart)
            //                  .Select(x => x.idOrder)
            //                  .ToList();

            //var cartobj = BunAndBagelEntities.GetContext().Cart
            //                   .Where(c => orderobj.Contains(c.idOrder))
            //                   .Select(x => x.idGoods)
            //                   .ToList();
            //var goodsInCart = BunAndBagelEntities.GetContext().beautyGoods
            //                             .Where(x => cartobj.Contains(x.idGoods))
            //                             .ToList();
            //ListOrders.ItemsSource = goodsInCart;


            //if (ListOrders.Items.Count > 0)
            //{
            //    btnCheckout.IsEnabled = true;
            //    tbCounter.Text = "Всего в корзине " + ListOrders.Items.Count + " товаров";
            //}
            //else
            //{
            //    btnCheckout.IsEnabled = false;
            //    btnCheckout.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F6C5DC"));
            //    tbCounter.Text = "Ваша корзина пустая!";
            //}
        }

        //    private void btnAdd_Click(object sender, RoutedEventArgs e)
        //    {
        //        var userObj = AppConnect.modelOdb.User;

        //        int us = Convert.ToInt32(App.Current.Properties["roleUser"].ToString());
        //        if (us == 1)
        //        {
        //            AppFrame.FrmMain.Navigate(new Main((sender as Button).DataContext as User));
        //        }
        //        else if (us == 2)
        //        {
        //            AppFrame.FrmMain.Navigate(new Main((sender as Button).DataContext as User));
        //        }
        //    }

        //    private void btnDel_Click(object sender, RoutedEventArgs e)
        //    {
        //        if (MessageBox.Show("Вы точно хотите удалить выбранный товар из заказа?", "Подтверждение удаления",
        //            MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
        //        {

        //            ListOrders.ItemsSource = BunAndBagelEntities.GetContext().Cart.ToList();
        //            Button b = sender as Button;
        //            int ID = int.Parse(((b.Parent as StackPanel).Children[0] as TextBlock).Text);
        //            Console.WriteLine(ID);
        //            AppConnect.modelOdb.Cart.Remove(
        //            AppConnect.modelOdb.Cart.Where(x => x.Id == ID).First());
        //            AppConnect.modelOdb.SaveChanges();
        //            AppFrame.FrmMain.GoBack();
        //            AppFrame.FrmMain.Navigate(new PageCart());
        //        }
        //    }

        //    private void btnCheckout_Click(object sender, RoutedEventArgs e)
        //    {
        //        if (MessageBox.Show("Вы точно хотите оформить заказ? Все товары из вашей корзины будут удалены!", "Внимание!",
        //            MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
        //        {
        //            try
        //            {
        //                addManager();
        //                CreatePDF();
        //                MessageBox.Show("PDF документ был успешно загружен!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        //                RemoveItemsFromCart();
        //                AppFrame.FrmMain.Navigate(new PageQR());

        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show(ex.Message);
        //            }

        //        }
        //    }

        //    private void CreatePDF()
        //    {
        //        iTextSharp.text.Document document = new Document();

        //        try
        //        {
        //            string downloadFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads\\";
        //            int fileIndex = 1;

        //            string fileName = "";
        //            do
        //            {
        //                fileName = System.IO.Path.Combine(downloadFolder, $"order_{fileIndex}.pdf");
        //                fileIndex++;
        //            } while (File.Exists(fileName));

        //            PdfWriter.GetInstance(document, new FileStream(fileName, FileMode.Create));

        //            document.Open();
        //            BaseFont baseFont = BaseFont.CreateFont("C:\\Windows\\Fonts\\Arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

        //            Font font = new Font(baseFont, 12);
        //            Font font1 = new Font(baseFont, 24, 2, BaseColor.GRAY);
        //            Paragraph paragraph1 = new Paragraph("Список товаров", font1);
        //            paragraph1.Alignment = Element.ALIGN_CENTER;
        //            document.Add(paragraph1);
        //            decimal sum = 0;

        //            var goodsobj = BunAndBagelEntities.GetContext().Cart
        //                                 .Where(x => x.orders.idUsers == idusercart)
        //                                 .ToList();
        //            foreach (var item in goodsobj)
        //            {
        //                if (item is Cart data)
        //                {

        //                    Image img = Image.GetInstance("\"C:\\Users\\Evgen\\Downloads\"" + data.beautyGoods.CurrentPhoto);
        //                    img.ScaleAbsolute(100f, 100f);
        //                    document.Add(img);
        //                    document.Add(new Paragraph("Название: " + data.ProductBunAndBagel.nameGoods, font));
        //                    document.Add(new Paragraph("Категория: " + data.ProductBunAndBagel.category1.nameCategory, font));
        //                    document.Add(new Paragraph("Тип товара: " + data.ProductBunAndBagel.typeGoods1.nameType, font));
        //                    document.Add(new Paragraph("Описание: " + data.ProductBunAndBagel.description, font));
        //                    document.Add(new Paragraph("Цена: " + data.Pro.price.ToString() + " руб.", font));
        //                    document.Add(new Paragraph(" "));
        //                    sum += data.beautyGoods.price;

        //                }
        //            }
        //            Paragraph paragraph = new Paragraph("Сумма = " + sum.ToString(), font);
        //            paragraph.Alignment = Element.ALIGN_RIGHT;
        //            document.Add(paragraph);
        //        }
        //        catch (DocumentException de)
        //        {
        //            MessageBox.Show(de.Message);
        //        }
        //        catch (IOException ioe)
        //        {
        //            MessageBox.Show(ioe.Message);
        //        }
        //        finally
        //        {
        //            document.Close();
        //        }
        //    }
        //    private void pageVisible(object sender, DependencyPropertyChangedEventArgs e)
        //    {
        //        if (Visibility == Visibility.Visible)
        //        {
        //            BunAndBagelEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
        //            ListOrders.ItemsSource = BunAndBagelEntities.GetContext().cart.ToList();
        //        }
        //    }

        //    public void RemoveItemsFromCart()
        //    {

        //        var context = BunAndBagelEntities.GetContext();
        //        var itemsToDelete = context.cart
        //        .Where(x => x.orders.idUsers == idusercart)
        //        .ToList();

        //        if (itemsToDelete.Any())
        //        {
        //            context.cart.RemoveRange(itemsToDelete);
        //            context.SaveChanges();
        //        }
        //    }

        //    public void addManager()
        //    {
        //        int idUsers = Convert.ToInt32(App.Current.Properties["idUser"].ToString());
        //        var order = BunAndBagelEntities.GetContext().Order.FirstOrDefault(o => o.Id_User == idUsers);
        //        var cartItems = BunAndBagelEntities.GetContext().Cart.Where(c => c.Order.idUsers == idUsers);

        //        order = new Order()
        //        {
        //            idUsers = idUsers,
        //            idStatus = 1
        //        };

        //        BunAndBagelEntities.GetContext().Order.Add(order);
        //        BunAndBagelEntities.GetContext().SaveChanges();

        //        foreach (var cartItem in cartItems)
        //        {
        //            var cartNew = new OrderingProducts()
        //            {
        //                idOrder = order.idOrder,
        //                idGoods = cartItem.idGoods
        //            };

        //            BunAndBagelEntities.GetContext().OrderingProducts.Add(cartNew);
        //        }

        //        BunAndBagelEntities.GetContext().SaveChanges();
        //    }
        //}
    }
}
