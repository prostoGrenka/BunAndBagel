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
		int idUserCart = Convert.ToInt32(App.Current.Properties["Id"].ToString());  // Берём ID юзера со страницы авторизации
		public PageCart()
        {
            InitializeComponent();

			//List<ProductBunAndBagel> productInCart = AppConnect.modelOdb.ProductBunAndBagel.ToList();
			//var currentProduct = BunAndBagelEntities.GetContext().ProductBunAndBagel.ToList();
			//ListOrders.ItemsSource = currentProduct;

			var orderoObj = BunAndBagelEntities.GetContext().Order  
				   .Where(x => x.Id_User == idUserCart)
				   .Select(x => x.Id)
				   .ToList();

			var cartobj = BunAndBagelEntities.GetContext().Cart
				   .Where(c => orderoObj.Contains((int)c.Id_Order))
				   .Select(x => x.Id_Product)
				   .ToList();

			var goodsInCart = BunAndBagelEntities.GetContext().ProductBunAndBagel
							 .Where(x => cartobj.Contains(x.Id))
							 .ToList();

			ListOrders.ItemsSource = goodsInCart;

			ListOrders.ItemsSource = goodsInCart;

			if (ListOrders.Items.Count <= 0)
			{
				ListOrders.IsEnabled = false;
			}
			else { ListOrders.IsEnabled = true; }
		}

		private void btnBack_Click(object sender, RoutedEventArgs e)
		{
			AppFrame.FrmMain.Navigate(new Main((sender as Button).DataContext as User));
		}

		private void btnCheckout_Click(object sender, RoutedEventArgs e)
		{

			if (MessageBox.Show($"Вы точно хотите сформировать заказ?", "Внимание",
			   MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
			{
				try
				{
					CreatePDF();
					AddManagerOrder();
					removecart();
					MessageBox.Show("PDF документ заказа успешно сформирован!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
					AppFrame.FrmMain.Navigate(new PageQR());
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message.ToString());
				}
			}

		}

		private void CreatePDF()
		{
			Document doc = new Document();

			try
			{
				string fileName = System.IO.Path.Combine("C:\\Users\\Evgen\\Downloads",  $"order_{ DateTime.Now:yyyyMMddHH}.pdf");
				PdfWriter.GetInstance(doc, new FileStream(fileName, FileMode.Create));

				doc.Open();
				BaseFont basefont = BaseFont.CreateFont("C:\\Windows\\Fonts\\Arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

				Font font = new Font(basefont, 12);
				Font font1 = new Font(basefont, 25, 3, BaseColor.BLUE);
				Paragraph paragraph1 = new Paragraph("список товаров", font1);
				doc.Add(paragraph1);
				int sum = 0;

				var orderObj = BunAndBagelEntities.GetContext().Order
							   .Where(x => x.Id_User == idUserCart)
							   .Select(x => x.Id)
							   .ToList();

				var cartObj = BunAndBagelEntities.GetContext().Cart
								   .Where(c => orderObj.Contains((int)c.Id_Order))
								   .ToList();

				foreach (var item in cartObj)
				{
					if (item is Cart)
					{
						Cart data = (Cart)item;
						Image img = Image.GetInstance("C:\\Users\\Evgen\\Desktop\\BunAndBagel-master\\BunAndBagel\\Picture\\" + data.ProductBunAndBagel.CurrentPhoto);
						img.ScaleAbsolute(100f, 100f);
						doc.Add(img);
						doc.Add(new Paragraph("Haзвaние: " + data.ProductBunAndBagel.Name, font));
						doc.Add(new Paragraph("Oпиcaние: " + data.ProductBunAndBagel.Desciption, font));
						doc.Add(new Paragraph("Категория: " + data.ProductBunAndBagel.Category, font));
						doc.Add(new Paragraph("Cтоимость: " + data.ProductBunAndBagel.Price.ToString() + "py6.", font));
						sum += (int)data.ProductBunAndBagel.Price;
					}
				}
				Paragraph paragraph = new Paragraph("Cyммa = " + sum.ToString() + "руб.", font);
				paragraph.Alignment = Element.ALIGN_RIGHT;
				doc.Add(paragraph);
			}
			catch (DocumentException de)
			{
				Console.WriteLine(de.Message);
			}
			catch (IOException ioe)
			{
				Console.Error.WriteLine(ioe.Message);
			}
			finally
			{
				doc.Close();
			}
		}

		private void AddManagerOrder()
		{
			try
			{
				var orderObj = BunAndBagelEntities.GetContext().Order
				.Where(x => x.Id_User == idUserCart)
				.Select(x => x.Id)
				.ToList();

				var cartObj = BunAndBagelEntities.GetContext().Cart
								   .Where(c => orderObj.Contains((int)c.Id_Order))
								   .ToList();

				foreach (var item in cartObj)
				{
					int idUsers = Convert.ToInt32(App.Current.Properties["Id"].ToString());
					var order = BunAndBagelEntities.GetContext().Order.FirstOrDefault(o => o.Id_User == idUsers);
					var cartnew = new OrderingProducts()
					{
						IdUser = idUsers,
						IdOrder = order.Id,
						IdProduct = (int)item.Id_Order,
						IdStatusOrder = 2
					};
					BunAndBagelEntities.GetContext().OrderingProducts.Add(cartnew);
					BunAndBagelEntities.GetContext().SaveChanges();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message.ToString());
			}
		}

		private void removecart()
		{
			int userId = Convert.ToInt32(App.Current.Properties["Id"]);
			var order = BunAndBagelEntities.GetContext().Order.FirstOrDefault(o => o.Id_User == userId && o.Id_StatusOrder == 2);

			var cartItems = BunAndBagelEntities.GetContext().Cart.Where(c => c.Id_Product == order.Id).ToList();
			BunAndBagelEntities.GetContext().Cart.RemoveRange(cartItems);
			BunAndBagelEntities.GetContext().SaveChanges();


			ListOrders.ItemsSource = new List<Cart>();

		}
	}
}

