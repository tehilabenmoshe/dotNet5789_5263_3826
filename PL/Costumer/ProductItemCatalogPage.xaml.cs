using BO;
using PL.Manager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ProductItemCatalogPage.xaml
    /// </summary>
    public partial class ProductItemCatalogPage : Page
    {
        Frame temp;

        BlApi.IBL? bl = BlApi.Factory.Get() ?? throw new NullReferenceException("missing bl");
        ObservableCollection<ProductItem> productItem = new();
        public ProductItemCatalogPage(Frame f )
        {
            InitializeComponent();
            IEnumerableToPL(bl.Product.GetProductItemListForCustomer()); //returns the productItem List from bo
            ProductItemListView.DataContext = productItem;
            CategoryBox.ItemsSource = Enum.GetValues(typeof(BO.Category));
            temp = f;
        }



        private void IEnumerableToPL(IEnumerable<ProductItem> list)
        {
            productItem.Clear();
            foreach (var temp in list)
                productItem.Add(temp);
        }

        private void ProductItemListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CategoryBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CategoryBox.SelectedItem is BO.Category.all)
                IEnumerableToPL(bl!.Product.GetProductItemListForCustomer());
            else if (CategoryBox.SelectedItem is BO.Category)
                IEnumerableToPL(bl!.Product.FilterProducItemtList(p => p.Category == (BO.Category)CategoryBox.SelectedItem));
            else if (CategoryBox.SelectedItem is "")
                IEnumerableToPL(bl!.Product.GetProductItemListForCustomer());
        }

      

        private void OnClick(object sender, RoutedEventArgs e)
        {
            CartWindow c =new CartWindow();
            c.Show();
        }


        private void ProductItemListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            InitializeComponent();
            // CartWindow cw = new CartWindow((BO.ProductItem)ProductItemListView.SelectedItem);
            //ProductWindow plv = new ProductWindow(((BO.ProductForList)ProductListView.SelectedItem).ID);
            // plv.InitializeComponent();
            //  plv.Show();
            //OrderWindow o = new OrderWindow((BO.OrderForList)ProductItemListView.SelectedItem);

            //o.InitializeComponent();
            //o.Show();


            //  ProductToCartPage p = new ProductToCartPage((BO.ProductItem)ProductItemListView.SelectedItem);
            // p.Show();
            //this.Content= new ProductToCartPage((BO.ProductItem)ProductItemListView.SelectedItem);
            temp.Content= new ProductToCartPage((BO.ProductItem)ProductItemListView.SelectedItem);
            
        }



    }
}
