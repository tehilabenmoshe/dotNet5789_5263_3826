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

namespace PL.Costumer
{
    /// <summary>
    /// Interaction logic for ProductToCartPage.xaml
    /// </summary>
    public partial class ProductToCartPage : Page
    {
        BO.ProductItem temp=new BO.ProductItem();
        public ProductToCartPage()
        {
            InitializeComponent();
        }

        public ProductToCartPage(BO.ProductItem p)
        {
            InitializeComponent();
            ProductItemName.Text = p!.Name.ToString();
            ProductItemCategory.Text = p!.Category.ToString();
            PriceBox.Text = p!.Price.ToString();
            //InStockBox.Text = p!.InStock.ToString();

            if (p.InStock)
                InStockBox.Text = "Product In-Stock";
            else
                InStockBox.Text = "Product Out Of Stock";

            temp = p;

        }

        private void ProductItemCategory_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            CartWindow c = new CartWindow(temp);
            c.Show();   
        }
    }
}
