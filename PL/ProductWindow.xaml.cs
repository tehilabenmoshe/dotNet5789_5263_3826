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
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace PL
{
    /// <summary>
    /// Interaction logic for EditProduct.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        BlApi.IBL? bl = BlApi.Factory.Get() ?? throw new NullReferenceException("missing bl");
        public ProductWindow()
        {
            InitializeComponent();
            CategoryBox.ItemsSource = Enum.GetValues(typeof(BO.Category));
        }

        private void CategoryBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //
            //if (CategoryBox.SelectedItem is "")
            //    CategoryBox.SelectedItem = BO.Category.none;

            //if (CategoryBox.SelectedItem is BO.Category.all)
            //    IEnumerableToPL(bl!.Product.getProductForList());
            //else if (CategoryBox.SelectedItem is BO.Category)
            //    IEnumerableToPL(bl!.Product.FilterProductList(p => p.Category == (BO.Category)CategoryBox.SelectedItem));
            //else if (CategoryBox.SelectedItem is "")
            //    IEnumerableToPL(bl!.Product.getProductForList());

        }



        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void CategoryBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddBox_Click(object sender, RoutedEventArgs e)//אמור לעבוד
        {
            BO.Product temp = new BO.Product();
            temp.Name = NameBox.Text;
            temp.ID = IDBox.;
            temp.Price = (double)PriceBox.Text;
            temp.InStock = InStockBox.;
            temp.Category = (BO.Category)CategoryBox.SelectedItem;
            bl!.Product.AddProduct(temp);
        }
    }
}
