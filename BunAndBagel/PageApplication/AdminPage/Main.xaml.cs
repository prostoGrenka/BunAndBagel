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

            if (products.Count > 0)
            {
                tbCounter.Text = "Найдено " + products.Count.ToString() + "товаров";
            }
            else
            {
                tbCounter.Text = "Ничего не найдено";
            }
            listProducts.ItemsSource = products; //hi
        }
    }
}
